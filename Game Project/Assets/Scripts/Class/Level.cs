using UnityEngine;
using System.Collections;

[System.Serializable]
public class Level {

	public string LeveId;
	public bool IsLocked = true;
	public bool IsLeader = false;
	public int 	starCount = 0; // max of three
	public int levelNum;
	public int stageNum;
	public int highScore = 0;

	public Level(int LNum, int SNum)
	{
		levelNum =	LNum;
		stageNum = 	SNum;

		LeveId  = "LevelId " + stageNum + "" + levelNum;
	}



	public string LevelIdNumber
	{
		get{return LeveId;}
	}

}
