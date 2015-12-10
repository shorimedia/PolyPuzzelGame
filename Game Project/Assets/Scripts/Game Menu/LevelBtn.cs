using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PlayerPrefs = PreviewLabs.PlayerPrefs;

//
// Script Name: LevelBtn
//Script by: Victor L Josey
// Description: 
// (c) 2015 Shoori Studios LLC  All rights reserved.

public class LevelBtn : MonoBehaviour {

	public Level level;
	public Text numberText;

	public Image[] Stars;
	public Image Leader;
	public Image Lock;



	private int levelNum;
	private int stageNum;


    public FadeScreen fadescreen;


	public int LevelNumber
	{
		get{ return levelNum;}
		set{ levelNum = value;}
	}

	public int StageNumber
	{
		get{ return stageNum;}
		set{ stageNum = value;}
	}

	public bool IsLock
	{
		get{ return level.IsLocked;}
		set{ level.IsLocked = value;}
	}





	public void SetLevelBtn()// create a new level and button text
	{
		level = new Level(levelNum,stageNum );
		numberText.text = levelNum.ToString();

	}




	public void StartGame()
	{

        if (IsLock == false)
        {

            fadescreen.IsOpen = false;

            PlayerPrefs.SetInt("Game Level", level.levelNum);
            PlayerPrefs.SetInt("Game Stage", level.stageNum);
            Debug.Log("Saving Game Level and Stage");

            StartCoroutine("StartLoading");
        }
	}


    IEnumerator StartLoading()
    {

#if UNITY_ANDROID
         Handheld.SetActivityIndicatorStyle(AndroidActivityIndicatorStyle.Large);
# endif

         Handheld.StartActivityIndicator();

        yield return new WaitForSeconds(0.97f);

        AsyncOperation async = Application.LoadLevelAsync(2);
        yield return async;
        //Debug.Log("Loading complete");
       
    }




	public void SetBtnImages() // the image to show stars or lock
	{

		Lock.enabled = level.IsLocked;

		Leader.enabled = level.IsLeader ;

		switch(level.starCount)
		{
		case 0:
			Stars[0].enabled = false;
			Stars[1].enabled = false;
			Stars[2].enabled = false;
			break;
		case 1:
			Stars[0].enabled = true;
			Stars[1].enabled = false;
			Stars[2].enabled = false;
			break;
		case 2: 
			Stars[0].enabled = true;
			Stars[1].enabled = true;
			Stars[2].enabled = false;
			break;
		case 3: 
			Stars[0].enabled = true;
			Stars[1].enabled = true;
			Stars[2].enabled = true;
			break;

		}


	}


}
