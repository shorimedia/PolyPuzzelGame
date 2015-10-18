using UnityEngine;


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

        PlayParicleFX("AttachItem FX");

        if (iAttach.name == "Plus Peg" || iAttach.name == "Plus Zero")
        {
            PlayParicleFX("AddPeg FX");
        
        }
   

		ItemEquip = true;
		// Add new data
		AttachedItem = iAttach;
		// set new item to read Peg data
		AttachedItem.PegObject = this.gameObject.GetComponent<PegStateMachine>();
		AttachedItem.Use();
	}

    void PlayParicleFX(string name)
    {
        GameObject obj = PoolerScript.current.GetPooledObject(name);

        if (obj == null) return;

        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);

    }

}
