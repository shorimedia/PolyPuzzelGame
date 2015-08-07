using UnityEngine;
using System.Collections;

public class CloudDataDemo : MonoBehaviour {

	private Rect windowRect = new Rect((Screen.width/2)-(Screen.width/6),(Screen.height/10),(Screen.width/3),0);
	private string rxData;
	
	private GUIStyle button;
	
	void OnGUI() {
		// Creates a window to hold the demo buttons in
		windowRect = GUILayout.Window(0, windowRect, buttonWindow, "Buttons");
	}
	
	string dataKey = "myKey"; // Replace with any key you want
	string data = "Data 1"; // Replace with whatever data you want
	string data2 ="Data 2";// Replace with whatever data you want
	
	void buttonWindow (int windowID) {
		if (GUILayout.Button ("Send data\nto server: " + data, GUILayout.Height(Screen.height/10))) {
			SwarmActiveUser.saveUserData(dataKey, data);
		}
		
		if (GUILayout.Button ("Send data\nto server: " + data2, GUILayout.Height(Screen.height/10))) {
			SwarmActiveUser.saveUserData(dataKey, data2);
		}
		
		if (GUILayout.Button ("Retrieve data\nfrom server: " + rxData, GUILayout.Height(Screen.height/10))) {
			SwarmActiveUser.getUserData (dataKey, delegate(string responseData) {
				Debug.Log("Got back in CloudDataDemo, data: "+responseData);
				rxData = responseData;
			});	
		}
		
		if (GUILayout.Button ("Back", GUILayout.Height(Screen.height/10))) {
			Application.LoadLevel("HomeDemo");				
		}		
	}
}
