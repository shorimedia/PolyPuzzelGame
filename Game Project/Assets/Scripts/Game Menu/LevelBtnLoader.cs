using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PlayerPrefs = PreviewLabs.PlayerPrefs;

//
// Script Name: LevelBtnLoader
//Script by: Victor L Josey
// Description: 
// (c) 2015 Shoori Studios LLC  All rights reserved.

public class LevelBtnLoader : MonoBehaviour {

	//public GameObject levelBtn;
	public GameObject[] LevelBtns;
	

	public int NumberOfLevels = 30;


	void Awake()
	{

	}


	public void LoadButtons(int stage)
	{
		//LevelBtns = new GameObject[30];

		for(int b = 0; b < NumberOfLevels; b++)
		{
//			GameObject Btn = (GameObject)Instantiate(levelBtn);
//			Btn.transform.parent = this.transform;
//			LevelBtns[b] = Btn;
			LevelBtn levelCode = LevelBtns[b].GetComponent<LevelBtn>();
			levelCode.LevelNumber = b + 1;
			levelCode.StageNumber = stage;

			levelCode.SetLevelBtn();
			//levelCode.level.highScore = PlayerPrefs.GetInt(levelCode.level.LeveId + " Highscore");

			//if(levelCode.LevelNumber == 1)
			//{
             //   PlayerPrefs.SetBool("Level " + levelCode.LevelNumber + " _Lock", false);
			//}

            //Setting the levels
            levelCode.IsLock = PlayerPrefs.GetBool("Level" + levelCode.LevelNumber + "_Lock");
            levelCode.level.starCount = PlayerPrefs.GetInt("Level" + levelCode.LevelNumber + "_Stars");
            levelCode.level.IsLeader = PlayerPrefs.GetBool("Level" + levelCode.LevelNumber + "_LeaderBoard");
			levelCode.SetBtnImages();
		}



	}

}
