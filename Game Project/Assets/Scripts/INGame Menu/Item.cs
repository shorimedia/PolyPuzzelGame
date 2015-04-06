using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public Sprite spriteNeutral;

	public int maxSize = 1;

	public float itemTime;

	public string type = "Item";

	public bool timeIsActive = true;

	// Use this for initialization
	void Start () {
	
	}

	public Item(Item item)
	{
		spriteNeutral = item.spriteNeutral;
		itemTime = item.itemTime;
		timeIsActive = item.timeIsActive;

	}


	
	public void Use()
	{

	}

}
