using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public Menu CurrenMenu;
	
	// Use this for initialization
	void Start ()
	{
		ShowMenu(CurrenMenu);
	}


	public void ShowMenu(Menu menu)
	{
		if ( CurrenMenu != null)
			CurrenMenu.IsOpen = false; CurrenMenu.ShowDepends(false);
		

		CurrenMenu = menu;
		CurrenMenu.IsOpen = true;
		CurrenMenu.ShowDepends(true);


	}

}
