using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {

	
	public Item[] itemsCommon;
	public Item[] itemsUncommon;
	public Item[] itemsRare;
	public Item[] itemsVeryRare;

	public ItemBar itemBar;

	public 	bool spawn = false;


	public int TestIndex = 0;
	public string TestType = "Common";




	

	// Use this for initialization
	void Start () {
		//items = new Item();
	
		Object[] commonObject = Resources.LoadAll("ItemsPrefabs/Common/");
		Object[] uncommonObject = Resources.LoadAll("ItemsPrefabs/Uncommon/");
		Object[] rareObject = Resources.LoadAll("ItemsPrefabs/Rare/");
		Object[] veryRareObject = Resources.LoadAll("ItemsPrefabs/VeryRare/");

		GameObject[] go; 

		// Set Array sizes for each type
		itemsCommon = new Item[commonObject.Length];
		itemsUncommon = new Item[uncommonObject.Length];
		itemsRare = new Item[rareObject.Length];
		itemsVeryRare = new Item[veryRareObject.Length];


#region	  Common Items loading
		go = new GameObject[commonObject.Length];

		for(int i = 0; i < commonObject.Length; i++)
		{

			go[i] = (GameObject)commonObject[i];

			itemsCommon[i] = go[i].GetComponent<Item>();

		}

		Debug.Log(commonObject.Length + " Common Items loaded");
#endregion


#region	  Uncommon Items loading
		go = new GameObject[uncommonObject.Length];
		
		for(int i = 0; i < uncommonObject.Length; i++)
		{
			
			go[i] = (GameObject)uncommonObject[i];
			
			itemsUncommon[i] = go[i].GetComponent<Item>();
			
		}
		
		Debug.Log(uncommonObject.Length + " Uncommon Items loaded");
		#endregion


#region	  Rare Items loading
		go = new GameObject[rareObject.Length];
		
		for(int i = 0; i < rareObject.Length; i++)
		{
			
			go[i] = (GameObject)rareObject[i];
			
			itemsRare[i] = go[i].GetComponent<Item>();
		}
		
		Debug.Log(rareObject.Length + " Rare Items loaded");
		#endregion


#region	   Very Rare Items loading
		go = new GameObject[veryRareObject.Length];
		
		for(int i = 0; i < veryRareObject.Length; i++)
		{
			
			go[i] = (GameObject)veryRareObject[i];
			
			itemsVeryRare[i] = go[i].GetComponent<Item>();
		}
		
		Debug.Log(veryRareObject.Length + " Very Rare Items loaded");
		#endregion



	}
	

	void Update()
	{
		if(spawn)
		{
			TestItem(TestIndex, TestType);
			SpawnItem();
			spawn = false;
		}

	}



	int DeterminLevelBraket(int numLevel)
	{

		int LevelBraket = 0;

		if (numLevel >= 1 && numLevel <= 6)
		{
			LevelBraket = 1;
		}else 		

			if (numLevel >= 7 && numLevel <= 12)
		{
			LevelBraket = 2;
		}else

			if (numLevel >= 13 && numLevel <= 18)
		{
			LevelBraket = 3;
		}else

			if (numLevel >= 19 && numLevel <= 24)
		{
			LevelBraket = 4;
		}else
			if (numLevel >= 25 && numLevel <= 30)
		{
			LevelBraket =5;
		}

		return LevelBraket;
	}


	#region  Spawner
	public void SpawnItem() 
	{
		// rarity on the items
		float random = Random.Range (0f,1f);
		int randomIndex;


		//
		switch(DeterminLevelBraket(GameManger.LEVEL_NUM))
		{
		case 1 :
			if(random >= 0f && random <= 0.70f )
			{
				randomIndex = Random.Range(0,itemsCommon.Length);
				itemBar.AddItem(itemsCommon[randomIndex]);

			}else if(random >= 0.71f && random <= 0.92f )
			{
			
				randomIndex = Random.Range(0,itemsUncommon.Length);
				itemBar.AddItem(itemsUncommon[randomIndex]);

			}else if(random >= 0.92f && random <= 0.97f )
			{
				randomIndex = Random.Range(0,itemsRare.Length);
				itemBar.AddItem(itemsRare[randomIndex]);

			}else if(random >= 0.98f && random <= 1f )
			{
				randomIndex = Random.Range(0,itemsVeryRare.Length);
				itemBar.AddItem(itemsVeryRare[randomIndex]);
			}

			break;
		case 2 : 


			if(random >= 0f && random <= 0.60f )
			{
				randomIndex = Random.Range(0,itemsCommon.Length);
				itemBar.AddItem(itemsCommon[randomIndex]);
				
			}else if(random >= 0.61f && random <= 0.90f )
			{
				
				randomIndex = Random.Range(0,itemsUncommon.Length);
				itemBar.AddItem(itemsUncommon[randomIndex]);
				
			}else if(random >= 0.91f && random <= 0.97f )
			{
				randomIndex = Random.Range(0,itemsRare.Length);
				itemBar.AddItem(itemsRare[randomIndex]);
				
			}else if(random >= 0.98f && random <= 1f )
			{
				randomIndex = Random.Range(0,itemsVeryRare.Length);
				itemBar.AddItem(itemsVeryRare[randomIndex]);
			}



			break;
		case 3 :

			if(random >= 0f && random <= 0.60f )
			{
				randomIndex = Random.Range(0,itemsCommon.Length);
				itemBar.AddItem(itemsCommon[randomIndex]);
				
			}else if(random >= 0.61f && random <= 0.89f )
			{
				
				randomIndex = Random.Range(0,itemsUncommon.Length);
				itemBar.AddItem(itemsUncommon[randomIndex]);
				
			}else if(random >= 0.90f && random <= 0.96f )
			{
				randomIndex = Random.Range(0,itemsRare.Length);
				itemBar.AddItem(itemsRare[randomIndex]);
				
			}else if(random >= 0.97f && random <= 1f )
			{
				randomIndex = Random.Range(0,itemsVeryRare.Length);
				itemBar.AddItem(itemsVeryRare[randomIndex]);
			}

			break;
		case 4 :

			if(random >= 0f && random <= 0.50f )
			{
				randomIndex = Random.Range(0,itemsCommon.Length);
				itemBar.AddItem(itemsCommon[randomIndex]);
				
			}else if(random >= 0.51f && random <= 0.89f )
			{
				
				randomIndex = Random.Range(0,itemsUncommon.Length);
				itemBar.AddItem(itemsUncommon[randomIndex]);
				
			}else if(random >= 0.90f && random <= 0.96f )
			{
				randomIndex = Random.Range(0,itemsRare.Length);
				itemBar.AddItem(itemsRare[randomIndex]);
				
			}else if(random >= 0.97f && random <= 1f )
			{
				randomIndex = Random.Range(0,itemsVeryRare.Length);
				itemBar.AddItem(itemsVeryRare[randomIndex]);
			}

			break;
		case 5 :


			if(random >= 0f && random <= 0.50f )
			{
				randomIndex = Random.Range(0,itemsCommon.Length);
				itemBar.AddItem(itemsCommon[randomIndex]);
				
			}else if(random >= 0.51f && random <= 0.80f )
			{
				
				randomIndex = Random.Range(0,itemsUncommon.Length);
				itemBar.AddItem(itemsUncommon[randomIndex]);
				
			}else if(random >= 0.81f && random <= 0.95f )
			{
				randomIndex = Random.Range(0,itemsRare.Length);
				itemBar.AddItem(itemsRare[randomIndex]);
				
			}else if(random >= 0.96f && random <= 1f )
			{
				randomIndex = Random.Range(0,itemsVeryRare.Length);
				itemBar.AddItem(itemsVeryRare[randomIndex]);
			}

			break;
		}


		Debug.Log ("new Item");
	
	}
	#endregion


	void TestItem(int index,string type)
	{


		switch(type)
		{

		case "Common": 
			itemBar.AddItem(itemsCommon[index]);
			break;
		case "Uncommon": 
			itemBar.AddItem(itemsUncommon[index]);
			break;
		case "Rare":
			itemBar.AddItem(itemsRare[index]);
			break;
		case "VeryRare": 

			itemBar.AddItem(itemsVeryRare[index]);
			break;

		}

	}

}
