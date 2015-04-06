using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {

	public Item[] items;
	public ItemBar itemBar;

	public 	bool spawn = false;



	// Use this for initialization
	void Start () {
		//items = new Item();
	}
	

	void Update()
	{
		if(spawn)
		{
			SpawnItem();
			spawn = false;
		}

	}


	void SpawnItem() 
	{

		int randomIndex = Random.Range (0, items.Length);

		//itemBar.AddItem(items[randomIndex]);
		itemBar.AddItem(items[0]);
	
	}
}
