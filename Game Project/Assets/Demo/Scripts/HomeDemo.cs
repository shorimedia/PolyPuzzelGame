using UnityEngine;
using System.Collections;

[ExecuteInEditMode]

public class HomeDemo : MonoBehaviour {
	
	private Rect windowRect = new Rect(((Screen.width - Screen.width/3 ) / 2),(Screen.height/10),(Screen.width/3),0);
	private string username;
	
	void OnStart () {

	}
	
	void OnGUI() {
		
		// Creates a window to hold the demo buttons in
		windowRect = GUILayout.Window(0, windowRect, buttonWindow, "Buttons");
	}
	
	void buttonWindow (int windowID) {
		
		if (GUILayout.Button ("Get Username: " + username,GUILayout.Height(Screen.height/10))) {
			username = SwarmActiveUser.getUsername();
		}
		
		if (GUILayout.Button ("Achievements Demo", GUILayout.Height(Screen.height/10))) {
			Application.LoadLevel("AchievementsDemo");				
		}
		
		if (GUILayout.Button ("Leaderboards Demo", GUILayout.Height(Screen.height/10))) {
			Application.LoadLevel("LeaderboardsDemo");			
		}
		
		if (GUILayout.Button ("Cloud Data Demo", GUILayout.Height(Screen.height/10))) {
			Application.LoadLevel("CloudDataDemo");		
		}
		
		if (GUILayout.Button ("Store Demo", GUILayout.Height(Screen.height/10))) {
			Application.LoadLevel("StoreDemo");						
		}
		
		if (GUILayout.Button ("Show Dashboard", GUILayout.Height(Screen.height/10))) {
			Swarm.showDashboard();
			
		}
	}
}
