using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

//Shoori Studios LLC
//Description: Pattern base class for xml 

 [System.Serializable]
public class Pattern 
{
       [SerializeField]
	public string name;
       [SerializeField]
	public int levelNum;
	private int stageNum;

     [SerializeField]
	public List<int> pegEmptyNum = new List<int>();

     [SerializeField]
	public string id;
	
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


	public List<int> PegEmptyNum
	{
		get{ return pegEmptyNum;}
		set { PegEmptyNum = value; }
	}

}
