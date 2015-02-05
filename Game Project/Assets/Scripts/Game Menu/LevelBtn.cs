using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PlayerPrefs = PreviewLabs.PlayerPrefs;

public class LevelBtn : MonoBehaviour {

	public Level level;
	public Text numberText;

	public Image[] Stars;
	public Image Leader;
	public Image Lock;



	private int levelNum;
	private int stageNum;




	// Use this for initialization
	void Start () {
	
		//level = new Level(levelNum,stageNum , );
	}

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

		PlayerPrefs.SetInt("Game Level", level.levelNum);
		PlayerPrefs.SetInt("Game Stage", level.stageNum);
		Debug.Log("Saving Game Level and Stage");
		Application.LoadLevel(1);
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
