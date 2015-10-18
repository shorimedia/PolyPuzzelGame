//GameManger
// Controls a games states
using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using PlayerPrefs = PreviewLabs.PlayerPrefs;

//
// Script Name: Game Manager
//Script by: Victor L Josey
// Description: Main Game State Machine
// (c) 2015 Shoori Studios LLC  All rights reserved.

public class GameManger : MonoBehaviour {

	public enum GameState
	{
		Win,
		Lose,
		OpenGame,
		CloseGame,
		Game,
		Pause

	}

	public GameState gameState = GameState.OpenGame;

	public static bool ACTIVE = false;
	public static PegStateMachine  CURRENT_ACTIVE_BLOCK; // The peg thats currently activly selected
	public static PegStateMachine  CURRENT_OPEN_BLOCK;

	//End Game tracking possivle move by player
	public static int CURRENT_NUM_EMPTY = 0;


	public static int BLOCK_COUNT;

	
	public static int TOTAL_POINTS_COUNT;
	public static int TOTAL_SCORE = 0;

	// Set up the level varibles
	public static int LEVEL_NUM = 1;
	public static int STAGE_NUM = 1;

	public int level;
	public int stage;
	public bool testMode = false;

    public GameSave gameSave;
	public Text pointText;

	public Text LevelTitle;

	public FadeScreen titleTrans;
	public FadeScreen  imageTrans;

	public FadeScreen WinScreen;

	public FadeScreen LoseScreen;

	public FadeScreen Background;

	public WinPopup winScript;
    public WinPopup loseScript;



	public PlatformManager pfManager;

	private GameTime timer; 

	public static bool CanTouch;

	void OnEnable()

	{	

		// reset or set move tracking data
		CURRENT_NUM_EMPTY = 0;

		// test any level for debug only
		if(testMode ==  true || PlayerPrefs.GetInt("Game Level") == 0 ) 
		{
			LEVEL_NUM = level;
			STAGE_NUM = stage;



		}else
		{
			//load Game stage and level
			LEVEL_NUM = PlayerPrefs.GetInt("Game Level");
			STAGE_NUM = PlayerPrefs.GetInt("Game Stage");

            #if DEBUG
			Debug.Log ("Set Level Number " + PlayerPrefs.GetInt("Game Level"));
            #endif

			// the values to show in inspector
			stage = STAGE_NUM;
			level = LEVEL_NUM;
		}

		TOTAL_POINTS_COUNT = 0;

		timer = this.GetComponent<GameTime>(); 

        #if DEBUG
		Debug.Log ("Level Number " + PlayerPrefs.GetInt("Game Level"));
        #endif
	}


	// Use this for initialization
	void Start () {

		ACTIVE = false;

	// Start Game
		ChangeGameState();
        Handheld.StopActivityIndicator();

	}


	public bool ShowActive;
	// Update is called once per frame
	 void Update () 
	{

		ShowActive = ACTIVE;
		if(testMode)
		{
		ChangeGameState();
			testMode = false;
		}


	}
	

	int NumberOfPlayableBlocks()
	{
		int pegCount = 0;
	
			switch(STAGE_NUM){
		case 1 : 
			pegCount = 61 - CURRENT_NUM_EMPTY;
			break;
		case 2: pegCount = 91 - CURRENT_NUM_EMPTY; break;
		case 3 : pegCount = 127 - CURRENT_NUM_EMPTY; break;
		case 4 : pegCount = 169 - CURRENT_NUM_EMPTY; break;
		default: pegCount = 61 - CURRENT_NUM_EMPTY; break;
		}


		return pegCount;
	}


