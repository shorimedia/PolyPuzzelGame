using UnityEngine;
using System.Collections;
using PlayerPrefs = PreviewLabs.PlayerPrefs;

//
// Script Name: GameSave
//Script by: Victor L Josey
// Description: save and loads game data
// (c) 2015 Shoori Studios LLC  All rights reserved.


public class GameSave : MonoBehaviour {

	public void SaveData(){

		PlayerPrefs.SetString("Level" + GameManger.LEVEL_NUM + "_Time", GameTime.TEXT_TIME );
		PlayerPrefs.SetInt("Level" + GameManger.LEVEL_NUM + "_Time_in_seconds", GameTime.TIME_IN_SECONDS);
		PlayerPrefs.SetInt("Level" + GameManger.LEVEL_NUM + "_Score", GameManger.TOTAL_SCORE);
		PlayerPrefs.SetBool("Level" + GameManger.LEVEL_NUM + "_LeaderBoard", false);

		Debug.Log("Game Data Saved!");
        // save data
     //   PlayerPrefs.Flush();
	}





    public bool CheckHighScore(int level, int score)
    {
        bool NewScore = false;
        //load current level highest score
        int highScore = PlayerPrefs.GetInt("Level" + level + "_HighScore");

        //Check if player beat that current high score
        if (highScore < score)
        {
            PlayerPrefs.SetInt("Level" + level + "_HighScore", score);
            NewScore = true;
        }
        // save data
        PlayerPrefs.Flush();
        return NewScore;
    }

    public void RateLevel(int pegNumber, int level)
    {
        int starNum = 0;


         switch(pegNumber)
         {
             case 1: starNum = 3; break;
             case 2: starNum = 2; break;
             case 3: starNum = 1; break;

             default: //nothing
                 break;
         }

        //Check if this is the best rating of level
         if (PlayerPrefs.GetInt("Level" + level + "_Stars") < starNum)
        {
            PlayerPrefs.SetInt("Level" + level + "_Stars", starNum);
            PlayerPrefs.Flush();
            //return starNum;
        }
    }

// Save all data
	public void OnApplicationQuit()
	{
		PlayerPrefs.Flush();
	}

}
