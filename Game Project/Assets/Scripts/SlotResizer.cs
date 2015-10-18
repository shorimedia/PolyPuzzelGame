using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//resize slot base on devices screen size

public class SlotResizer : MonoBehaviour {


    public GridLayoutGroup grid;


	// Use this for initialization
	void Awake () 
		{

		//print(Screen.currentResolution);
        //print(Screen.width);
        //print(Screen.height);

        if ((Screen.width == 1920 && Screen.height == 1080) || (Screen.width > 1900 && Screen.height > 800))
        {
            
            grid.cellSize = new Vector2(120,120);
            grid.spacing = new Vector2(0, -42);
        }


        if (Screen.width == 1280 && Screen.height == 800)
        {
            grid.cellSize = new Vector2(100, 100);
            grid.spacing = new Vector2(0,-5);
        }


        if (Screen.width == 854 && Screen.height == 480)
        {
            grid.cellSize = new Vector2(70, 70);
            grid.spacing = new Vector2(0, 25);
        }

        if (Screen.width == 800 && Screen.height == 480)
        {
            grid.cellSize = new Vector2(70, 70);
            grid.spacing = new Vector2(0, 25);
        }

        if (Screen.width == 480 && Screen.height == 320)
        {
            grid.cellSize = new Vector2(50, 50);
            grid.spacing = new Vector2(0, 60);
        }



    }



}
