using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using System;

public class SimpleAds : MonoBehaviour {

	public PlatformManager pfManager;

	// Use this for initialization
	void Start () 
	{
		Advertisement.Initialize("23615", true);
		//Advertisement.Show();

		//StartCoroutine (ShowAdWhenReady());
	}


    public void StartAds()
	{

		if(pfManager.hasAds == true)
		{
		StartCoroutine (ShowAdWhenReady());
		}
		// else do nothing
	}


	IEnumerator ShowAdWhenReady()
	{
		while(!Advertisement.isReady())
			yield return null;

		Advertisement.Show();
	}
}
