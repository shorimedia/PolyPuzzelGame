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


	private bool isHolder = false;

	public bool IsHolder
	{
		get{ return isHolder;}
		set{ isHolder = value;}
	}


	void Awake () 
	{
		//  get the timebar's slider and canvasgroup for this slot 
		timeBar = timeBarObject.GetComponent<Slider>();
		timeBarCanvasGroup = timeBarObject.GetComponent<CanvasGroup>();
	}

	// Use this for initialization
	void Start () {

		Messenger.AddListener<float>("Set MaxTime", SetMaxTime);

		timeBar.maxValue = maxTime; // Set max value of timebar
		timeBar.value = maxTime;	// set the timebar  value to max

		items = new Stack<Item>();
		//RectTransform slotRec = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {

		if(items.Count != 0 && items.Peek().itemTime <= 0f)
		{
			// play sound wait then clear item
			OutOfTime();
		
			Debug.Log ( gameObject.name + " has clear item due to time");
			
		}


//		if(items.Count != 0)
//		Debug.Log (items.Count);
	
	
	}

	// Used when adding new items
	 public void AddItem(Item item)
	{
		items.Push(item);
		Debug.Log ("push Item");

		item.itemTime = maxTime;     // When a new item is created set its timer to max

		item.timeIsActive = true;

		StartCoroutine(CountDownTime(item)); // start count down

		// this slot is in the holder inventory alway hide time bar
		if(isHolder)
		{
		timeBarCanvasGroup.alpha = 0;
		}
		else
		{
		timeBarCanvasGroup.alpha = 1;
		}

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

		// this slot is in the holder inventory alway hide time bar
		if(isHolder)
		{
			this.items.Peek().timeIsActive = false;
			timeBarCanvasGroup.alpha = 0;
			// don't restart time is on holder's slot
		}
		else
		{
			this.items.Peek().timeIsActive = true;
			StartCoroutine(CountDownTime(this.items.Peek()));		// continue the countdown.
			timeBarCanvasGroup.alpha = 1;  // make timebar visiable
		}
	
		ChangeSprite(CurrentItem.spriteNeutral); // change sprite to item's sprite.
	}

	// Change the Sprite on slot Image component
	private void ChangeSprite(Sprite neutral)
	{
		GetComponent<Image>().sprite = neutral;

	}
	

	// Count down timer for each item
	IEnumerator CountDownTime (Item item) {

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
	

	private void UseItem()
	{

		if( !IsEmpty)
		{
		items.Pop().Use();

			if(IsEmpty)
			{
				ChangeSprite(slotEmpty);
				timeBarCanvasGroup.alpha = 0;
				ItemBar.EmptySlots++;
			}
		}
	}

	public void ClearSlot()
	{
		items.Clear();
		timeBarCanvasGroup.alpha = 0;
		ChangeSprite(slotEmpty);

	}


	public void OutOfTime()
	{

		if( !IsEmpty)
		{

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

		// right click was clicked
		if( eventData.button == PointerEventData.InputButton.Right && !GameObject.Find("Hover"))
		{
			UseItem();
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
