using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PegUpdater : MonoBehaviour {

	public Transform[] checkPoints = new Transform[6]; 

	public PegStateMachine pegState;
	public PegTypeMach PegType;
	public PosStatus posStatus;

	//[HideInInspector]
	public PegStateMachine[] neighborHEX = new PegStateMachine[6];

	[HideInInspector]
	public int 	tokenIndex;      //indcate what side the sending  neighbor is on.




	// Use this for initialization
	void Start () {
		Messenger.AddListener( "Check Neighbor", CheckNeighbor );
	}


	#region Check Peg's Neighbor
	
	public void CheckNeighbor(){

		if(PegType.blockType == PegTypeMach.BlockType.Empty && GameManger.ACTIVE == false)
		{
			pegState.moveIn = false;
		}


		// check Peg neighbors
		
		RaycastHit hit;
		
		for(int c = 0; c < checkPoints.Length; c++){
			
			Ray ray = new Ray (checkPoints[c].position,  checkPoints[c].forward);
			
			if (Physics.Raycast(ray , out hit,1)){
				
				if (hit.collider.tag == "Peg"){ 
					
					
					//Debug.Log ("Hit object at check point " + (c + 1 ) + " "+ hit.collider.name );
					if(neighborHEX[c] == null){
						neighborHEX[c] = hit.collider.gameObject.GetComponent<PegStateMachine>();
					}

				
					if(PegType.blockType != PegTypeMach.BlockType.Empty){
						if (neighborHEX[c].PegType.blockType != PegTypeMach.BlockType.Empty ){
							//Debug.Log (" check next");
							neighborHEX[c].pUpdater.SecondNeighborCheck(c); }


					}else{
						

						if (neighborHEX[c].PegType.blockType != PegTypeMach.BlockType.Empty ){
							
						}
						
					}


					// use for end game condition n = null side
					
					if (neighborHEX[c].PegType.blockType == PegTypeMach.BlockType.Empty  || neighborHEX[c].PegType.CompareType(PegType.blockType) == false )
					{
						posStatus.side[c] = 'E';

					}else if(neighborHEX[c].PegType.CompareType(PegType.blockType)  ==  true  && neighborHEX[c].posStatus.side[c] != 'E' && neighborHEX[c].PegType.blockType != PegType.blockType)
					{
						posStatus.side[c] = 'E';

					}else 
					{

						posStatus.side[c] = 'C';


					}
					
				}else{
					// if ray hit any other object onther then Peg tag
					posStatus.side[c] = 'N';
				}
				
			}else{
				// if ray doesnt hit any thing
				posStatus.side[c] = 'N';
			}
			
		}
		
		
		// Set Status
		posStatus.SetPositionState();

		
	}
	#endregion





	int PointIndex(int num){
		// tell block what checkpoint to send a ray too
		int pointNum = num;
		
		switch(num){
		case 0 : pointNum = 3;  break;
		case 1 : pointNum = 4;  break;
		case 2 : pointNum = 5;  break;
		case 3 : pointNum = 0;  break;
		case 4 : pointNum = 1;  break;
		case 5 : pointNum = 2;  break;
		default : Debug.LogWarning("Index number out of range!!"); break;
			
		}
		return pointNum;
		
	}
	
	
	
	// If block is a middle block. check next if its empty type
	
	void SecondNeighborCheck(int index){
		RaycastHit hit;
		
		Ray ray = new Ray (checkPoints[index].position,  checkPoints[index].forward);
		
		if (Physics.Raycast(ray , out hit,1)){
			if (hit.collider != null){ 
				//			Debug.Log ("Hit object at check point " + (index + 1 ) + " "+ hit.collider.name );
				neighborHEX[index] = hit.collider.gameObject.GetComponent<PegStateMachine>();
				
				if (neighborHEX[index].PegType.blockType == PegTypeMach.BlockType.Empty ){
					
					// if block one and to are compatible
					
					
					// check actived neighbor via revese index direction and check if the type is jumpable
					if(PegType.CompareType(neighborHEX[PointIndex(index)].PegType.blockType ) )
					{
						pegState.isJumpable = true;
						if(GameManger.ACTIVE == true)
						{
						neighborHEX[index].moveIn = true;
						}else{
							neighborHEX[index].moveIn = false;
						}
						neighborHEX[index].pUpdater.tokenIndex = PointIndex(index);
						GameManger.CURRENT_OPEN_BLOCK = neighborHEX[index];
					}else{   
						pegState.isJumpable = false;

						if(GameManger.ACTIVE == true){
						pegState.blockState = PegStateMachine.BlockState.Uncapable;
						}else{
							pegState.blockState = PegStateMachine.BlockState.Normal;
						}

						pegState.ChangeBlockState();
						neighborHEX[index].moveIn = false;
						neighborHEX[index].pUpdater.tokenIndex = PointIndex(index);
					}

					Debug.Log (" check two! done");
					
				}
				
			}
			
		}
		//	Debug.Log ("Index # " + index);
	}
	
	

	void OnDisable(){
		
		Messenger.RemoveListener( "Check Neighbor", CheckNeighbor  );
		
	}
}
