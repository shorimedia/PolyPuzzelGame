using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//
// Script Name: Item info
//Script by: Victor L Josey
// Description: Use to set the item  info UI elements.
// (c) 2015 Shoori Studios LLC  All rights reserved.


public class ItemInfo : MonoBehaviour {

    public Text name;
    public Text details;
    public Image icon;


public void CreateItemInfo(string n, string d, Sprite i)
    {
        name.text = n;
        details.text = d;
        icon.sprite = i;

    }
}
