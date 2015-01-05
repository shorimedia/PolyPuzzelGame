using UnityEngine;
using System.Collections;
using PlayerPrefs = PreviewLabs.PlayerPrefs;

public class GameSave : MonoBehaviour {

	public void SaveData(){

		PlayerPrefs.SetString("Level " + GameManger.LEVEL_NUM + "_Time", GameTime.TEXT_TIME );
		PlayerPrefs.SetInt("Level " + GameManger.LEVEL_NUM + "_Time_in_seconds", GameTime.TIME_IN_SECONDS);
		PlayerPrefs.SetInt("Level " + GameManger.LEVEL_NUM + "_Score", GameManger.TOTAL_SCORE);
		PlayerPrefs.SetBool("Level " + GameManger.LEVEL_NUM + "_LeaderBoard", false);

		Debug.Log("Game Data Saved!");
	}

//	// Save all data
//	public void OnApplicationQuit()
//	{
//		PlayerPrefs.Flush();
//	}

}
