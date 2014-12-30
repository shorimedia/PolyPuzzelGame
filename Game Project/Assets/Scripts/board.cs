using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class board : MonoBehaviour {

	public GameObject hex;

	public Transform[] hexagonToken = new Transform[169];
	public List<HexBlock> TokenData = new List<HexBlock>();


	void Awake(){
		
		Messenger.AddListener( "Check for Empties", EmptyCheck );
		
		//Messenger.AddListener( "Clear Level", Clear );
	}



	// Use this for initialization
	void Start () {
		StartGame(1);
	}




	void StartGame (int LvNum) {

		switch(LvNum){
		case 1: 
			Generator(61);
			break;
		case 2: 
			Generator(91);
			break;
		case 3: 
			Generator(127);
			break;

		case 4: 
			Generator(169);
			break;
		}
	}
	



	void Grid(){






	}


	void Generator(int hexAmount){
		GameObject go;

		for(int i = 0; i < hexAmount; i++){

			go = Instantiate(hex, new Vector3(hexagonToken[i].position.x,hexagonToken[i].position.y + 1 ,hexagonToken[i].position.z), hexagonToken[i].rotation) as GameObject;
			TokenData.Add(go.GetComponent<HexBlock>());


			// set center token to empty
			if(i == 0){
				TokenData[i].blockType = HexBlock.BlockType.Empty;
				TokenData[i].ChangeBlockType();

			}
		}


		EmptyCheck();
		CheckMoves();
		TokenData[0].EmptyPing();
	}

	public void EmptyCheck(){
		GameManger.CURRENT_NUM_EMPTY = 0;

		for(int i = 0; i < TokenData.Count; i++){

			if (TokenData[i].blockType == HexBlock.BlockType.Empty){
				GameManger.CURRENT_NUM_EMPTY++;
			}
		}


	} 

	public void CheckMoves(){

		for(int i = 0; i < TokenData.Count; i++){
			
			if (TokenData[i].blockType != HexBlock.BlockType.Empty){
				TokenData[i].CheckNeighbor();
			}
		}
	}


	void RandomType(){

	}



	void OnDisable(){


		Messenger.RemoveListener( "Check for Empties", EmptyCheck );
		
		//Messenger.RemoveListener( "Clear Level", Clear );
	}
		
}
