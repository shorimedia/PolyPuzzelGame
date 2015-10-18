using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//
// Script Name: Item info Manager
//Script by: Victor L Josey
// Description: Manage to  the list of revealed Items.
// (c) 2015 Shoori Studios LLC  All rights reserved.


public class ItemInfoManager : MonoBehaviour {

    public GameObject parent;
    public ItemInfo InfoBox;

    public ItemSpawner iSpawn;

    public List<ItemInfo> ItemData = new List<ItemInfo>();


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

   public void LoadIemInfo()
    {

        if (ItemData.Count > 0)
        {

            foreach(ItemInfo imIf in ItemData )
            {
                Destroy(imIf.gameObject);
            }

            ItemData.Clear();
            Debug.Log("Delete Item data");
        }

    
       
        foreach(Item im in iSpawn.itemsCommon)
        {

            if (im.revealed == true)
            {
                ItemInfo newData = (ItemInfo)Instantiate(InfoBox, parent.gameObject.transform.position, Quaternion.identity);
                newData.CreateItemInfo(im.name, im.details, im.spriteNeutral);
                newData.transform.SetParent(parent.gameObject.transform);

                ItemData.Add(newData);
            }
        }

        foreach (Item im in iSpawn.itemsUncommon)
        {

            if (im.revealed == true)
            {
                ItemInfo newData = (ItemInfo)Instantiate(InfoBox, parent.gameObject.transform.position, Quaternion.identity);
                newData.CreateItemInfo(im.name, im.details, im.spriteNeutral);
                newData.transform.SetParent(parent.gameObject.transform);

                ItemData.Add(newData);
            }
        }

        foreach (Item im in iSpawn.itemsRare)
        {

            if (im.revealed == true)
            {
                ItemInfo newData = (ItemInfo)Instantiate(InfoBox, parent.gameObject.transform.position, Quaternion.identity);
                newData.CreateItemInfo(im.name, im.details, im.spriteNeutral);
                newData.transform.SetParent(parent.gameObject.transform);

                ItemData.Add(newData);
            }
        }


        foreach (Item im in iSpawn.itemsVeryRare)
        {

            if (im.revealed == true)
            {
                ItemInfo newData = (ItemInfo)Instantiate(InfoBox, parent.gameObject.transform.position, Quaternion.identity);
                newData.CreateItemInfo(im.name, im.details, im.spriteNeutral);
                newData.transform.SetParent(parent.gameObject.transform);

                ItemData.Add(newData);
            }
        }


    }
}
