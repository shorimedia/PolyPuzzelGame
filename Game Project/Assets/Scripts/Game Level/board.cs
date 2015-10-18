using UnityEngine;
using System.Collections.Generic;

//
// Script Name: Board
//Script by: Victor L Josey
// Description: Setup Game board base on level rules and pattern data before the start of the game
// (c) 2015 Shoori Studios LLC  All rights reserved.


public class board : MonoBehaviour {

	public GameObject hex;
	
	public static List<PegStateMachine> TokenData = new List<PegStateMachine>();
	public LevelManager levelManager;

    public PatternManager pManager;
    public List<Pattern> levelPattern = new List<Pattern>();
    public Pattern currentPattern;

	private int ranNum;//  random use for random placement of starter blocks 
	private int[] mulitRanNum; // holds the random numbers for multi starter block levels


    //setting
	
	// Use this for initialization
	void Start ()
    {
		Debug.Log ("Create Game Boad");
        currentPattern = new Pattern();

		levelManager = new LevelManager(GameManger.LEVEL_NUM);
		StartGame(GameManger.STAGE_NUM);
	}



	//
	void StartGame (int stageNum) {

        // Setup game stage and peg count
		switch(stageNum){
		case 1: 
			GameManger.BLOCK_COUNT = 61;
                foreach (Pattern pat in pManager.StageOneList)
                {
                    if(pat.LevelNumber == GameManger.LEVEL_NUM)
                    {
                        levelPattern.Add(pat);
                    }
                
                }
                
			break;
		case 2: 
			GameManger.BLOCK_COUNT = 91;
            foreach (Pattern pat in pManager.StageTwoList)
            {
                if (pat.LevelNumber == GameManger.LEVEL_NUM)
                {
                    levelPattern.Add(pat);
                }

            }
			break;
		case 3: 
			GameManger.BLOCK_COUNT = 127;
            foreach (Pattern pat in pManager.StageThreeList)
            {
                if (pat.LevelNumber == GameManger.LEVEL_NUM)
                {
                    levelPattern.Add(pat);
                }

            }
			break;

		case 4: 
			GameManger.BLOCK_COUNT = 169;
            foreach (Pattern pat in pManager.StageFourList)
            {
                if (pat.LevelNumber == GameManger.LEVEL_NUM)
                {
                    levelPattern.Add(pat);
                }

            }
			break;
		}


        // Pick a random pattern in for the level and stage
        int ranf = Random.Range(0, levelPattern.Count);
        currentPattern = levelPattern[ranf];
        
        //Setup game board
		Generator(GameManger.BLOCK_COUNT);

	}


	void Generator(int hexAmount)
    {

		TokenData.Clear();

		levelManager.SetRandomNum(Random.Range(0f,1.0f), Random.Range(0f,1.0f));

	
		for(int i = 0; i < hexAmount; i++)
		{
			TokenData.Add(GameObject.Find("Peg: " + i).GetComponent<PegStateMachine>());
			bool set = true;


			// check if peg is empty via pattern
            for (int l = 0; l < currentPattern.PegEmptyNum.Count; l++)
					{

                        if (currentPattern.PegEmptyNum[l] == i)
									{
											TokenData[i].PegType.blockType = PegTypeMach.BlockType.Empty;
											TokenData[i].PegType.ChangeBlockType();
											TokenData[i].moveIn = false;
											set = false;
											break;
									}
					}

            // Only peg type if peg is not emptys set
			if(set)
				{
					levelManager.LevelTypeSelect(TokenData[i].PegType);
					TokenData[i].ChangeBlockState();
				}

		}

        SetTokenNeighbors(hexAmount);
        //Update empty peg count. Call emptycheck method in the EndGameCheck.cs
		Messenger.Broadcast("Check Empties");
	}

    private void SetTokenNeighbors(int hexAmount)
    {
       // Debug.Log("set token " + hexAmount);
		for(int i = 0; i < hexAmount; i++)
        {
            TokenData[i].pagState_pUpdater.SetNeighborPegs();
           // Debug.Log("Token " + i + "/" + hexAmount);
        }
    }
		
}
