using UnityEngine;
using System.Collections;

public class HexBlock : MonoBehaviour {
	public Transform[] checkPoints = new Transform[6]; 

	public enum BlockType {
		Red,
		Blue,
		Green,
		Empty
	}

	public BlockType blockType = BlockType.Blue;


	public enum BlockState{
		EmptyState,
		Active,
		Normal,
		Dissolve,
		Hover,
		Selected,
		Move
	}

	public BlockState blockState = BlockState.Normal;


	public HexBlock[] neighborHEX = new HexBlock[6];

	public bool isJumpable = false;
	public bool moveIn  = false;  // if a block is empty and block can move into there set true 
	public int 	tokenIndex;      //indcate what side the sending  neighbor is on.

	private int possibleMoves = 0; // remove this
	private int _ping = 0;
	private int _nullPing = 0;


	public HexType hexType;





	void Awake(){
		
		Messenger.AddListener( "Check Neighbor", CheckNeighbor );
		hexType = new HexType(HexType.BlockType.Fire);

	}
	
	// Use this for initialization
	void Start () {
		ChangeBlockType();
	}

	public void CheckNeighbor(){
		// check block neighbors
	
		RaycastHit hit;

		for(int c = 0; c < checkPoints.Length; c++){

			Ray ray = new Ray (checkPoints[c].position,  checkPoints[c].forward);

		if (Physics.Raycast(ray , out hit,1)){
				if (hit.collider != null){ 


					//Debug.Log ("Hit object at check point " + (c + 1 ) + " "+ hit.collider.name );
					if(neighborHEX[c] == null){
					neighborHEX[c] = hit.collider.gameObject.GetComponent<HexBlock>();
					}
					if(blockType != BlockType.Empty){
					if (neighborHEX[c].blockType != BlockType.Empty ){
						//Debug.Log (" check next");
						neighborHEX[c].SecondNeighborCheck(c); }
					}else{

						if (neighborHEX[c].blockType != BlockType.Empty ){

						}

					}}
		}}

	}






	void OnMouseOver() {

		if(blockState != BlockState.Active && blockType != BlockType.Empty ){
		blockState = BlockState.Hover;
		ChangeBlockState();
		}else if (moveIn == true && blockType == BlockType.Empty){
			blockState = BlockState.Hover;
			ChangeBlockState();
		}
	}

	void OnMouseExit() {
		if(blockState != BlockState.Active ){
		blockState = BlockState.Normal;
			ChangeBlockState();
		}


		//if active set empty blocks back fasle
		if(GameManger.ACTIVE == false){
			moveIn = false;
		}

	}

	void OnMouseUp() {
		//Debug.Log("Drag ended!");
	}


	void OnMouseDown() {
	

		if(blockState != BlockState.Active && GameManger.ACTIVE == false && blockType != BlockType.Empty){
			blockState = BlockState.Active;

		}else if(blockState == BlockState.Active){
			GameManger.ACTIVE = false;
			blockState = BlockState.Normal;
			}else if( GameManger.ACTIVE == true && blockType == BlockType.Empty && moveIn == true){
			blockState = BlockState.Move;
			GameManger.ACTIVE = false;

		}



		ChangeBlockState();
		//Debug.Log("Click!");
	}


