using UnityEngine;
using System.Collections;

// This script is use for anamtions event
//to change to the game state with animations




public class CallEvent : MonoBehaviour {

	public GameObject objectItem;

	private GameManger manager;




	// Use this for initialization
	void Start () {
	
		manager = objectItem.GetComponent<GameManger>();


	}
	
	// Update is called once per frame
	public void Submit (string state) {

// Set the game state to the state name
		manager.SetGameState(state);
	
	}
}
