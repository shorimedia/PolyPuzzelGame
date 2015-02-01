using UnityEngine;
using System.Collections;

public class DemoPart3 : MonoBehaviour {
	// In part 3, we'll show how we sync tracks in playlists when we play a new soundconnection.
	// Syncing starts off the new soundconnection at the same time that the previous soundconnection
	// ended at. Start the new soundconnection at a track in the middle of the playlist.
	// This example shows two soundconnections with 2 parts of the loop. SyncTrack2 has two alternate
	// versions of SyncTrack1.  They'll switch and pick off where the other left off.
	// Let's begin!
	
	// This variable let's us know if we're in ULTRA MEGA MODE! or not!
	bool ultraMegaMode = false;
	
	string buttonText = "";
	
	public Texture2D icon;
	GUIStyle buttonSTyle;
	
	// On start, play SyncTrack1 playlist. Notice on the SoundManager prefab, I set cross duration to 0
	// And I checked "Ignore Level Load"
	// Why did I do this? Having a cross duration is counterproductive for looping clips.
	// It won't loop correctly if its fading...
	// And ignoring the level loading functionality gives me complete control over when I play things.
	void Start () {
		SoundManager.PlayConnection("SyncTrack1");
	}
	
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(4.5f*Screen.width/10f, Screen.height/12f, Screen.width/10f, Screen.height/6f), icon);
		
		buttonSTyle = GUI.skin.button;
		buttonSTyle.fontStyle = FontStyle.Bold;
		buttonSTyle.fontSize = 16;
		
		// This is just stupid cosmetic stuff =]
		// based on ultra mega mode state, the text and color of the button will change
		if(!ultraMegaMode)
		{
			buttonText = "ULTRA\nMEGA\nMODE\n!!!!!\n\n(example of track sync)";
			GUI.color = Color.white;
		}
		else
		{
			buttonText = "WOOOO!!!";
			GUI.color = RandomColor();
		}
		
		// Now for the real stuff! It starts or ends ultra mega mode based on bonus state.
		if(GUI.Button(new Rect(Screen.width/3f,Screen.height/3f,Screen.width/3f,Screen.height/3f), buttonText, buttonSTyle))
		{
			if(!ultraMegaMode)
			{
				ultraMegaMode = true;
				
				// We have to make sure each swap is the same track from the previous playlist.
				// So we get the track number of the current song playing.
				int trackNum = SoundManager.GetTrackNumber(SoundManager.GetCurrentSong());
				
				// Then we play the second track, with sync enabled, and at a certain track number.
				SoundManager.PlayConnection("SyncTrack2", true, trackNum);
			}
			else
			{
				// Do the opposite here.
				ultraMegaMode = false;
				
				int trackNum = SoundManager.GetTrackNumber(SoundManager.GetCurrentSong());
				SoundManager.PlayConnection("SyncTrack1", true, trackNum);
			}
		}
	}
	
	Color RandomColor()
	{
		int num = Random.Range(0,5);
		switch(num)
		{
		case 0:
			return Color.blue;
		case 1:
			return Color.red;
		case 2:
			return Color.cyan;
		case 3:
			return Color.green;
		case 4:
			return Color.yellow;
		default:
			return Color.white;
		}
	}
}
