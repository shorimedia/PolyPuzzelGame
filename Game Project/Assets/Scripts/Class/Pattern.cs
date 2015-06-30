using UnityEngine;
using System.Collections;

public class Pattern 
{

	private string name;
	private int levelNum;
	private int stageNum;
	private int[] pegEmptyNum;

	private string id;
	
	public Pattern ()
	{

	}
	
	public Pattern(string N, int L, int S)
	{
		name = N;
		levelNum = L;
		stageNum = S;
	}

public string Id
	{
		get{return id;}
		set{id = value;}
	}


	
	public string PatternName
	{
		get{ return name;}
		set{ name = value;}
	}

	public int LevelNumber
	{
		get { return levelNum;}
		set { levelNum = value;}
	}

	public int StageNumber
	{
		get {return stageNum;}
		set {stageNum = value;}
	}


	public int[] PegEmptyNum
	{
		get{ return pegEmptyNum;}
		set { PegEmptyNum = value; }
	}

}
