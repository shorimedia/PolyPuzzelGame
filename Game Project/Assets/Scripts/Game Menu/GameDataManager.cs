using UnityEngine;
using System.Collections;
using PlayerPrefs = PreviewLabs.PlayerPrefs;

public class GameDataManager : MonoBehaviour {

	// set game data
//	public static int SET_STAGE_NUM;
//	public static int SET_LEVEL_NUM;
	
	private bool newGame = true;

	// Use this for initialization
	void Awake () {

		//newGame = PlayerPrefs.GetBool("New Game");

		if(newGame == null || newGame == true){
			Debug.Log("Saving to Game");
			PlayerPrefs.SetBool("New Game", false);

			for(int S = 2; S <= 4; S++){
			PlayerPrefs.SetBool("Stage Number " + S + " LockStatus", true );
			}
			PlayerPrefs.SetBool("Stage Number " + 1 + " LockStatus", false );
		}
	}
	

}
