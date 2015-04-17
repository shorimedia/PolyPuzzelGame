using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraSliderZoom : MonoBehaviour {

	public float cameraZoomIndex;
	

	public float CameraZoom
	{
		get{return cameraZoomIndex * 0.1f; UpdateZoom();}
		set
		{
			if(value > 0)
			{
			cameraZoomIndex = value / 0.1f; 
			UpdateZoom();
			}
		}

	}


	// Use this for initialization
	void Start () {


	
	}
	
	// Update is called once per frame
	void UpdateZoom () {

		Camera.main.orthographicSize = cameraZoomIndex;
	
	}
}
