using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public Menu CurrenMenu;

	public bool IsSubMenu = false;

	
	// Use this for initialization
	void Start ()
	{


		//If menu is a sub menu do not show at start
		if(IsSubMenu == false) 
		{
		ShowMenu(CurrenMenu);
		}
	}


	public void ShowMenu(Menu menu)
	{
		if ( CurrenMenu != null)
		{
			CurrenMenu.IsOpen = false; CurrenMenu.ShowDepends(false);
		}

		CurrenMenu = menu;
		CurrenMenu.IsOpen = true;
		SoundManager.PlaySFX(SoundManager.Load("Transistion_Airwave_05"));
		CurrenMenu.ShowDepends(true);


	}

}
