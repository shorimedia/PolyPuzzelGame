using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class board : MonoBehaviour {



	public GameObject hex;

	public Transform[] hexagonGridSpace = new Transform[169];
	public List<HexBlock> TokenData = new List<HexBlock>();
	public LevelManager levelManager;



	private int ranNum;//  random use for random placement of starter blocks 
	private int[] mulitRanNum; // holds the random numbers for multi starter block levels

	void Awake(){
		Messenger.AddListener( "Check for Empties", EmptyCheck );

	}



	// Use this for initialization
	void Start () {
		levelManager = new LevelManager(GameManger.LEVEL_NUM);
		StartGame(GameManger.STAGE_NUM);
	}



	//
	void StartGame (int stageNum) {

		switch(stageNum){
		case 1: 
			GameManger.BLOCK_COUNT = 61;
			break;
		case 2: 
			GameManger.BLOCK_COUNT = 91;
			break;
		case 3: 
			GameManger.BLOCK_COUNT = 127;
			break;

		case 4: 
			GameManger.BLOCK_COUNT = 169;
			break;
		}

		Generator(GameManger.BLOCK_COUNT);

	}


	void Generator(int hexAmount){
		GameObject go;

		levelManager.SetRandomNum(Random.Range(0f,1.0f), Random.Range(0f,1.0f));

		for(int i = 0; i < hexAmount; i++){

			go = Instantiate(hex, new Vector3(hexagonGridSpace[i].position.x,hexagonGridSpace[i].position.y + 1 ,hexagonGridSpace[i].position.z), hexagonGridSpace[i].rotation) as GameObject;
			TokenData.Add(go.GetComponent<HexBlock>());

			levelManager.LevelTypeSelect(TokenData[i]);

		}

		//setup empty block location

		// if empty space at random locations
		if( levelManager.randomLoc == true){
			mulitRanNum = new int[5];

			for(int i = 0; i < levelManager.startEmptyNum; i++){
				ranNum = Random.Range(0,GameManger.BLOCK_COUNT );
				TokenData[mulitRanNum[i]].blockType = HexBlock.BlockType.Empty;
				TokenData[mulitRanNum[i]].ChangeBlockType();
				mulitRanNum[i] = ranNum;
			}

		}else{    
			// if empty space are centered
			for(int i = 0; i < levelManager.startEmptyNum; i++){
				TokenData[i].blockType = HexBlock.BlockType.Empty;
				TokenData[i].ChangeBlockType();
			}
		
		}


		EmptyCheck();
		CheckMoves();


		// Set the neighbors of all the empty blocks
		if( levelManager.randomLoc == true){


			for(int i = 0; i < levelManager.startEmptyNum; i++){
				TokenData[ranNum].EmptyPing();
				TokenData[ranNum].moveIn = false;
			}
			
		}else{    

			// base pon the level number set the number of empty blocks
			for(int i = 0; i < levelManager.startEmptyNum; i++){
				TokenData[i].EmptyPing();
				TokenData[i].moveIn = false;
			}
			
		}

	}

	// count the number of empty blocks in the scene
	public void EmptyCheck(){
		GameManger.CURRENT_NUM_EMPTY = 0;

		for(int i = 0; i < TokenData.Count; i++){

			if (TokenData[i].blockType == HexBlock.BlockType.Empty){
				GameManger.CURRENT_NUM_EMPTY++;
			}
		}


	} 

	// every block check their neighboring blocks
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
	}
		
}
