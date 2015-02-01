using UnityEngine;
using System.Collections;

public class ScriptWithEvents : MonoBehaviour {
	
	// Here we have some parameterless void events.
	// That is the requirement for events to be automatically recognized 
	// by SMP
	public delegate void AnyOldEvent();
	public event AnyOldEvent Explode;
	public event AnyOldEvent Detonate;
	
	// subscribe to the events for fun. not necessary though
	void Start () {
		Explode += TellMeAnExplosionHappened;
		Detonate += TellMeADetonationHappened;
	}
	
	// call events on GUI press. Will automatically fire SMP actions. No coding necessary
	void OnGUI () {
		if(GUI.Button(new Rect(Screen.width/5f, Screen.height/3f, Screen.width/5f, Screen.height/3f), "Explode"))
			Explode();
		if(GUI.Button(new Rect(3*Screen.width/5f, Screen.height/3f, Screen.width/5f, Screen.height/3f), "Detonate"))
			Detonate();
	}
	
	void TellMeAnExplosionHappened()
	{
		Debug.LogWarning("EXPLOSION!!!!!");
	}
	
	void TellMeADetonationHappened()
	{
		Debug.LogWarning("DETONATION!!!!!");
	}
}
