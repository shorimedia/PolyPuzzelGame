using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemBar : MonoBehaviour {

	public int slotNum;
	public bool isHolderBar = false;
	public GameObject slotPrefab;
	public GameObject iconPrefab;

	private static GameObject hoverObject;
	public Canvas canvas;
	private static Slot from,to;
	public float hoverYOffset;
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
	
		//create item bars slots
		CreateItemBar();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonUp(0))
		{
			// Remove the seleced item from the inventory
			if(!eventSystem.IsPointerOverGameObject(-1) && from != null)
			   {
				from.GetComponent<Image>().color = Color.white;
				from.ClearSlot(); // remove item from slot
				Destroy(GameObject.Find ("Hover")); // remove icon

				//Reset objects
				to = null;
				from = null;
				hoverObject = null;
				emptySlots++;
			}


			Debug.Log (emptySlots);
		}


		//checks if hoverObject exists
		if( hoverObject != null)
		{
			Vector2 position;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out position);
			//Adds the offeset to the  position
			position.Set(position.x, position.y - hoverYOffset);
			//Set the hoverObject's psition
			hoverObject.transform.position = canvas.transform.TransformPoint(position);
		}
	}


	private void CreateItemBar()
	{
		slots = new List<GameObject>();

		if(!isHolderBar)
		{
		emptySlots = slotNum;
		}


		Debug.Log (emptySlots);

		// create a slot times slotNum amount
		for(int i = 0; i < slotNum; i++)
		{
			GameObject newSlot = (GameObject)Instantiate(slotPrefab, gameObject.transform.position, Quaternion.identity);
			newSlot.name = "Slot " + i;
			newSlot.transform.SetParent(this.gameObject.transform);

			// check if inventory is holder then set it's slot to hlder slots
			if( isHolderBar)
			{
				newSlot.GetComponent<Slot>().IsHolder = isHolderBar;
			}

			slots.Add(newSlot);
		}
	}

	public bool AddItem(Item item)
	{
		if(item.maxSize == 1)
		{
			PlaceEmpty(item);
			return true;
		}
		else
		{
			foreach(GameObject slot in slots)
			{
				Slot tmp = slot.GetComponent<Slot>();

				if(!tmp.IsEmpty)
				{
					if(tmp.CurrentItem.type == item.type && tmp.IsAvailable)
					{

						tmp.AddItem(item);
						return true;
					}
				}
			}

//			if (emptySlots > 0)
//			{
//				PlaceEmpty(item);
//			}

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
				from = clicked.GetComponent<Slot>();
				from.GetComponent<Image>().color = Color.gray;


				//Connect the icon to the mouse curse when move items
				hoverObject = (GameObject)Instantiate(iconPrefab);
				hoverObject.GetComponent<Image>().sprite = clicked.GetComponent<Image>().sprite;
				hoverObject.name = "Hover";

				RectTransform hoverTransform = hoverObject.GetComponent<RectTransform>();
				RectTransform clickedTransform = clicked.GetComponent<RectTransform>();

				hoverTransform.SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, clickedTransform.sizeDelta.x);
				hoverTransform.SetSizeWithCurrentAnchors( RectTransform.Axis.Vertical, clickedTransform.sizeDelta.y);

				hoverObject.transform.SetParent(GameObject.Find ("Canvas").transform,true); 
				hoverObject.transform.localScale = from.gameObject.transform.localScale;


			}
		}
		else if ( to == null)
		{
			to = clicked.GetComponent<Slot>();
			Destroy(GameObject.Find ("Hover"));
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
			hoverObject = null;
		}
	}

}
