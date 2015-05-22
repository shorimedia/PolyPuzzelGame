//GameManger
// Controls a games states
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PlayerPrefs = PreviewLabs.PlayerPrefs;

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



//	public int count;
//	public int countTwo;
//	public int countThree;

	public static int TOTAL_POINTS_COUNT;
	public static int TOTAL_SCORE = 0;

	// Set up the level varibles
	public static int LEVEL_NUM = 1;
	public static int STAGE_NUM = 1;

	public int level;
	public int stage;
	public bool testMode = false;
	public Text pointText;

	public Text LevelTitle;

	public FadeScreen titleTrans;
	public FadeScreen  imageTrans;

	public FadeScreen WinScreen;

	public FadeScreen LoseScreen;

	public FadeScreen Background;

	public WinPopup winScript;


	private GameTime timer; 

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
			Debug.Log ("Set Level Number " + PlayerPrefs.GetInt("Game Level"));

			// the values to show in inspector
			stage = STAGE_NUM;
			level = LEVEL_NUM;
		}

		TOTAL_POINTS_COUNT = 0;

		timer = this.GetComponent<GameTime>(); 
		Debug.Log ("Level Number " + PlayerPrefs.GetInt("Game Level"));
	}


	// Use this for initialization
	void Start () {

	// Start Game
		ChangeGameState();

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
		return (TOTAL_POINTS_COUNT * 6 / GameTime.TIME_IN_SECONDS ) + (TOTAL_POINTS_COUNT - (NumberOfPlayableBlocks() * 100)) + TOTAL_POINTS_COUNT;
	}


	
	public void ChangeGameState(){


	
		switch(gameState){
		case GameState.Win:
			// show win screen with score, points and time
			Debug.Log("Winner!!!!!!");



			winScript.SetTxt(TOTAL_SCORE.ToString(),TOTAL_SCORE.ToString(),GameTime.TEXT_TIME, GameTime.TEXT_TIME, NumberOfPlayableBlocks().ToString());


			if( TOTAL_SCORE > PlayerPrefs.GetInt("Level " + GameManger.LEVEL_NUM + "_Score") && GameTime.TIME_IN_SECONDS > PlayerPrefs.GetInt("Level " + GameManger.LEVEL_NUM + "_Time_in_seconds")){

				SendMessage("SaveData");
			}



			// Check leader board data


			// Set Win screen mode

//			winScript.winState = WinPopup.WinState.Normal;
//
//			winScript.winState = WinPopup.WinState.Best;
//
//			winScript.winState = WinPopup.WinState.Leader;




			timer.ToggleTimer = false;
			Background.IsOpen = true;
			WinScreen.IsOpen = true;

			break;
		
		case GameState.Lose: 
			// show loser screen
			Debug.Log("Loser!!!!!!");


			timer.ToggleTimer = false;
			Background.IsOpen = true;
			LoseScreen.IsOpen = true;
			break;

		case GameState.OpenGame: 
			LevelTitle.text = "Level " +  STAGE_NUM + " - " + LEVEL_NUM;
			timer.ToggleTimer = false;

			// display Level title
			titleTrans.IsOpen = true;
			// fade in from black
			imageTrans.StartCoroutine("DelayOpen", 3.3f);
			titleTrans.StartCoroutine("DelayClose", 3);

			// Start thet game after the title and image fades
			StartCoroutine("StartGame",3.4f);
			break;

		case GameState.CloseGame: 
			//save game

			timer.ToggleTimer = false;
			//Fade out screen
			imageTrans.IsOpen = false;

			// opens next scene
			StartCoroutine("DelayNextScene", 2.3f);
			break;

		case GameState.Game: 
			// In game runtime
			
			timer.ToggleTimer = true;
			break;
		case GameState.Pause: 

			timer.ToggleTimer = false;

			break;
		}
	}


	public void SetGameState(string State)
	{
		switch(State)
		{
		case "Win": gameState = GameState.Win; break;
		case "Lose": gameState = GameState.Lose; break;
		case "Pause":   gameState = GameState.Pause; break;
		case "Game":   gameState = GameState.Game; break;

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

		ChangeGameState();
	}


	public void RestartGame()
	{

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
		SaveGame();
	
		// the values to show in inspector
		stage = STAGE_NUM;
		level = LEVEL_NUM;

	

		gameState = GameState.CloseGame;

		ChangeGameState();
	}

	public void SaveGame()
	{
		// save game data

		PlayerPrefs.SetInt("Game Level", LEVEL_NUM);
		PlayerPrefs.SetInt("Game Stage", STAGE_NUM);
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





	
}
