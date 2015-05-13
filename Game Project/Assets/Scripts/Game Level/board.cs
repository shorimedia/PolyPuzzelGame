using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class board : MonoBehaviour {



	public GameObject hex;

	public Transform[] hexagonGridSpace = new Transform[169];
	public static List<PegStateMachine> TokenData = new List<PegStateMachine>();
	public LevelManager levelManager;



	private int ranNum;//  random use for random placement of starter blocks 
	private int[] mulitRanNum; // holds the random numbers for multi starter block levels



	
	// Use this for initialization
	void Start () {

		Debug.Log ("Board is Started");

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

		// clear list a new level starts so that only one instsist is connected
		TokenData.Clear();

		GameObject go;

		levelManager.SetRandomNum(Random.Range(0f,1.0f), Random.Range(0f,1.0f));

		for(int i = 0; i < hexAmount; i++){

			go = Instantiate(hex, new Vector3(hexagonGridSpace[i].position.x,hexagonGridSpace[i].position.y + 1 ,hexagonGridSpace[i].position.z), hexagonGridSpace[i].rotation) as GameObject;
			TokenData.Add(go.GetComponent<PegStateMachine>());

			levelManager.LevelTypeSelect(TokenData[i].PegType);

		}

		//setup empty block location

		// if empty space at random locations
		if( levelManager.randomLoc == true){
			mulitRanNum = new int[5];

			for(int i = 0; i < levelManager.startEmptyNum; i++){
				ranNum = Random.Range(0,GameManger.BLOCK_COUNT );
				TokenData[mulitRanNum[i]].PegType.blockType = PegTypeMach.BlockType.Empty;
				TokenData[mulitRanNum[i]].PegType.ChangeBlockType();
				mulitRanNum[i] = ranNum;
			}

		}else{    
			// if empty space are centered
			for(int i = 0; i < levelManager.startEmptyNum; i++){
				TokenData[i].PegType.blockType = PegTypeMach.BlockType.Empty;
				TokenData[i].PegType.ChangeBlockType();
			}
		
		}


		Messenger.Broadcast("Check Empties");
		//CheckMoves();


		// Set the neighbors of all the empty blocks
		if( levelManager.randomLoc == true){


			for(int i = 0; i < levelManager.startEmptyNum; i++){
				TokenData[ranNum].pUpdater.EmptyPing();
				TokenData[ranNum].moveIn = false;
			}
			
		}else{    

			// base pon the level number set the number of empty blocks
			for(int i = 0; i < levelManager.startEmptyNum; i++){
				TokenData[i].pUpdater.EmptyPing();
				TokenData[i].moveIn = false;
			}
			
		}

	}


		
}
