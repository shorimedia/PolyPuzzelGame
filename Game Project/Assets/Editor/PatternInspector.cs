using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Linq;
using System.Collections.Generic;

[CustomEditor(typeof(PatternManager))]
public class PatternInspector : Editor 

{
	private string name;
	private int levelNum;
	private int stageNum;
	private int[] pegEmptyNum;

	private bool showNewPattern;
	private bool showPatternList;


	private bool showStageListOne, showStageListTwo, showStageListThree, showStageListFour;

	private bool[] pegSpace = new bool[169];
	private string[] pegSpaceName = new string[169];

	public enum Stage
	{
		First,
		Second,
		Third,
		Fourth
	}

	public Stage stage =  Stage.First;
	public int pegCount = 61;

	
	private int Row;
	public int currentRow;
	private int MaxRow = 8;
	

	void OnEnable()
	{
		for(int i = 0; i < pegSpaceName.Length; i++)
		{
			pegSpaceName[i] = i.ToString();
			pegSpace[i] = false;
		}


	}

	int ResetRowCount()
	{
		float rowF = pegCount/MaxRow;
		Row = Mathf.RoundToInt(rowF) + 2;
		return Row;
	}

	int ArrayLimit(int num)
	{
		int newNum = MaxRow * num;
		return newNum;
	}



	public override void OnInspectorGUI()
	{
		GUILayout.Label ("This is a Label in a Custom Editor");
		if(GUILayout.Button("Sync",GUILayout.Height(30) ))
		{
			
		}
		GUILayout.Space(15);

		// area for new patterns creation
		showNewPattern = EditorGUILayout.Foldout(showNewPattern, "New Pattern");


		if(showNewPattern)
		{



			GUILayout.BeginHorizontal();
			stage = (Stage) EditorGUILayout.EnumPopup("Stage Number:", stage);
			if (GUILayout.Button("Set", GUILayout.Width(65)))
				SetStageNumber(stage);
			GUILayout.EndHorizontal();
			GUILayout.Space(20);
			//New pattern data display

		

			#region Row  
			for(int r = 1; r < ResetRowCount(); r++){
			GUILayout.BeginHorizontal();
				 
				currentRow = ArrayLimit(r) - MaxRow;




				for(int s = currentRow ; s < ArrayLimit(r); s++)
				{
	
					switch(pegSpace[s])
					{
					case true:  
						pegSpaceName[s]= "x";
						break;
						
					case false:
						pegSpaceName[s]= s.ToString();
						break;
					}

						if(GUILayout.Button(pegSpaceName[s],GUILayout.Height(40),GUILayout.Width(40) ))
						{

							if(pegSpace[s])
							{
								pegSpace[s] = false;
								
							pegSpaceName[s]= s.ToString();
							}else
							{
								pegSpace[s] = true;
								pegSpaceName[s]= "x";
							}


						Debug.Log(pegSpace[s]);
						}

			if (s == pegCount-1)
					{
						break;
					}


			}
			
			GUILayout.EndHorizontal();
			}
			#endregion

		

			GUILayout.Space(15);
			//Button for New patterns editor
			GUILayout.BeginHorizontal();
			if(GUILayout.Button("Create",GUILayout.Height(30) ))
			{
				XmlDataSave();
			}
			if(GUILayout.Button("Clear",GUILayout.Height(30) ))
			{
				
			}
			GUILayout.EndHorizontal();

		}

		GUILayout.Space(15);

		// Area for pattern list
		showPatternList =EditorGUILayout.Foldout(showPatternList,"Pattern List");

		if(showPatternList)
		{
			// Stage one area

			showStageListOne  = EditorGUILayout.Foldout(showStageListOne,"Stage 1 List");

			if(showStageListOne)
			{
			 
			GUILayout.Space(15);
			if(GUILayout.Button("Refresh",GUILayout.Height(30) ))
			{
				
			}
			}



		}
	}


	void XmlDataSave(Pattern newPattern)
	{
		XDocument xmlDoc = new XDocument(

			new XDeclaration("1.0","utf-8","yes"),

			new XComment("Pattern Saved Data"),

			new XElement("Patterns",

		             		new XElement("Pattern", new XAttribute("id", newPattern.Id),

		                    new XElement("Name",newPattern.PatternName),
		                    new XElement("LevelNumber",newPattern.LevelNumber),
		                    new XElement("StageNumber", newPattern.StageNumber),
		                    new XElement("PegEmpty", 

							            from peg in newPattern.PegEmptyNum
										select new XElement("PegNum", peg)
											))
		             ));
			xmlDoc.Save(@"\Asset\PatternData.xml");
	}



	void SetStageNumber(Stage set)
	{
		switch(set)
		{
			case Stage.First: pegCount = 61; break;
			case Stage.Second: pegCount = 91; break;
			case Stage.Third: pegCount = 127; break;
			case Stage.Fourth: pegCount = 169; break;
		}
	}
	

}
