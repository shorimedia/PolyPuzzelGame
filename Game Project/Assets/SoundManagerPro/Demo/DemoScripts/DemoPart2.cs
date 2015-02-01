using UnityEngine;
using System.Collections;

public class DemoPart2 : MonoBehaviour {
	public Texture2D icon;
	int part = 0;
	public GameObject part1;
	public GameObject part2;
	public GameObject part3;
	public GameObject part4;
	public GameObject part5;
	public GameObject part6;
	
	// Update is called once per frame
	void OnGUI () {
		GUI.DrawTexture(new Rect(2*Screen.width/5f, Screen.height/3f, Screen.width/5f, Screen.height/3f), icon);
		
		if(part == 0)
		{
			if(GUI.Button(new Rect(Screen.width/3f, 3f*Screen.height/4f, Screen.width/3f, Screen.height/4f), "Click Next\nThrough The Examples\nOf Different Ways To\nUse AudioSourcePro"))
			{
				part++;
				ActivatePart(part1);
			}
		}
		else if(part == 1)
		{
			if(GUI.Button(new Rect(Screen.width/3f, 3f*Screen.height/4f, Screen.width/3f, Screen.height/4f), "Part 1:\nUse AudioClip As ClipType\n(Plays On Awake)\n\nNext"))
			{
				part++;
				ActivatePart(part2);
			}
		}
		else if(part == 2)
		{
			if(GUI.Button(new Rect(Screen.width/3f, 3f*Screen.height/4f, Screen.width/3f, Screen.height/4f), "Part 2:\nUse SoundManager Clip As ClipType\n(Plays On Awake)\n\nNext"))
			{
				part++;
				ActivatePart(part3);
			}
		}
		else if(part == 3)
		{
			if(GUI.Button(new Rect(Screen.width/3f, 3f*Screen.height/4f, Screen.width/3f, Screen.height/4f), "Part 3:\nUse SFXGroup Clip As ClipType\n(Plays On Awake)\n\nNext"))
			{
				part++;
				ActivatePart(part4);
			}
		}
		else if(part == 4)
		{
			if(GUI.Button(new Rect(Screen.width/3f, 3f*Screen.height/4f, Screen.width/3f, Screen.height/4f), "Part 4:\nSet Up Event Triggers\n(Loops OnEnable/Stops OnDisable)\n\nNext"))
			{
				part++;
				ActivatePart(part5);
			}
		}
		else if(part == 5)
		{
			if(GUI.Button(new Rect(Screen.width/3f, 3f*Screen.height/4f, Screen.width/3f, Screen.height/4f), "Part 5:\nCustom Event Triggers From Script\n(Loops On Explode/Stops On Detonate)\n\nNext"))
			{
				part = 6;
				ActivatePart(part6);
			}
		}
		else if(part == 6)
		{
			if(GUI.Button(new Rect(Screen.width/3f, 3f*Screen.height/4f, Screen.width/3f, Screen.height/4f), "Part 6:\nDemonstration of using\nTags, Layers, Names as filters\n\nRestart"))
			{
				part = 0;
				ActivatePart(null);
			}
		}
	}
	
	void ActivatePart(GameObject partX)
	{
#if UNITY_3_4 || UNITY_3_5
		if(part1.active)
			part1.SetActiveRecursively(false);
		if(part2.active)
			part2.SetActiveRecursively(false);
		if(part3.active)
			part3.SetActiveRecursively(false);
		if(part4.active)
			part4.SetActiveRecursively(false);
		if(part5.active)
			part5.SetActiveRecursively(false);
		if(part6.active)
			part6.SetActiveRecursively(false);
		if(partX != null)
			partX.SetActiveRecursively(true);
#else
		if(part1.activeSelf)
			part1.SetActive(false);
		if(part2.activeSelf)
			part2.SetActive(false);
		if(part3.activeSelf)
			part3.SetActive(false);
		if(part4.activeSelf)
			part4.SetActive(false);
		if(part5.activeSelf)
			part5.SetActive(false);
		if(part6.activeSelf)
			part6.SetActive(false);
		if(partX != null)
			partX.SetActive(true);
#endif
	}
}