	public void ChangeBlockState(){

		switch (blockState){
		case BlockState.EmptyState :
			if (moveIn == true){
				// block type = moved block
				blockState = BlockState.Move;
			}

			break;
		case BlockState.Active :
			GameManger.ACTIVE = true;
			GameManger.CURRENT_ACTIVE_BLOCK = this.GetComponent<HexBlock>();
			CheckNeighbor();
			renderer.material.color = new Color(0, 1, 1);

			break;
		case BlockState.Normal : 
			ChangeBlockType();
			break;

		case BlockState.Dissolve :
			if(isJumpable == true){
				GameManger.TOTAL_POINTS_COUNT += hexType.points; 
				isJumpable = false;
			}

			blockType = BlockType.Empty;
			//ChangeBlockType();

			blockState = BlockState.Normal;

			EmptyPing();
			UpdateNeighbor();
			ChangeBlockState();
			break;
		case BlockState.Hover	:
			ChangeBlockType();
			renderer.material.color = new Color(0, 30, 1);

			break;
		case BlockState.Selected : break;
		case BlockState.Move :
			if(GameManger.CURRENT_ACTIVE_BLOCK != null){
				blockType = GameManger.CURRENT_ACTIVE_BLOCK.blockType; 

				RaycastHit hit;
				Ray ray = new Ray (checkPoints[tokenIndex].position,  checkPoints[tokenIndex].forward);
				
				if (Physics.Raycast(ray , out hit,1)){
					if (hit.collider != null){ 
						neighborHEX[tokenIndex] = hit.collider.gameObject.GetComponent<HexBlock>();
						neighborHEX[tokenIndex].blockState = BlockState.Dissolve;
						//neighborHEX[tokenIndex].blockType = BlockType.Empty;
						//neighborHEX[tokenIndex].ChangeBlockType();
						neighborHEX[tokenIndex].ChangeBlockState();
					}}

				GameManger.CURRENT_ACTIVE_BLOCK.blockState = BlockState.Dissolve;
				GameManger.CURRENT_ACTIVE_BLOCK.blockType  = BlockType.Empty;
				GameManger.CURRENT_ACTIVE_BLOCK.ChangeBlockType();
				GameManger.CURRENT_ACTIVE_BLOCK.ChangeBlockState();
				GameManger.CURRENT_ACTIVE_BLOCK  = null;

				GameManger.TOTAL_PINGS -= _ping;
				_ping = 0;
				GameManger.TOTAL_NULL_PINGS -= _nullPing ;
				_nullPing = 0;
				UpdateNeighbor();
				Messenger.Broadcast("Check for Empties");
			}
			break;

		}
	}

	public void ChangeBlockType(){
		switch (blockType){
		case BlockType.Blue :
			hexType.ChangeHexType(HexType.BlockType.Fire);
			renderer.material = hexType.blockMaterial;
				break;
		case BlockType.Red : 
			renderer.material.color = new Color(1, 0, 0.23f);
			break;
		case BlockType.Green	 :
			renderer.material.color = new Color(0, 1,0.23f);
			break;

		case BlockType.Empty : 
			hexType.ChangeHexType(HexType.BlockType.Empty);
			renderer.material = hexType.blockMaterial;

			break;

		}

	}

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
				Debug.Log ("Hit object at check point " + (index + 1 ) + " "+ hit.collider.name );
				neighborHEX[index] = hit.collider.gameObject.GetComponent<HexBlock>();

				if (neighborHEX[index].blockType == BlockType.Empty ){

					// if block one and to are compatible
					isJumpable = true;
					neighborHEX[index].moveIn = true;
					neighborHEX[index].tokenIndex = PointIndex(index);
					Debug.Log (" check two! done");

				}

			}
			
		}
		Debug.Log ("Index # " + index);
	}


	public void EmptyPing(){

		RaycastHit hit;
		
		for(int c = 0; c < checkPoints.Length; c++){
			
			Ray ray = new Ray (checkPoints[c].position,  checkPoints[c].forward);
			
			if (Physics.Raycast(ray , out hit,1)){
				if (hit.collider != null){ 
						//Debug.Log ("Hit object at check point " + (c + 1 ) + " "+ hit.collider.name );
						if(neighborHEX[c] == null){
							neighborHEX[c] = hit.collider.gameObject.GetComponent<HexBlock>();
						}

						if (neighborHEX[c].blockType != BlockType.Empty && neighborHEX[c].blockType != null  ){
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


	public void UpdateNeighbor(){
		// check block neighbors
		
		RaycastHit hit;
		
		for(int c = 0; c < checkPoints.Length; c++){
			
			Ray ray = new Ray (checkPoints[c].position,  checkPoints[c].forward);
			
			if (Physics.Raycast(ray , out hit,1)){
				if (hit.collider != null){ 
		
					if (neighborHEX[c].blockType == BlockType.Empty ){
						GameManger.TOTAL_PINGS -= neighborHEX[c]._ping ;
						neighborHEX[c]._ping = 0;
						GameManger.TOTAL_NULL_PINGS -= neighborHEX[c]._nullPing ;
						neighborHEX[c]._nullPing = 0;
						neighborHEX[c].EmptyPing();
						}
				}
			}}
	}


	void OnDisable(){
		
		Messenger.RemoveListener( "Check Neighbor", CheckNeighbor  );

	}
}
