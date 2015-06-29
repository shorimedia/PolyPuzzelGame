using UnityEngine;
using System.Collections;

public class SlotAudio : MonoBehaviour {

	public string itemPopup, itemselect, itemUse, itemDestroy;


	public void StartPlay (string soundName) 
	{

		switch(soundName)
		{
		case "Popup": SoundManager.PlaySFX(itemPopup);break;
		case "Select" : SoundManager.PlaySFX(itemselect);break;
		case "Use": SoundManager.PlaySFX(itemUse); break;
		case "Destroy": SoundManager.PlaySFX(itemDestroy); break;
		}
	
	}
}
