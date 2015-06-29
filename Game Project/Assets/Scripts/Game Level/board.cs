using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class board : MonoBehaviour {

	public int[] patternList;

	public GameObject hex;
	
	public static List<PegStateMachine> TokenData = new List<PegStateMachine>();
	public LevelManager levelManager;



	private int ranNum;//  random use for random placement of starter blocks 
	private int[] mulitRanNum; // holds the random numbers for multi starter block levels



	
	// Use this for initialization
	void Start () {

		Debug.Log ("Create Game Boad");
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

		TokenData.Clear();

		levelManager.SetRandomNum(Random.Range(0f,1.0f), Random.Range(0f,1.0f));

	
		for(int i = 0; i < hexAmount; i++)
		{
			TokenData.Add(GameObject.Find("Peg: " + i).GetComponent<PegStateMachine>());
			bool set = true;


			// check if peg is empty via pattern
					for(int l = 0; l < patternList.Length; l++)
					{

						if( patternList[l] == i)
									{
											TokenData[i].PegType.blockType = PegTypeMach.BlockType.Empty;
											TokenData[i].PegType.ChangeBlockType();
											TokenData[i].moveIn = false;
											set = false;
											break;
									}
							}


			if(set)
				{
					levelManager.LevelTypeSelect(TokenData[i].PegType);
					TokenData[i].ChangeBlockState();
				}

		}



		Messenger.Broadcast("Check Empties");
	}


		
}
