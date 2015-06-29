using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
	public GameObject timeBarObject; // Slide UI object

	public float maxTime;

	// slider object's slider component
	private Slider timeBar;

	// use to set timebar's alpha
	private CanvasGroup timeBarCanvasGroup;

	public CanvasGroup TimeBarCanvas
	{
		get{return timeBarCanvasGroup;}
		set{ timeBarCanvasGroup = value;}
	}

	public Sprite slotEmpty;

	private Stack<Item> items;

	public Stack<Item> Items
	{
		get { return items;}
		set{items = value; }
	}
	
	public bool IsEmpty
	{
		get{ return items.Count == 0;}
	}
	
	public bool IsAvailable
	{
		get{ return CurrentItem.maxSize > items.Count;}
	
	}

	public Item CurrentItem
	{
		get{ return items.Peek();}
	}


	public SlotAudio slotAudio;

	void Awake () 
	{
		//  get the timebar's slider and canvasgroup for this slot 
		timeBar = timeBarObject.GetComponent<Slider>();
		timeBarCanvasGroup = timeBarObject.GetComponent<CanvasGroup>();
	}

	// Use this for initialization
	void Start () 
	{

		Messenger.AddListener<float>("Set MaxTime", SetMaxTime);

		timeBar.maxValue = maxTime; // Set max value of timebar
		timeBar.value = maxTime;	// set the timebar  value to max

		items = new Stack<Item>();
		//RectTransform slotRec = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {

		if(items.Count != 0 && items.Peek().itemTime <= 0f )
		{


			if(ItemBar.ItemSelectionMode == true && ItemBar.from  != this.gameObject.GetComponent<Slot>())
			{
				OutOfTime();
			}else if(ItemBar.ItemSelectionMode == false)
			{
				OutOfTime();
			}
		
			Debug.Log ( gameObject.name + " has clear item due to time limitations");
		}

	}



	// Used when adding new items
	 public void AddItem(Item item)
	{
		slotAudio.StartPlay("Popup");
		items.Push(item);
		Debug.Log ("push Item");

		item.itemTime = maxTime;     // When a new item is created set its timer to max

		item.timeIsActive = true;

		StartCoroutine(CountDownTime(item)); // start count down


		timeBarCanvasGroup.alpha = 1;


		ChangeSprite(item.spriteNeutral);
	}


	// Use when item change slot location
	public void AddItems(Stack<Item> items)
	{
		this.items = new Stack<Item>(items);

		// transfer timeBar time data to this slot
		this.items.Peek().itemTime = items.Peek().itemTime;   // Make sure that the new copy of the item values matches old item
		this.items.Peek().timeIsActive = items.Peek().timeIsActive;
		timeBar.value = this.items.Peek().itemTime;				// Set slot's timeBar value to the Item's.


			timeBarCanvasGroup.alpha = 1;
			// don't restart time is on holder's slot

	
		ChangeSprite(CurrentItem.spriteNeutral); // change sprite to item's sprite.
	}

	// Change the Sprite on slot Image component
	private void ChangeSprite(Sprite neutral)
	{
		GetComponent<Image>().sprite = neutral;

	}
	

	// Count down timer for each item
	IEnumerator CountDownTime (Item item) 
	{

		float itemTime = item.itemTime;

		while( itemTime > 0)
		{
			if(item.timeIsActive)
			{
			itemTime -=  1 * Time.deltaTime;
			timeBar.value = itemTime;
			item.itemTime = itemTime;
			yield return null;
			}
			else
			{
				StopCoroutine("CountDownTime");
				return false;
			}
			
		}
		
	}
	


	public void ClearSlot()
	{
		slotAudio.StartPlay("Use");
		items.Clear();
		timeBarCanvasGroup.alpha = 0;
		ChangeSprite(slotEmpty);
		// ItemBar.EmptySlots++ is place with itembar code for this function

	}


	public void OutOfTime()
	{

		if( !IsEmpty)
		{
			slotAudio.StartPlay("Destroy");
			items.Pop();

			
			if(IsEmpty)
			{
				ChangeSprite(slotEmpty);
				timeBarCanvasGroup.alpha = 0;
				ItemBar.EmptySlots++;
			}
		}
		
	}





	public void OnPointerClick (PointerEventData eventData)
	{

		//Debug.LogWarning("Item Click");

		if(IsEmpty == false && ItemBar.ItemSelectionMode == false)
		{
		
			slotAudio.StartPlay("Select");
			
			ItemBar.ItemSelectionMode = true;
		}else
		{
			slotAudio.StartPlay("Select");
			ItemBar.ItemSelectionMode = false;
		}
	

	}


public void SetMaxTime(float addTime)
	{
		maxTime += addTime;

		timeBar.maxValue = maxTime; // Set max value of timebar
		timeBar.value = maxTime;	// set the timebar  value to max
	}


	void OnDisable(){
		
		Messenger.RemoveListener<float>( "Set MaxTime", SetMaxTime );
	}


}
