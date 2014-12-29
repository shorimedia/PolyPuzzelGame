//GameManger
// Controls a games states
using UnityEngine;
using System.Collections;

public class GameManger : MonoBehaviour {

	public static bool ACTIVE = false;
	public static HexBlock  CURRENTACTIVEBLOCK;
	public static int CURRENTNUMEMPTY = 0;


	public int count;

	public static int LevelNum = 1;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		count = CURRENTNUMEMPTY;
	}


	public void LoseGame(){

	}

	public void WinGame(){

	}

	public void CheckMoves(){

	}


}
