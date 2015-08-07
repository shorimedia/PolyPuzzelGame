using UnityEngine;
using System.Collections;

public class LeaderboardsDemo : MonoBehaviour {
	
	private Rect windowRect = new Rect((Screen.width/2)-(Screen.width/6),(Screen.height/10),(Screen.width/3),0);

	void OnGUI() {
		// Creates a window to hold the demo buttons in
		windowRect = GUILayout.Window(0, windowRect, buttonWindow, "Buttons");
	}
	
	int leaderboardId = 2169; 	// Replace this with the leaderboard id from the tutorial app you created
	
	void buttonWindow (int windowID) {	
		if (GUILayout.Button ("Submit Score:\n5 points", GUILayout.Height(Screen.height/10))) {
			SwarmLeaderboard.submitScoreGetRank(leaderboardId, 5, delegate(int responseData) {
				Debug.Log("Got rank in submitScore, rank: "+responseData);
			});
		}
		
		if (GUILayout.Button ("Submit Score:\n10 points\nand Show List", GUILayout.Height(Screen.height/10))) {
//			SwarmLeaderboard.submitScore(leaderboardId, 10);
			SwarmLeaderboard.submitScoreAndShowLeaderboard(leaderboardId, 10);
		}

		if (GUILayout.Button ("Disable\nNotification\nPopups", GUILayout.Height(Screen.height/10))) {		
			Swarm.disableNotificationPopups();
		}
		
		if (GUILayout.Button ("Enable\nNotification\nPopups", GUILayout.Height(Screen.height/10))) {		
			Swarm.enableNotificationPopups();
		}
		
		if (GUILayout.Button ("Show 1\nLeaderboard", GUILayout.Height(Screen.height/10))) {
			SwarmLeaderboard.showLeaderboard(leaderboardId);		
		}		
		
		if (GUILayout.Button ("Show All\nLeaderboards", GUILayout.Height(Screen.height/10))) {
			Swarm.showLeaderboards();		
		}
		
		if (GUILayout.Button ("Back", GUILayout.Height(Screen.height/10))) {
			Application.LoadLevel("HomeDemo");				
		}
	}
}
