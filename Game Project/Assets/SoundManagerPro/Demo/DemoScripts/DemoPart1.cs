using UnityEngine;
using System.Collections;

public class DemoPart1 : MonoBehaviour {
	// Particle effect to go with explosion sound ;)
	public GameObject explosionPrefab;
	public Texture2D AntiLunchBoxLogo;
	
	// Sample AudioClips
	public AudioClip sample1;
	public AudioClip sample2;
	public AudioClip sample3;
	public AudioClip sample4;
	
	int thecolor = 0;
	Color buttonColor = Color.yellow;
	
	float unitX;
	float unitY;

	int page = 1;
	
	// A SoundConnection to use later, initialization is shown in Start()
	SoundConnection sc;
	
	// Just so we don't have duplicates while moving through scenes
	void Awake () {
		if(GameObject.FindObjectsOfType(typeof(DemoPart1)).Length > 1)
		{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
	}
	
	void Start()
	{
		// How to initialize a SoundConnection
		sc = SoundManager.CreateSoundConnection("TemporarySoundConnection", SoundManager.PlayMethod.ContinuousPlayThrough, sample4, sample2, sample3, sample1);
		
		unitX = Screen.width / 48f;
		unitY = Screen.height / 30f;
	}	
	
	GUIStyle boxStyle;
	GUIStyle buttonSTyle;
	GUIStyle labelStyle;
	void OnGUI()
	{
		boxStyle = GUI.skin.box;
		boxStyle.fontStyle = FontStyle.Bold;
		boxStyle.fontSize = 14;
		
		buttonSTyle = GUI.skin.button;
		buttonSTyle.fontStyle = FontStyle.Bold;
		
		labelStyle = GUI.skin.label;
		labelStyle.alignment = TextAnchor.MiddleCenter;
		labelStyle.fontStyle = FontStyle.Bold;
		labelStyle.normal.textColor = Color.black;
		
		/* TITLE */
		GUI.Box(new Rect(16f*unitX, 0f, 16f*unitX, 2f*unitY), "AntiLunchBox\nSoundManagerPro 3.0", boxStyle);
		
		/* LOAD SCENES */
		GUI.color = buttonColor;
		if(GUI.Button(new Rect(6f*unitX, 3f*unitY, 8f*unitX, 4f*unitY), "Load Level:\nMusicScene1" ) )
		{
			Application.LoadLevel("MusicScene1"); //LoadLevelAsync also works for UNITY PRO users
		}
		
		
		if(GUI.Button(new Rect(20f*unitX, 3f*unitY, 8f*unitX, 4f*unitY), "Load Level:\nMusicScene2" ) )
		{
			Application.LoadLevel("MusicScene2");
		}
		
		
		if(GUI.Button(new Rect(34f*unitX, 3f*unitY, 8f*unitX, 4f*unitY), "Load Level:\nMusicScene3" ) )
		{
			Application.LoadLevel("MusicScene3");
		}
		GUI.color = Color.white;
		
		/* COLUMN 1 */
		float yPos = unitY * 8f;
		float xPos = unitX * 5f;
		float height = unitY * 3f;
		float width = unitX * 10f;
		
		
		switch(page)
		{
		case 1: // PAGE 1
			// Will play sample1, interrupting the current SoundConnection.  You can resume a SoundConnection when it's done using another overload below
			if(GUI.Button(new Rect(xPos, yPos, width, height), "Play Sample1"))
			{
				// You can use SoundManager.Load(clipname, customPath) -- Or set SoundManager.resourcesPath and forget the custom path
				SoundManager.Play(sample1);
			}
			
			// Plays sample2 immediately, no crossfade
			if(GUI.Button(new Rect(xPos, yPos+=(unitY*4f), width, height), "Play Sample2\nImmediately"))
			{
				SoundManager.PlayImmediately(sample2);
			}
			
			// Plays sample3, and will call 'ChangeButtonColor' when the song ends.
			if(GUI.Button(new Rect(xPos, yPos+=(unitY*4f), width, height), "Play Sample3 Then\nChange Button Colors\nOn Song End"))
			{
				SoundManager.Play(sample3, false, ChangeButtonColor);
			}
			
			// Will play a SoundConnection made in code.  Does not save unless you use (AddSoundConnection/ReplaceSoundConnection)
			if(GUI.Button(new Rect(xPos, yPos+=(unitY*4f), width, height), "Play Temporary\nSoundConnection"))
			{
				SoundManager.PlayConnection(sc);
			}
			
			// Play a custom SoundConnection not tied to a level
			if(GUI.Button(new Rect(xPos, yPos+=(unitY*4f), width, height), "Play Custom\nSoundConnection\n(\"MyCustom\")"))
			{
				SoundManager.PlayConnection("MyCustom");
			}
			
			/* COLUMN 2 */
			yPos = unitY * 8f;
			xPos = unitX * 19f;
			
			// Set to 50% volume
			if(GUI.Button(new Rect(xPos, yPos, width, height), "Set All Volume 50%"))
			{
				SoundManager.SetVolume(.5f);
			}
			
			// Set to 100% volume
			if(GUI.Button(new Rect(xPos, yPos+=(unitY*4f), width, height), "Set All Volume 100%"))
			{
				SoundManager.SetVolume(1f);
			}
			
			// Set pitch to .75
			if(GUI.Button(new Rect(xPos, yPos+=(unitY*4f), width, height), "Set All Pitch 75%"))
			{
				SoundManager.SetPitch(.75f);
			}
			
			// Set pitch to 1
			if(GUI.Button(new Rect(xPos, yPos+=(unitY*4f), width, height), "Set All Pitch 100%"))
			{
				SoundManager.SetPitch(1f);
			}
			
			/* COLUMN 3 */
			yPos = unitY * 8f;
			xPos = unitX * 33f;
			
			// Crossfade out all music and stop it
			if(GUI.Button(new Rect(xPos, yPos, width, height), "Stop Music"))
			{
				SoundManager.StopMusic();
			}
			
			// Stop music immediately, no crossfade
			if(GUI.Button(new Rect(xPos, yPos+=(unitY*4f), width, height), "Stop Music\nImmediately"))
			{
				SoundManager.StopMusicImmediately();
			}
			
			// Toggle mute on or off, returns mute status
			if(GUI.Button(new Rect(xPos, yPos+=(unitY*4f), width, height), "Toggle All Mute"))
			{
				SoundManager.Mute();
			}
			
			// Toggle mute of SFX on or off, returns mute status
			if(GUI.Button(new Rect(xPos, yPos+=(unitY*4f), width, height), "Toggle SFX Mute"))
			{
				SoundManager.MuteSFX();
			}
			
			// Toggle mute of music on or off, returns mute status
			if(GUI.Button(new Rect(xPos, yPos+=(unitY*4f), width, height), "Toggle Music Mute"))
			{
				SoundManager.MuteMusic();
			}
			break;
			
			
			
			
			
			
			
			
			
			
			
			
		case 2: // PAGE 2
			// Go to next track in playlist
			if(GUI.Button(new Rect(xPos, yPos, width, height), "Next Track"))
			{
				SoundManager.Next();
			}
			
			// Go to previous track in playlist
			if(GUI.Button(new Rect(xPos, yPos+=(unitY*4f), width, height), "Prev Track"))
			{
				SoundManager.Prev();
			}
			
			// Play sample1 in a loop
			if(GUI.Button(new Rect(xPos, yPos+=(unitY*4f), width, height), "Play Sample1 as\na Looping Track"))
			{
				SoundManager.Play(sample1, true);
			}
			
			// Pause all sound
			if(GUI.Button(new Rect(xPos, yPos+=(unitY*4f), width, height), "Pause"))
			{
				SoundManager.Pause();
			}
			
			// Unpause all sound
			if(GUI.Button(new Rect(xPos, yPos+=(unitY*4f), width, height), "UnPause"))
			{
				SoundManager.UnPause();
			}
			
			/* COLUMN 2 */
			yPos = unitY * 8f;
			xPos = unitX * 19f;
			
			// Have SoundManager ignore level loading. So you can set SoundConnections to play when YOU want
			if(GUI.Button(new Rect(xPos, yPos, width, height), "Set SoundManager to\nIgnore AI"))
			{
				SoundManager.SetIgnoreLevelLoad(true);
			}
			
			// Plays sfx, but it will never play more than the set cap amount. The default cap amount is 3.
			if(GUI.Button(new Rect(xPos, yPos+=(unitY*4f), width, height), "Play Capped SFX\nBUTTON SMASH\nTHIS BUTTON!"))
			{
				SoundManager.PlayCappedSFX("Explosion1", "Explosion");
			}
			
			// Set pitch of only SFX to 1.25
			if(GUI.Button(new Rect(xPos, yPos+=(unitY*4f), width, height), "Set Pitch of SFX\n125%"))
			{
				SoundManager.SetPitchSFX(1.25f);
			}
			
			// Set pitch of only SFX to 1
			if(GUI.Button(new Rect(xPos, yPos+=(unitY*4f), width, height), "Set Pitch of SFX\n100%"))
			{
				SoundManager.SetPitchSFX(1f);
			}
			
			/* COLUMN 3 */
			yPos = unitY * 8f;
			xPos = unitX * 33f;
			
			// Remove the SoundConnection from a scene
			if(GUI.Button(new Rect(xPos, yPos, width, height), "Remove SoundConnection\nin MusicScene1"))
			{
				SoundManager.RemoveSoundConnectionForLevel("MusicScene1");
			}
			
			// Add a SoundConnection to a scene, or if it exists already, replace it.
			if(GUI.Button(new Rect(xPos, yPos+=(unitY*4f), width, height), "Add/Replace\nSoundConnection\nin MusicScene1"))
			{
				SoundConnection replacement = SoundManager.CreateSoundConnection("MusicScene1", SoundManager.PlayMethod.ContinuousPlayThrough, sample4, sample2, sample3, sample1);
				SoundManager.ReplaceSoundConnection(replacement);
			}
			
			// Add a CUSTOM SoundConnection to a scene, or if it exists already, replace it.
			if(GUI.Button(new Rect(xPos, yPos+=(unitY*4f), width, height), "Add/Replace\nCustom SoundConnection\n(\"NewCustom\")"))
			{
				SoundConnection newCustom = SoundManager.CreateSoundConnection("NewCustom", SoundManager.PlayMethod.ContinuousPlayThrough, sample4, sample2, sample3, sample1);
				SoundManager.ReplaceSoundConnection(newCustom);
			}
			
			// Plays a random SFX from a group
			if(GUI.Button(new Rect(xPos, yPos+=(unitY*4f), width, height), "Play Random SFX\nfrom MyGroup"))
			{
				SoundManager.PlaySFX(SoundManager.LoadFromGroup("MyGroup"));
			}
			
			// Change the duration of music crossfades
			if(GUI.Button(new Rect(xPos, yPos+=(unitY*4f), width, height), "Change the Crossfade\nDuration to 1s"))
			{
				SoundManager.SetCrossDuration(1f);
			}
			break;
			
			
			
			
			
			
			
			
			
			
			
		case 3: // PAGE 3
			// Cross in a SFX. Crossfade, crossout are all available as well
			if(GUI.Button(new Rect(xPos, yPos, width, height), "Cross in a SFX"))
			{
				SoundManager.CrossIn(1f, SoundManager.PlaySFX("Eruption1"));
			}
			
			// Play a SFX while ducking all other sound
			if(GUI.Button(new Rect(xPos, yPos+=(unitY*4f), width, height), "Use ducking for\nFlash Bang effect"))
			{
				SoundManager.PlaySFX("Flashbang1", false, 0f, 1f, 1f, Vector3.zero, null, SoundDuckingSetting.DuckAll, .1f);
			}
			
			// Play a SFX while ducking all other sound and while pitch
			if(GUI.Button(new Rect(xPos, yPos+=(unitY*4f), width, height), "Use ducking for\nSlo Mo effect"))
			{
				SoundManager.PlaySFX("Pentakill1", false, 0f, 1f, .65f, Vector3.zero, null, SoundDuckingSetting.DuckAll, .1f, .5f);
			}
			
			// Run function to debug output at end of SFX play
			if(GUI.Button(new Rect(xPos, yPos+=(unitY*4f), width, height), "Run function that\ndebugs at end\nof playing SFX"))
			{
				SoundManager.PlaySFX("Explosion1", false, 0f, 1f, 1f, Vector3.zero, RunThisOnEnd);
			}
			
			// Play SFX with delay
			if(GUI.Button(new Rect(xPos, yPos+=(unitY*4f), width, height), "Play SFX after\n1 second"))
			{
				SoundManager.PlaySFX("Explosion1", false, 1f, 1f, 1f, Vector3.zero);
			}
			break;
		default:
			break;
		}

		/* FOOTER */
		// Spawn the explosion particle effect and play the sound effect in 2 different ways (there are 6 more overloads)
		if(GUI.Button(new Rect(20f*unitX, 24f*unitY + Mathf.PingPong(Time.time*24f, unitY), 8f*unitX, 5f*unitY), AntiLunchBoxLogo, labelStyle))
		{
			GameObject newExplosion = GameObject.Instantiate(explosionPrefab, Camera.main.transform.position + (5f * Camera.main.transform.forward), Quaternion.identity) as GameObject;
			if(Random.Range(0,2) == 1)
				// This will play the SFX from the Stored SFXs on SoundManager, in the pooling system.
				SoundManager.PlaySFX("Explosion1");
			else
				// This will play the SFX on the gameobject--if it doesn't have an audiosource, it'll add it for you.
				SoundManager.PlaySFX(newExplosion, "Explosion1");
			
			//If you want to make sure that audiosource is 2D, use this:
			//SoundManagerTools.make2D( ref SoundManager.PlaySFX(newExplosion, "Explosion1"));
			//OR 3d
			//SoundManagerTools.make3D( ref SoundManager.PlaySFX(newExplosion, "Explosion1"));
		}
		GUI.Label(new Rect(20f*unitX, 24f*unitY + Mathf.PingPong(Time.time*24f, unitY), 8f*unitX, 5f*unitY), "Click Me!", labelStyle);

		if(!CanGoNext()) GUI.enabled = false;
		if(GUI.Button(new Rect(Screen.width - 75f, 0f, 75f, 50f), "Next\nPage"))
			page++;
		GUI.enabled = true;

		if(!CanGoPrev()) GUI.enabled = false;
		if(GUI.Button(new Rect(0f, 0f, 75f, 50f), "Prev\nPage"))
			page--;
		GUI.enabled = true;
	}
	
	void ChangeButtonColor()
	{
		switch(thecolor)
		{
		case 0:
			buttonColor = Color.blue;
			thecolor = 1;
			break;
		case 1:
			buttonColor = Color.red;
			thecolor = 2;
			break;
		case 2:
			buttonColor = Color.green;
			thecolor = 3;
			break;
		case 3:
		default:
			buttonColor = Color.yellow;
			thecolor = 0;
			break;
		}
	}
	
	void RunThisOnEnd()
	{
		Debug.LogWarning("SFX finished playing.");
	}

	bool CanGoNext()
	{
		switch(page)
		{
		case 1:
			return true;
		case 2:
			return true;
		case 3:
			return false;
		default:
			return false;
		}
	}

	bool CanGoPrev()
	{
		switch(page)
		{
		case 1:
			return false;
		case 2:
			return true;
		case 3:
			return true;
		default:
			return false;
		}
	}
	
	void OnLevelWasLoaded(int level)
	{
		switch(Application.loadedLevelName)
		{
		case "MusicScene1":
			Camera.main.backgroundColor = Color.gray;
			break;
		case "MusicScene2":
			Camera.main.backgroundColor = Color.magenta;
			break;
		case "MusicScene3":
			Camera.main.backgroundColor = Color.blue;
			break;
		default:
			Camera.main.backgroundColor = new Color(49f/255f, 77f/255f, 121f/255f, 5f/255f);
			break;
		}
	}
}
