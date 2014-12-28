using UnityEngine;
using System.Collections;

public class AchievementsDemo : MonoBehaviour {

	private Rect windowRect = new Rect((Screen.width/2)-(Screen.width/6),(Screen.height/10),(Screen.width/3),0);

	void OnGUI() {
		// Creates a window to hold the buttons
		windowRect = GUILayout.Window(0, windowRect, buttonWindow, "Buttons");
	}
	
	int achievement1Id = 4247; // Replace with the first achievement id from the tutorial app you created
	int achievement2Id = 4249; // Replace with the second achievement id from the tutorial app you created
	
	void buttonWindow (int windowID) {
		if (GUILayout.Button("Level Up!" ,GUILayout.Height(Screen.height/10))){
			SwarmAchievement.unlockAchievement(achievement1Id);
		}
		
		if (GUILayout.Button("Tutorial Started\nand Show List", GUILayout.Height(Screen.height/10))){	
//			SwarmAchievement.unlockAchievement(achievement2Id);
			SwarmAchievement.unlockAchievementAndShowAchievements(achievement2Id);
		}
		
		if (GUILayout.Button ("Show Achievements", GUILayout.Height(Screen.height/10))) {
			Swarm.showAchievments();
		}
		
		if (GUILayout.Button ("Back", GUILayout.Height(Screen.height/10))) {
			Application.LoadLevel("HomeDemo");				
		}				
		
	}
	
}