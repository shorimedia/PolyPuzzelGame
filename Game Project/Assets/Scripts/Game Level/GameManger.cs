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
	public static HexBlock  CURRENT_ACTIVE_BLOCK;
	public static HexBlock  CURRENT_OPEN_BLOCK;

	//End Game tracking possivle move by player
	public static int CURRENT_NUM_EMPTY = 0;
	public static int TOTAL_PINGS = 0; // When a empty have a non-empty side to it
	public static int TOTAL_NULL_PINGS = 0; // When a empty have a non-empty side to it

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


	private GameTime timer; 

	void Awake()

	{	
		// test any level for debug only
		if(testMode ==  true){
			LEVEL_NUM = level;
			STAGE_NUM = stage;
		}else
		{
			//load Game stage and level
			LEVEL_NUM = PlayerPrefs.GetInt("Game Level");
			STAGE_NUM = PlayerPrefs.GetInt("Game Stage");



			// the values to show in inspector
			stage = STAGE_NUM;
			level = LEVEL_NUM;
		}

		TOTAL_POINTS_COUNT = 0;

		timer = this.GetComponent<GameTime>(); 
	}


	// Use this for initialization
	void Start () {
	

		ChangeGameState();

	}
	
	// Update is called once per frame
	 void Update () 
	{


//		count = CURRENT_NUM_EMPTY;
//		countTwo = TOTAL_PINGS;
//		countThree = TOTAL_NULL_PINGS;
		if(testMode)
		{
		ChangeGameState();
			testMode = false;
		}
		CalculateMoves();

		//only working the Gametime script
		//pointText.text = TOTAL_POINTS_COUNT.ToString();

	//	Debug.Log (TotalNulls());

	}


	int NumberOfPlayableBlocks()
	{
	
			switch(STAGE_NUM){
		case 1 : 
			return 61 - CURRENT_NUM_EMPTY;
			break;
		case 2: return 91 - CURRENT_NUM_EMPTY; break;
		case 3 : return 127 - CURRENT_NUM_EMPTY; break;
		case 4 : return 169 - CURRENT_NUM_EMPTY; break;
		default: return 61 - CURRENT_NUM_EMPTY; break;
		}
	}

	int TotalNulls()
	{

		switch(STAGE_NUM){
		case 1 : return 54 - TOTAL_NULL_PINGS; break;
		case 2: return  66 - TOTAL_NULL_PINGS;  break;
		case 3 : return  78 - TOTAL_NULL_PINGS;  break;
		case 4 : return  90 - TOTAL_NULL_PINGS;  break;
		default: return  54 - TOTAL_NULL_PINGS;  break;
		}

	}
	

	void CalculateMoves()
	{

		int MaxSpace = NumberOfPlayableBlocks() * 6; 


		int CurrentSpace = TOTAL_PINGS + TotalNulls();

		if(CurrentSpace == MaxSpace){

			Debug.Log("End Of Game!!!!!!!!    Score: " + TOTAL_SCORE);
			EndGame();
		}

	}


	public int ScoreCalculator(){
		return (TOTAL_POINTS_COUNT * 6 / GameTime.TIME_IN_SECONDS ) + (TOTAL_POINTS_COUNT - (NumberOfPlayableBlocks() * 100)) + TOTAL_POINTS_COUNT;
	}


	
	public void ChangeGameState(){


	
		switch(gameState){
		case GameState.Win:
			// show win screen with score, points and time
			Debug.Log("Winner!!!!!!");

			if( TOTAL_SCORE > PlayerPrefs.GetInt("Level " + GameManger.LEVEL_NUM + "_Score") && GameTime.TIME_IN_SECONDS > PlayerPrefs.GetInt("Level " + GameManger.LEVEL_NUM + "_Time_in_seconds")){

				SendMessage("SaveData");
			}

			timer.ToggleTimer = false;
			Background.IsOpen = true;
			WinScreen.IsOpen = true;

			break;
		
		case GameState.Lose: 
			// show loser screen
			Debug.LogError("Loser!!!!!!");
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
