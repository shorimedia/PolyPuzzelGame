using UnityEngine;
using System.Collections;

public class LoginDemo : MonoBehaviour {
	
	private Rect windowRect;
	
	// To obtain your applicationId and applicationKey, please register for an account and
	// setup a new app at http://swarmconnect.com
	//
	int applicationId = 1495; // Replace with the application id from your Swarm admin panel at http://swarmconnect.com
	string applicationKey = "3a6cf48a88705635fb4fee35dad6be75"; // Replace with the application key from your Swarm admin panel at http://swarmconnect.com

	void OnGUI() {
		
		// Creates a window to hold the buttons
		windowRect = new Rect((Screen.width/2)-(Screen.width/6),(Screen.height/10),(Screen.width/3),0);
		windowRect = GUILayout.Window(0, windowRect, buttonWindow, "Swarm Demo");
	}	
	
	void buttonWindow (int windowID) {	
		
		// Display the login screen when the login button is pressed (and initialize Swarm if needed).
		// Note that the call to Swarm.init will automatically display the login screen upon initialization.
		// Also, if you call Swarm.init and the user has already logged in, the login screen will be skipped
        // thereby enabling the user to play the game without having to log in every time.
		if (GUILayout.Button ("Login", GUILayout.Height(Screen.height/10))) {
	
			if (Swarm.isInitialized() && !Swarm.isLoggedIn()){
				Swarm.showLogin();
			} else {
				Swarm.init(applicationId, applicationKey);
			}
			
		}	
	}
	
	void Start () {
		
		if (Swarm.isEnabled() == true) {
			Debug.Log ("Is enabled");	
		} else {
			Debug.Log ("Is not enabled");
		}

		// Proceed to the "Demo" level upon a successful login.
		SwarmLoginManager.addLoginListener(delegate(int status) {
			
			if (status == SwarmLoginManager.USER_LOGGED_IN) {
				Application.LoadLevel("HomeDemo");
			} else if (status == SwarmLoginManager.LOGIN_STARTED) {
				Debug.Log ("login started!");
			} else if (status == SwarmLoginManager.LOGIN_CANCELED) {
				Debug.Log ("login canceled!");
			} else if (status == SwarmLoginManager.USER_LOGGED_OUT) {
				Debug.Log ("user logged out!");
			}
		});
	}
}
