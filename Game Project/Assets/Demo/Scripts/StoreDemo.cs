using UnityEngine;
using System.Collections;

public class StoreDemo : MonoBehaviour {

	private Rect windowRect = new Rect((Screen.width/2)-(Screen.width/6),(Screen.height/8),(Screen.width/3),0);
	private int itemQuantity;

	void OnGUI() {
		// Creates a window to hold the demo buttons in
		windowRect = GUILayout.Window(0, windowRect, buttonWindow, "Buttons");
	}

	
	void buttonWindow (int windowID) {
		
		if (GUILayout.Button ("Get Potion\nQuantity "+itemQuantity, GUILayout.Height(Screen.height/8))) {			
			SwarmUserInventory.getItemQuantity(405, delegate(int responseData) {
				Debug.Log ("got back in getItemQuantity, data: "+responseData);	
				itemQuantity = responseData;
			});
		}
		
		if (GUILayout.Button ("Buy Potion", GUILayout.Height(Screen.height/10))) {
			SwarmStore.purchaseItemListing(455, delegate(int responseData) {
				Debug.Log("Got back in purchaseItemListing, data: "+responseData);
			});
		}
		
		if (GUILayout.Button ("Use Potion", GUILayout.Height(Screen.height/10))) {
			SwarmUserInventory.consumeItem(405);
		}
		
		if (GUILayout.Button ("Show Store", GUILayout.Height(Screen.height/10))) {
			Swarm.showStore();				
		}
		
		if (GUILayout.Button ("Back", GUILayout.Height(Screen.height/10))) {
			Application.LoadLevel("HomeDemo");				
		}		
	}
}
