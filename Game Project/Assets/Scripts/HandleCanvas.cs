using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HandleCanvas : MonoBehaviour {

	private CanvasScaler scaler;

	// Use this for initialization
	void Start () {
		scaler = GetComponent<CanvasScaler>();
		scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
	
	}
	

}
