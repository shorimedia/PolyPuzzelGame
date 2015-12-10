using UnityEngine;

//
// Script Name: PegUpdater
//Script by: Victor L Josey
// Description: Checks and Update Peg status and surrounding pegs
// (c) 2015 Shoori Studios LLC  All rights reserved.

public class PegUpdater : MonoBehaviour {

	public Transform[] checkPoints = new Transform[6]; 

	public PegStateMachine pegState_Update;
	public PegTypeMach PegType;
	public PosStatus posStatus;
    public PegController pegControl;


    //[HideInInspector]
    public PegStateMachine[] neighborHEX = new PegStateMachine[6];

	[HideInInspector]
	public int 	tokenIndex;      //indcate what side the sending  neighbor is on.

	// Use this for initialization
    void Start()
    {
        //Messenger.AddListener("Check Neighbor", CheckNeighbor);
    }


    void FixedUpdate()
    {
        if (GameManger.ACTIVE == false && pegState_Update.moveIn == true)
        {

            pegState_Update.blockState = PegStateMachine.BlockState.Normal;
            pegState_Update.ChangeBlockState();
            pegState_Update.moveIn = false;

        }


        if (GameManger.ACTIVE == false && pegState_Update.isJumpable == true)
        {
            pegState_Update.isJumpable = false;
        }

        if (GameManger.ACTIVE == false && pegState_Update.blockState == PegStateMachine.BlockState.Uncapable)
        {
            pegState_Update.blockState = PegStateMachine.BlockState.Normal;
            pegState_Update.ChangeBlockState();
        }


        if (GameManger.ACTIVE == true && pegState_Update.blockState != PegStateMachine.BlockState.Active)
        {
            pegControl.canTouch = false;

            if (GameManger.ACTIVE == true && PegType.blockType == PegTypeMach.BlockType.Empty)
            {
                pegControl.canTouch = true;
            }

        }
        else
        {
            pegControl.canTouch = true;

        }
    }

    public void SetNeighborPegs()
    {

        RaycastHit hit;

        for (int c = 0; c < checkPoints.Length; c++)
        {

            Ray ray = new Ray(checkPoints[c].position, checkPoints[c].forward);

            if (Physics.Raycast(ray, out hit, 1))
            {

                if (hit.collider.tag == "Peg")
                {

                    //Debug.Log ("Hit object at check point " + (c + 1 ) + " "+ hit.collider.name );
                    if (neighborHEX[c] == null)
                    {
                        neighborHEX[c] = hit.collider.gameObject.GetComponent<PegStateMachine>();
                    }

                }
                else
                {
                    // if ray hit any other object onther then Peg tag
                    posStatus.side[c] = 'N';
                }
            }
            else
            {
                // if ray doesnt hit any thing
                posStatus.side[c] = 'N';
            }

        }



    }



	#region Check Peg's Neighbor
	
	public void CheckNeighbor(){

		// check Peg neighbors
		
		for(int c = 0; c < checkPoints.Length; c++){

            if (neighborHEX[c] != null)
            {
                    /// If peg and the next peg
					if(PegType.blockType != PegTypeMach.BlockType.Empty){
						if (neighborHEX[c].PegType.blockType != PegTypeMach.BlockType.Empty )
                        {
							// Check the next peg over
							neighborHEX[c].pagState_pUpdater.SecondNeighborCheck(c); 
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


               // if (neighborHEX[c].posStatus.posState == PosStatus.PosState.IsoPeg)
              ///  {
              ///      posStatus.side[c] = 'E';
              ///  }
            }
			
		}
		
		
		// Set Status
		posStatus.SetPositionState();

		//pegState.SelectedSpace();
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


        if (neighborHEX[index] != null)
        {
            // Check if neighbor thats in the direction is emptyS
				if (neighborHEX[index].PegType.blockType == PegTypeMach.BlockType.Empty )
                {
					
					// if block one and to are compatible
					
					// check actived neighbor via revese index direction and check if the type is jumpable

					if(PegType.CompareType(neighborHEX[PointIndex(index)].PegType.blockType ) )
					{
						pegState_Update.isJumpable = true;

						if(neighborHEX[PointIndex(index)].blockState == PegStateMachine.BlockState.Active)
						{
							neighborHEX[index].moveIn = true;
						}


						neighborHEX[index].pagState_pUpdater.tokenIndex = PointIndex(index);
						GameManger.CURRENT_OPEN_BLOCK = neighborHEX[index];
					}else{   
	
						if(neighborHEX[PointIndex(index)].blockState == PegStateMachine.BlockState.Active)
						{
						    pegState_Update.blockState = PegStateMachine.BlockState.Uncapable;
                            neighborHEX[index].pagState_pUpdater.tokenIndex = PointIndex(index);
                            pegState_Update.ChangeBlockState();
						}else{
							pegState_Update.blockState = PegStateMachine.BlockState.Normal;
                            neighborHEX[index].pagState_pUpdater.tokenIndex = PointIndex(index);
                            pegState_Update.ChangeBlockState();
						}

					

					}

                    neighborHEX[index].blockState = PegStateMachine.BlockState.Selected;
                    neighborHEX[index].ChangeBlockState();

					//Debug.Log (" check two! done");
					
				}
				
			
			}
		
		//	Debug.Log ("Index # " + index);
	}
	


	void OnDisable(){
		
		//Messenger.RemoveListener( "Check Neighbor", CheckNeighbor  );

		
	}
}
