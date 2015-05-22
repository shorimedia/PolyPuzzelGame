using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class SimpleAds : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Advertisement.Initialize("23615", true);
		//Advertisement.Show();

		StartCoroutine (ShowAdWhenReady());
	}
	

	IEnumerator ShowAdWhenReady()
	{
		while(!Advertisement.isReady())
			yield return null;

		Advertisement.Show();
	}
}
