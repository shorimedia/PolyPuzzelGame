using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class PegBoardBilder : EditorWindow
{
	[MenuItem("Tools/Peg Board Builder")]
	 static void Init() 
	{
		PegBoardBilder window = (PegBoardBilder)EditorWindow.GetWindow (typeof (PegBoardBilder));
		window.Show();
	}

   public GameObject PegPrefab;

	private GameObject _StagePegs;

   public enum Stage
	{
		First,
		Second,
		Third,
		Fourth
	}

	private Stage stage;
	private int maxPegCount;

	public List<Transform> hexagonGridSpace;

	public static List<PegStateMachine> TokenDataTest = new List<PegStateMachine>();


	private bool canGenate;
	private string buttonName;
	
	void OnEnable()
	{


		// Check for stage object
		if(GameObject.Find("StagePegs: ") == null)
		{
				//Set Button to be able to generate pegs
				buttonName = "Generate";
				canGenate= true;

				// get peg prefab to create
				PegPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/PegPrefab/Peg.prefab") as GameObject;


				//Get the grid positions
				hexagonGridSpace  = new List<Transform>();

				GameObject go;
				
				for(int s = 0; s < 61; s++)
				{

				go = GameObject.Find("spaceToken " + s);
				   hexagonGridSpace.Add(go.transform);

					//Debug.Log (hexagonGridSpace[s].name);
				}


				//Debug.Log (PegPrefab.name);
		}else
		{
			//Set Button to be able to generate pegs

			_StagePegs = GameObject.Find("StagePegs: ");
			buttonName = "Clear";
			canGenate= false;

			// get peg prefab to create
			PegPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/PegPrefab/Peg.prefab") as GameObject;
			
			
			//Get the grid positions
			hexagonGridSpace  = new List<Transform>();
			
			GameObject go;
			
			for(int s = 0; s < 61; s++)
			{
				
				go = GameObject.Find("spaceToken " + s);
				hexagonGridSpace.Add(go.transform);
				
				//Debug.Log (hexagonGridSpace[s].name);
			}
		}

	}


	void OnGUI()
	{

		
		GUILayout.Space(10);
		EditorGUILayout.BeginHorizontal();

		stage = (Stage) EditorGUILayout.EnumPopup("Stage to create: " , stage);
		if (GUILayout.Button("Set: " + maxPegCount + " Pegs"))
			SetStage(stage);

		EditorGUILayout.EndHorizontal ();

		GUILayout.Space(10);



		if (GUILayout.Button(buttonName, GUILayout.Height(75)))
		{
			if(canGenate){
				Generator(maxPegCount); 
				canGenate= false;
				buttonName = "Clear";
			}else{
				Clear();
				buttonName = "Generate";
			}
		}


	}

	void SetStage(Stage set)
	{
		switch(set)
		{
		case Stage.First: maxPegCount = 61; break;
		case Stage.Second: maxPegCount = 91; break;
		case Stage.Third: maxPegCount = 127; break;
		case Stage.Fourth: maxPegCount = 169; break;
	}
	}



	void Generator(int hexAmount)
	{
		GameObject go;

		// Create a empty game object to hold peg
		_StagePegs = Instantiate(new GameObject(), new Vector3(hexagonGridSpace[0].position.x,hexagonGridSpace[0].position.y + 1 ,hexagonGridSpace[0].position.z), hexagonGridSpace[0].rotation) as GameObject;

		_StagePegs.name = "StagePegs: ";


		// Crreate Peg on the board
		for(int i = 0; i < hexAmount; i++)
		{
			go = Instantiate(PegPrefab, new Vector3(hexagonGridSpace[i].position.x,hexagonGridSpace[i].position.y + 1 ,hexagonGridSpace[i].position.z), hexagonGridSpace[i].rotation) as GameObject;

			go.name = "Peg: " + i;

			go.transform.parent = _StagePegs.transform;

			TokenDataTest.Add(go.GetComponent<PegStateMachine>());
						
			TokenDataTest[i].PegType.blockType = PegTypeMach.BlockType.Empty;
			TokenDataTest[i].PegType.ChangeBlockType();
		}


	}



	void Clear()
	{
		TokenDataTest.Clear();
		DestroyImmediate(_StagePegs);
		canGenate= true;
	}


}
