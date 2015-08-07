using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Linq;
using System.Collections.Generic;

//
// Script Name: PatternInspector
//Script by: Victor L Josey
// Description: Inspector for pattern manager and save patter to xml file.
// (c) 2015 Shoori Studios LLC  All rights reserved.


[CustomEditor(typeof(PatternManager))]
public class PatternInspector : Editor 

{
    public string dataFilePath = Application.streamingAssetsPath + "/Xml Assets/PatternData.xml";

    public string deletePatternname;
	private string name;
	private int levelNum;
	private int stageNum;
	private int[] pegEmptyNum;

	private bool showNewPattern;
	private bool showPatternList;


    public List<Pattern> stageOnePatterns, stageTwoPatterns, stageThreePatterns, stageFourPatterns;


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


	private List<int> newPegData = new List<int>();

    XmlDocument doc;

	void OnEnable()
	{
		for(int i = 0; i < pegSpaceName.Length; i++)
		{
			pegSpaceName[i] = i.ToString();
			pegSpace[i] = false;
		}

        xmlDoc = XDocument.Load(@dataFilePath);
        Debug.Log("Load Xml: " + xmlDoc);

        
       
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


    XDocument xmlDoc;


	public override void OnInspectorGUI()
	{
        PatternManager patternScript = (PatternManager)target;


        


		GUILayout.Label ("This is a Label in a Custom Editor");

		if(GUILayout.Button("Sync",GUILayout.Height(30) ))
		{
            XmlPatternLoad();

            patternScript.StageOneList = stageOnePatterns;
            patternScript.StageTwoList = stageTwoPatterns;
            patternScript.StageThreeList = stageThreePatterns;
            patternScript.StageFourList = stageFourPatterns;

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

			name = EditorGUILayout.TextField("Pattern Name: ", name);
			levelNum = EditorGUILayout.IntField("Level Number:", levelNum);
			stageNum = EditorGUILayout.IntField("Stage Number:", stageNum);
			GUILayout.Space(10);

            // Grid of button that repersents Peg in board to mark for patterns
			#region Button grid 
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

			GUI.backgroundColor =  Color.grey;
			if(GUILayout.Button("Create",GUILayout.Height(30)))
			{
				if(name != null && stageNum != 0 && levelNum != 0)
				{
				for(int p = 0; p < pegCount; p++)
				{
					if(pegSpace[p] == true)
						newPegData.Add(p);
				}

				Pattern newPattern = new Pattern(name,levelNum,stageNum);

				newPattern.Id = CreateId(name,levelNum,stageNum);

				for(int d = 0; d < newPegData.Count; d++)
				{

					newPattern.pegEmptyNum.Add(newPegData[d]);
				}

				XmlDataSave(newPattern);
					name = null;
					levelNum = 0;
					stageNum = 0;

					for(int i = 0; i < pegSpaceName.Length; i++)
					{
						pegSpaceName[i] = i.ToString();
						pegSpace[i] = false;
					}

				}}

            //Clear check mark on grid
			if(GUILayout.Button("Clear",GUILayout.Height(30) ))
			{
				name = null;
				levelNum = 0;
				stageNum = 0;

				for(int i = 0; i < pegSpaceName.Length; i++)
				{
					pegSpaceName[i] = i.ToString();
					pegSpace[i] = false;
				}

			}
			GUILayout.EndHorizontal();

		}

		GUILayout.Space(15);

		// Area for pattern list
		showPatternList =EditorGUILayout.Foldout(showPatternList,"Pattern List");

		if(showPatternList)
		{

           DrawDefaultInspector();

		}
        GUILayout.Space(15);

        GUILayout.BeginHorizontal();
        deletePatternname = EditorGUILayout.TextField("Pattern Name", deletePatternname);

        if (GUILayout.Button("Delete Pattern"))
        {
            doc = new XmlDocument();
            doc.Load(dataFilePath);

            foreach(XmlNode xNode in doc.SelectNodes("Patterns/Pattern"))
            {
                if (xNode.SelectSingleNode("Name").InnerText == deletePatternname)
                {
                    xNode.ParentNode.RemoveChild(xNode);
                    doc.Save(dataFilePath);
                    deletePatternname = " ";
                }
               

            }
           

        }
        GUILayout.EndHorizontal();
        

	}

    


    // Call when adding a new pattern to xml file
	void XmlDataSave(Pattern newPattern)
	{

        // If xml file is not found create a one 
        if (xmlDoc == null)
        {

            xmlDoc = new XDocument(

            new XDeclaration("1.0", "utf-8", "yes"),

            new XComment("Pattern Saved Data"),

            new XElement("Patterns"));

            xmlDoc.Save(@dataFilePath);
        }
        else
        
        {
           XElement newPat = new XElement("Pattern", new XAttribute("id", newPattern.Id),

            new XElement("Name", newPattern.PatternName),
            new XElement("LevelNumber", newPattern.LevelNumber),
            new XElement("StageNumber", newPattern.StageNumber),
            new XElement("PegEmpty",

                        from peg in newPattern.PegEmptyNum
                        select new XElement("PegNum", peg)
                            ));

           xmlDoc.Root.Add(newPat);
           xmlDoc.Save(@dataFilePath);
           Debug.Log("Save Xml: " + xmlDoc);
        }



	}


	string CreateId(string name, int level, int stage)
	{
		string IDNum;
		int ran1 = Random.Range(1,100);
		int ran2 = Random.Range(1,1001);

		IDNum = name[0] + name[0] + name[0]  + level.ToString() + stage.ToString() + ran1.ToString() +ran2.ToString();

		return IDNum;
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

    void XmlPatternLoad()
    {

       // reset list  to add new list data

        // clear now list
        //stageOnePatterns.Clear(); stageTwoPatterns.Clear(); stageThreePatterns.Clear(); stageFourPatterns.Clear();

        // Set new list
        stageOnePatterns = new List<Pattern>();
        stageTwoPatterns = new List<Pattern>();
        stageThreePatterns = new List<Pattern>();
        stageFourPatterns = new List<Pattern>();

        // Load Xml file
         doc  = new XmlDocument();
        doc.Load(dataFilePath);

        // Get Pattern data from Xml file and seperate by stages
        foreach(XmlNode node in doc.DocumentElement)
        {

            Pattern newP = new Pattern();

            newP.Id = node.Attributes["id"].Value;
            newP.PatternName = node.SelectSingleNode("Name").InnerText;
            // Debug.Log(node["Name"].Value);
            Debug.Log(node.SelectSingleNode("Name").InnerText);

            newP.LevelNumber = int.Parse(node.SelectSingleNode("LevelNumber").InnerText);
            newP.StageNumber = int.Parse(node.SelectSingleNode("StageNumber").InnerText);
         
            // Get peg numbers with the file

             XmlNode xmlLst = node.SelectSingleNode("PegEmpty");

             foreach (XmlNode xnode in xmlLst.ChildNodes)
            {
                newP.PegEmptyNum.Add(int.Parse(xnode.InnerText));
            }
 
            // Add pattern to list base on the stage number
            switch(newP.StageNumber)
            {
                case 1:
                    stageOnePatterns.Add(newP);
                    break;
                case 2:
                    stageTwoPatterns.Add(newP);
                    break;
                case 3:
                    stageThreePatterns.Add(newP);
                    break;
                case 4:
                    stageFourPatterns.Add(newP);
                    break;
            }


        }


        Debug.Log(stageOnePatterns.Count);
        Debug.Log(stageOnePatterns[0].PatternName);
        Debug.Log(stageOnePatterns[0].Id);
        Debug.Log(stageOnePatterns[0].pegEmptyNum[7]);

    }



}
