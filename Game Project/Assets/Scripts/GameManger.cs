//GameManger
// Controls a games states
using UnityEngine;
using System.Collections;

public class GameManger : MonoBehaviour {

	public static bool ACTIVE = false;
	public static HexBlock  CURRENT_ACTIVE_BLOCK;
	public static int CURRENT_NUM_EMPTY = 0;
	public static int TOTALPOSSIBLEMOVES = 0;

	public static int TOTAL_PINGS = 0; // When a empty have a non-empty side to it
	public static int TOTAL_NULL_PINGS = 0; // When a empty have a non-empty side to it


	public int count;
	public int countTwo;
	public int countThree;


	public static int LEVEL_NUM = 1;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		count = CURRENT_NUM_EMPTY;
		countTwo = TOTAL_PINGS;
		countThree = TOTAL_NULL_PINGS;

		CalculateMoves();
	}


	int NumberOfPlayableBlocks(){
		return 61 - CURRENT_NUM_EMPTY;
	}

	int TotalNulls(){
		return 54 - TOTAL_NULL_PINGS;
	}
	

	void CalculateMoves(){

		int MaxSpace = NumberOfPlayableBlocks() * 6; 


		int CurrentSpace = TOTAL_PINGS + TotalNulls();

		if(CurrentSpace == MaxSpace){
			Debug.LogError("End Of Game!!!!!!!!");
		}

	}


	public void LoseGame(){

	}

	public void WinGame(){

	}

	public void CheckMoves(){

	}


}
