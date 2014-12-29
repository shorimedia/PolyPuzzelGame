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

	// Use this for initialization
	void Start () {
	
	}

	void CheckNeighbor(){
		// check block neighbors

		RaycastHit hit;

		for(int c = 0; c < checkPoints.Length; c++){

			Ray ray = new Ray (checkPoints[c].position,  checkPoints[c].forward);

		if (Physics.Raycast(ray , out hit,1)){
				if (hit.collider != null){ 
					Debug.Log ("Hit object at check point " + (c + 1 ) + " "+ hit.collider.name );
					neighborHEX[c] = hit.collider.gameObject.GetComponent<HexBlock>();

					if (neighborHEX[c].blockType != BlockType.Empty ){

						SecondNeighborCheck(c);

						Debug.Log (" check next");
					}
				}

			}
		}

	}


	void OnMouseOver() {

		if(blockState != BlockState.Active){
		blockState = BlockState.Hover;
		ChangeBlockState();
		}
	}

	void OnMouseExit() {
		if(blockState != BlockState.Active){
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
	

		if(blockState != BlockState.Active && GameManger.ACTIVE == false){
			blockState = BlockState.Active;
			ChangeBlockState();
		}else if(blockState == BlockState.Active){
			GameManger.ACTIVE = false;
			blockState = BlockState.Normal;
			ChangeBlockState();
			}


		Debug.Log("Drag ended!");
	}


	void ChangeBlockState(){

		switch (blockState){
		case BlockState.EmptyState : break;
		case BlockState.Active :
			GameManger.ACTIVE = true;

			CheckNeighbor();
			renderer.material.color = new Color(0, 1, 1);
			break;
		case BlockState.Normal : 
			renderer.material.color = new Color(1, 1, 1);
			break;
		case BlockState.Dissolve : break;
		case BlockState.Hover	:
			renderer.material.color = new Color(0.1F, 0, 0);
			break;
		case BlockState.Selected : break;
		case BlockState.Move : break;

		}
	}

	void ChangeBlockType(BlockType type){
		switch (type){
		case BlockType.Blue : break;
		case BlockType.Red : break;
		case BlockType.Green	 : break;
		case BlockType.Empty : break;

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




	void SecondNeighborCheck(int index){
		RaycastHit hit;

		Ray ray = new Ray (checkPoints[PointIndex(index)].position,  checkPoints[PointIndex(index)].forward);
		
		if (Physics.Raycast(ray , out hit,1)){
			if (hit.collider != null){ 
				Debug.Log ("Hit object at check point " + (PointIndex(index) + 1 ) + " "+ hit.collider.name );
				neighborHEX[PointIndex(index)] = hit.collider.gameObject.GetComponent<HexBlock>();

				if (neighborHEX[PointIndex(index)].blockType == BlockType.Empty ){

					// if block one and to are compatible
					isJumpable = true;
					neighborHEX[PointIndex(index)].moveIn = true;
					Debug.Log (" check two! done");
				}

			}
			
		}
		Debug.Log ("Index # " + PointIndex(index));

	}



}
