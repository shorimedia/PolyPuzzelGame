using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PlayerPrefs = PreviewLabs.PlayerPrefs;

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
			levelCode.level.highScore = PlayerPrefs.GetInt(levelCode.level.LeveId + " Highscore");

			if(levelCode.LevelNumber == 1)
			{
			PlayerPrefs.SetBool("LevelId "+ stage + "" + levelCode.LevelNumber +" LockStatus", false);
			}

			if(levelCode.level.highScore == 0 && levelCode.LevelNumber > 1)
			{
				PlayerPrefs.SetBool("LevelId "+ stage + "" + levelCode.LevelNumber +" LockStatus", true);
			}

			levelCode.IsLock = PlayerPrefs.GetBool(levelCode.level.LeveId + " LockStatus");
			levelCode.level.starCount = PlayerPrefs.GetInt(levelCode.level.LeveId + " StarCount");
			levelCode.level.IsLeader = PlayerPrefs.GetBool(levelCode.level.LeveId + " Leader");
			levelCode.SetBtnImages();
		}



	}

}