	public int ScoreCalculator(){
        int subTotal = (TOTAL_POINTS_COUNT * 6 / GameTime.TIME_IN_SECONDS) + (TOTAL_POINTS_COUNT - (NumberOfPlayableBlocks() * 100)) + TOTAL_POINTS_COUNT;

        // Total should never be a negative number
        if (subTotal < 0)
        {
            subTotal = 10 * NumberOfPlayableBlocks();
        }

        return subTotal;
	}


	
	public void ChangeGameState(){

         int _nextLevel  =  GameManger.LEVEL_NUM + 1;
	
		switch(gameState){
		case GameState.Win:
			CanTouch = false;
			// show win screen with score, points and time
			Debug.Log("Winner!!!!!!");

          //  gameSave.CheckHighScore(LEVEL_NUM, TOTAL_SCORE);

            if (gameSave.CheckHighScore(LEVEL_NUM, TOTAL_SCORE))
            {
                winScript.winState = WinPopup.WinState.Best;
                
            }


            winScript.SetTxt(TOTAL_SCORE.ToString(), PlayerPrefs.GetInt("Level" + GameManger.LEVEL_NUM + "_HighScore").ToString(), GameTime.TEXT_TIME, GameTime.TEXT_TIME, NumberOfPlayableBlocks().ToString());


			if( TOTAL_SCORE > PlayerPrefs.GetInt("Level" + GameManger.LEVEL_NUM + "_Score") && GameTime.TIME_IN_SECONDS > PlayerPrefs.GetInt("Level" + GameManger.LEVEL_NUM + "_Time_in_seconds"))
            {

				SendMessage("SaveData");
			}

                winScript.SetStars(NumberOfPlayableBlocks());

                //Unlock next level
                PlayerPrefs.SetBool("Level" + _nextLevel + "_Lock", false);
                PlayerPrefs.SetInt("Game Level", _nextLevel);
			// Check leader board data

			timer.ToggleTimer = false;
			Background.IsOpen = true;
			WinScreen.IsOpen = true;
            Achievements.PegAchievements(NumberOfPlayableBlocks(), GameTime.TIME_IN_SECONDS);
            Achievements.FastTimeLeaderBoard(GameTime.TIME_IN_SECONDS);
            Achievements.HighScoreLeaderBoard(TOTAL_SCORE);

            PlayerPrefs.Flush();
			break;
		
		case GameState.Lose:
			CanTouch = false;
			// show loser screen
			Debug.Log("Loser!!!!!!");

            loseScript.SetPegNum(NumberOfPlayableBlocks());
			timer.ToggleTimer = false;
			Background.IsOpen = true;
			LoseScreen.IsOpen = true;
			break;

		case GameState.OpenGame: 


			CanTouch = false;

			// Check if game is the full, free, or demo
			if(pfManager.SetGameStatus(LEVEL_NUM))
			{

			LevelTitle.text = "Level " +  STAGE_NUM + " - " + LEVEL_NUM;
			timer.ToggleTimer = false;

			// display Level title
			titleTrans.IsOpen = true;
			// fade in from black
			imageTrans.StartCoroutine("DelayOpen", 3.3f);
			titleTrans.StartCoroutine("DelayClose", 3);

			// Start thet game after the title and image fades
			StartCoroutine("StartGame",3.4f);
			}else{
				timer.ToggleTimer = false;
				imageTrans.IsOpen = true;
			}


			break;

		case GameState.CloseGame: 
			//save game
			CanTouch = false;
			timer.ToggleTimer = false;
			//Fade out screen
			imageTrans.IsOpen = false;

			// opens next scene
			StartCoroutine("DelayNextScene", 2.3f);
			break;

		case GameState.Game: 
			// In game runtime
			
			timer.ToggleTimer = true;
			CanTouch = true;

			break;
		case GameState.Pause: 
			CanTouch = false;
			timer.ToggleTimer = false;

			break;
		}
	}


	public void SetGameState(string State)
	{
		switch(State)
		{
		case "Win":     gameState = GameState.Win; break;
		case "Lose":    gameState = GameState.Lose; break;
		case "Pause":   gameState = GameState.Pause; break;
		case "Game":    gameState = GameState.Game; break;

		}

		ChangeGameState();
	}


	// END OF GAME CONDITIONS
	public void EndGame()
	{
		TOTAL_SCORE = ScoreCalculator();
		
		if(NumberOfPlayableBlocks() > 5){
			gameState = GameState.Lose;
		}else{ gameState = GameState.Win;}

        gameSave.RateLevel(NumberOfPlayableBlocks(),LEVEL_NUM);
		ChangeGameState();
	}


	public void RestartGame()
	{

        PlayerPrefs.SetInt("Game Level", LEVEL_NUM);
        PlayerPrefs.Flush();

		//set to close state
		Application.LoadLevel(Application.loadedLevel);



	}

	public void NextLevel()
	{

		if( LEVEL_NUM == 30){
			STAGE_NUM ++;
			LEVEL_NUM = 1;
		}


		if( LEVEL_NUM <= 30){
			LEVEL_NUM ++;
		}

		//save level data
        gameSave.SaveData();
	
		// the values to show in inspector
		stage = STAGE_NUM;
		level = LEVEL_NUM;

	

		gameState = GameState.CloseGame;

		ChangeGameState();
	}



	public IEnumerator StartGame(float seconds)
	{
		//yield return new WaitForSeconds(seconds);

		yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(seconds));
		gameState = GameState.Game;
		ChangeGameState();
	}

	public IEnumerator DelayNextScene(float seconds)
	{
		//yield return new WaitForSeconds(seconds);
		
		yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(seconds));
		//Application.LoadLevel("Game_Menu");
		Application.LoadLevel(Application.loadedLevel);

	}

    public void OnApplicationQuit()
    {
        PlayerPrefs.Flush();
    }

    public void OnDisable()
    {
        GC.Collect();

      //  Debug.Log("GC Called");
    }

}
