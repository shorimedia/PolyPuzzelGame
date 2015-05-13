using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemAttachment : MonoBehaviour  {

	public bool ItemEquip = false;

	
	public Item AttachedItem;

//	private ItemBar itemBar;



	// Use this for initialization
	void Start () {
	
		// find the ItemBar script in the scene
//		itemBar = GameObject.Find("Item_Bar").GetComponent<ItemBar>();

	}

	
	public void ItemAttach(Item iAttach)
	{
		ItemEquip = true;
		// Add new data
		AttachedItem = iAttach;
		// set new item to read Peg data
		AttachedItem.PegObject = this.gameObject.GetComponent<PegStateMachine>();
		AttachedItem.Use();
	}

}
