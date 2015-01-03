//GameManger
// Controls a games states
using UnityEngine;
using System.Collections;

public class GameManger : MonoBehaviour {

	public enum GameState
	{
		Win,
		Lose,
		EndGame,
		Restart,
		SaveGame,
		NextLevel,
		Game,
		Pause

	}

	public GameState gameState = GameState.Game;

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

	public static int TOTAL_POINTS_COUNT = 0;
	public  int totalPoint;

	public static int TOTAL_SCORE = 0;


	public static int LEVEL_NUM = 1;
	public static int STAGE_NUM = 1;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		count = CURRENT_NUM_EMPTY;
//		countTwo = TOTAL_PINGS;
//		countThree = TOTAL_NULL_PINGS;

		totalPoint = TOTAL_POINTS_COUNT;

		CalculateMoves();
	}


	int NumberOfPlayableBlocks(){
	
			switch(STAGE_NUM){
		case 1 : return 61 - CURRENT_NUM_EMPTY; break;
		case 2: return 91 - CURRENT_NUM_EMPTY; break;
		case 3 : return 127 - CURRENT_NUM_EMPTY; break;
		case 4 : return 169 - CURRENT_NUM_EMPTY; break;
		default: return 61 - CURRENT_NUM_EMPTY; break;
		}
	}

	int TotalNulls(){

		switch(STAGE_NUM){
		case 1 : return 54 - TOTAL_NULL_PINGS; break;
		case 2: return  66 - TOTAL_NULL_PINGS;  break;
		case 3 : return  78 - TOTAL_NULL_PINGS;  break;
		case 4 : return  90 - TOTAL_NULL_PINGS;  break;
		default: return  54 - TOTAL_NULL_PINGS;  break;
		}

	}
	

	void CalculateMoves(){

		int MaxSpace = NumberOfPlayableBlocks() * 6; 


		int CurrentSpace = TOTAL_PINGS + TotalNulls();

		if(CurrentSpace == MaxSpace){
			gameState = GameState.EndGame;
			ChangeGameState();
			Debug.LogError("End Of Game!!!!!!!!    Score: " + TOTAL_SCORE);
		}

	}


	public int ScoreCalculator(){
		return (TOTAL_POINTS_COUNT * 6 / GameTime.TIME_IN_SECONDS ) + (TOTAL_POINTS_COUNT - (NumberOfPlayableBlocks() * 100)) + TOTAL_POINTS_COUNT;
	}


	
	public void ChangeGameState(){
		switch(gameState){
		case GameState.Win: break;
			// show win screen with score, points and time
			Debug.LogError("Winner!!!!!!");
		case GameState.Lose: 
			// show loser screen
			Debug.LogError("Loser!!!!!!");
			break;
		case GameState.EndGame: 
			TOTAL_SCORE = ScoreCalculator();

			if(NumberOfPlayableBlocks() > 5){
				gameState = GameState.Lose;
			} 
			break;
		case GameState.Restart: 
			// reset level
			//save level number and stage number
			//set reset to true
			Application.LoadLevel(Application.loadedLevel);

			break;
		case GameState.SaveGame: 
			// score, unlock, acheveiments,time, 
			break;
		case GameState.NextLevel:
			if( LEVEL_NUM <= 30){
				LEVEL_NUM ++;
			}
			break;
		case GameState.Game: 
			// do somthing
			break;
		case GameState.Pause: 
			// freeze time
			Time.timeScale = 0;
			//show pause screen
			break;
		}
	}


}
