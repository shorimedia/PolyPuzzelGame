using UnityEngine;
using System.Collections;
using PlayerPrefs = PreviewLabs.PlayerPrefs;
//
// Script Name: Game Data Manager
//Script by: Victor L Josey
// Description: Used on the main menu screen to check if new game and set level save data
// (c) 2015 Shoori Studios LLC  All rights reserved.

public class GameDataManager : MonoBehaviour {

	// set game data
//	public static int SET_STAGE_NUM;
//	public static int SET_LEVEL_NUM;
	
	public bool newGame = true;

	// Use this for initialization
	void Awake () {


        CheckNewGame();
	}


    public void CheckNewGame()
        {

            newGame = PlayerPrefs.GetBool("New Game");

            if (newGame == true)
            {
                Debug.Log("Saving to Game");

                // if New game set stage one to true and all other stages to false
                for (int S = 2; S <= 4; S++)
                {
                    PlayerPrefs.SetBool("Stage Number " + S + " LockStatus", true);
                }

                PlayerPrefs.SetBool("Stage Number " + 1 + " LockStatus", false);

                //set levels
                for (int l = 1; l < 31; l++)
                {
                    PlayerPrefs.SetBool("Level" + l + "_Lock", true);

                }

                PlayerPrefs.SetBool("Level1_Lock", false);

                Debug.Log(Application.persistentDataPath);
                PlayerPrefs.SetBool("New Game", false);
                newGame = false;
                PlayerPrefs.Flush();
            }
    
    }




	void Start()
	{
		//Use when then scene is open from a pause state
		if(Time.timeScale  < 1)
		{
			Time.timeScale = 1;
			Debug.Log("Fix time scale!!!");
		}

	}



	public void CloseApp()
	{
		Application.CancelQuit();
	}



    public void OnApplicationQuit() 
    {
       PlayerPrefs.Flush();
    }


}
