using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemBar : MonoBehaviour {

	public static bool ItemSelectionMode = false;


	public int slotNum;
	public GameObject slotPrefab;
	public GameObject iconPrefab;
	
	public  ItemAttachment PegAttach;

	public static GameObject PegAttachObject;


	public Canvas canvas;
	public static Slot from,to;

	public EventSystem eventSystem;

	private List<GameObject> slots;
	
	private static int emptySlots;

	public static int EmptySlots

	{
		get{ return emptySlots;}
		set{ emptySlots = value;}
	}

	

	// Use this for initialization
	void Start () {
	
		Messenger.AddListener< bool >("Set Selection Mode", SetSelectionMode);

		//create item bars slots
		CreateItemBar();
	}


	public bool TestShowBool;
	// Update is called once per frame
	void Update () {

		// delete for production verse
		TestShowBool = ItemSelectionMode;

	}



// Call this function when play press the pegs. check if in item selection mode
	public void SetSelectionMode(bool mode)
	{
		if(mode)
		{
			// get the Peg attach script from the current selected peg
			PegAttach = PegAttachObject.GetComponent<ItemAttachment>();


		} else
		{
			//Exit Item selection mode and reset pegattach
			ItemSelectionMode = false;
			PegAttach= null;
		}

			// Remove the seleced item from the inventory
			if(!eventSystem.IsPointerOverGameObject(-1) && from != null && PegAttach == null)
			{
				
				from.GetComponent<Image>().color = Color.white;
				from.ClearSlot(); // remove item from slot

				
				//Reset objects
				to = null;
				from = null;

				emptySlots++;
			}else if (!eventSystem.IsPointerOverGameObject(-1) && from != null && PegAttach != null)
			{
				
				PegAttach.ItemAttach(from.Items.Peek());
				
				from.GetComponent<Image>().color = Color.white;
				from.ClearSlot(); // remove item from slot

				//Reset objects
				to = null;
				from = null;
				emptySlots++;
				//				Debug.Log("Did not Click on the UI " + eventSystem.name);
			}

	}




	private void CreateItemBar()
	{
		slots = new List<GameObject>();

		emptySlots = slotNum;
	
		Debug.Log (emptySlots);

		// create a slot times slotNum amount
		for(int i = 0; i < slotNum; i++)
		{
			GameObject newSlot = (GameObject)Instantiate(slotPrefab, gameObject.transform.position, Quaternion.identity);
			newSlot.name = "Slot " + i;
			newSlot.transform.SetParent(this.gameObject.transform);

			slots.Add(newSlot);
		}
	}

	public bool AddItem(Item item)
	{
		if(item.maxSize == 1)
		{
			PlaceEmpty(item);
			Debug.Log ("Place Item");
			return true;
		}


		return false;
	}


	private bool PlaceEmpty(Item item)
	{
		if(emptySlots > 0)
		{
			foreach( GameObject slot in slots)
			{
				Slot tmp = slot.GetComponent<Slot>();

				if (tmp.IsEmpty)
				{
					tmp.AddItem(item);
					Debug.Log ("Send to lot Item");
					emptySlots--;
					return true;
				}
			}
		}
		return false;
	}


	public void MoveItem(GameObject clicked)
	{
		if(from == null)
		{
			if(!clicked.GetComponent<Slot>().IsEmpty)
			{
				// Select an slot
				from = clicked.GetComponent<Slot>();
				// Set seleted slot color
				from.GetComponent<Image>().color = Color.gray;


			}
		}
		else if ( to == null)
		{
			to = clicked.GetComponent<Slot>();

		}
		 

		if( to != null && from != null)
		{
			Stack<Item> tmpTo = new Stack<Item>(to.Items);
			to.AddItems(from.Items);

			if(tmpTo.Count == 0)
			{
				from.ClearSlot();

			}
			else
			{
				from.AddItems(tmpTo);
			}

			from.GetComponent<Image>().color = Color.white;
			to = null;
			from = null;
		}
	}


	void OnDisable()
	{
		
		Messenger.AddListener< bool >("Set Selection Mode", SetSelectionMode);
	}

}
