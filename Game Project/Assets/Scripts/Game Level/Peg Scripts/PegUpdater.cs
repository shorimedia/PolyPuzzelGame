using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PegUpdater : MonoBehaviour {

	public Transform[] checkPoints = new Transform[6]; 

	public PegStateMachine pegState;
	public PegTypeMach PegType;


	public PegStateMachine[] neighborHEX = new PegStateMachine[6];

	public int 	tokenIndex;      //indcate what side the sending  neighbor is on.

	public int _ping = 0;
	public int _nullPing = 0;


	public PosStatus posStatus;

	// Use this for initialization
	void Start () {
		Messenger.AddListener( "Check Neighbor", CheckNeighbor );
	}


	#region Check Peg's Neighbor
	
	public void CheckNeighbor(){
		
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
					
					if (neighborHEX[c].PegType.blockType == PegTypeMach.BlockType.Empty )
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
		SetHexPosStat();
		
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
					
					
					// check avctived neighbor via revase index direction and check if the type is jumpable
					if(PegType.CompareType(neighborHEX[PointIndex(index)].PegType.blockType ) == true)
					{
						pegState.isJumpable = true;
						neighborHEX[index].moveIn = true;
						neighborHEX[index].pUpdater.tokenIndex = PointIndex(index);
						GameManger.CURRENT_OPEN_BLOCK = neighborHEX[index];
					}else{   
						pegState.isJumpable = false;
						pegState.blockState = PegStateMachine.BlockState.Uncapable;
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
	
	
	public void EmptyPing(){
		
		RaycastHit hit;
		
		for(int c = 0; c < checkPoints.Length; c++){
			
			Ray ray = new Ray (checkPoints[c].position,  checkPoints[c].forward);
			
			if (Physics.Raycast(ray , out hit,1)){
				if (hit.collider != null){ 
					//Debug.Log ("Hit object at check point " + (c + 1 ) + " "+ hit.collider.name );
					if(neighborHEX[c] == null){
						neighborHEX[c] = hit.collider.gameObject.GetComponent<PegStateMachine>();
					}
					
					if (neighborHEX[c].PegType.blockType != PegTypeMach.BlockType.Empty ){
						_ping++;
						GameManger.TOTAL_PINGS ++;
					}
				}}
			
			if(neighborHEX[c] == null){
				_nullPing ++;
				GameManger.TOTAL_NULL_PINGS ++;     
			}
			
			
			
		}
		
	}  
	
	
	// check if there a peg next to this one
	public void UpdateNeighbor(){
		// check block neighbors
		
		RaycastHit hit;
		
		for(int c = 0; c < checkPoints.Length; c++){
			
			Ray ray = new Ray (checkPoints[c].position,  checkPoints[c].forward);
			
			if (Physics.Raycast(ray , out hit,1)){
				if (hit.collider != null){ 
					
					if (neighborHEX[c].PegType.blockType == PegTypeMach.BlockType.Empty ){
						GameManger.TOTAL_PINGS -= neighborHEX[c].pUpdater._ping ;
						neighborHEX[c].pUpdater._ping = 0;
						GameManger.TOTAL_NULL_PINGS -= neighborHEX[c].pUpdater._nullPing ;
						neighborHEX[c].pUpdater._nullPing = 0;
						neighborHEX[c].pUpdater.EmptyPing();
					}
				}
			}}
	}



	void SetHexPosStat()
	{
		posStatus.SetPositionState();
		
		// Check if end peg is cennected to a group
		if( posStatus.posState == PosStatus.PosState.EndPeg )
		{
			
			for(int i =0; i < neighborHEX.Length; i++)
			{
				
				if(neighborHEX[i] != null  && neighborHEX[i].posStatus.posState == PosStatus.PosState.AlignPeg )
				{
					// end peg is in a group
					posStatus.cennectedGroup = true;
					
					neighborHEX[i].posStatus.cennectedGroup = true;
					
					// set up a new group
					if(posStatus.groupIndex == 0)
					{
						
						EndGameCheck.TotalNum_GroupIndex++;
						
						posStatus.groupIndex = EndGameCheck.TotalNum_GroupIndex;
						
						EndGameCheck.groups.Add(posStatus.groupIndex, new List<PosStatus>());
						
						EndGameCheck.groups[posStatus.groupIndex].Add(posStatus);
						
					}
					
					neighborHEX[i].posStatus.groupIndex = posStatus.groupIndex;
				}
				
				
			}
			
			
			
		}else if(posStatus.posState == PosStatus.PosState.AlignPeg && posStatus.cennectedGroup == true)
		{
			
			for(int i =0; i < neighborHEX.Length; i++)
			{
				
				if(neighborHEX[i] != null  && neighborHEX[i].posStatus.posState == PosStatus.PosState.AlignPeg )
				{
					neighborHEX[i].posStatus.groupIndex = posStatus.groupIndex;
					neighborHEX[i].posStatus.cennectedGroup = true;
				}
			}
		}else { posStatus.cennectedGroup = true; }
	}


	
	void OnDisable(){
		
		Messenger.RemoveListener( "Check Neighbor", CheckNeighbor  );
		
	}
}
