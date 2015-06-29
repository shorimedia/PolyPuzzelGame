using UnityEngine;
using System.Collections;

public class PlaySoundFX : MonoBehaviour {
	public string soundName;

	public float delay = 0f;
	public float volume = 3.4f;
	public float pitch = 3.4f;
	public bool advance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Play a sound track
	public void StartPlay () 
	{
		if(advance)
		{
			SoundManager.PlaySFX(soundName,false,delay, volume,pitch);
		}else
		{
			SoundManager.PlaySFX(soundName);
		}
	}
}
