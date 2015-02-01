using UnityEngine;
using System.Collections;
using UnityEngine.Cloud.Analytics;

public class UnityAnalyticsIntegration : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
		const string projectId = "aa73181d-013f-4234-aad0-76832e776045";
		UnityAnalytics.StartSDK (projectId);
		
	}
	
}
