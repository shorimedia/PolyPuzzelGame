using UnityEngine;
using System.Collections;
using PlayerPrefs = PreviewLabs.PlayerPrefs;

public class SoundSettings : MonoBehaviour {

	public float musicValue = 1f;
	public float sfxValue = 1f;

	public bool  muteMusic = false;
	public bool  muteSFX = false;
	
	public float  SetMusic
	{
		get{ musicValue = SoundManager.GetVolumeMusic(); return musicValue;}
		set{musicValue = value; SoundManager.SetVolumeMusic(musicValue); PlayerPrefs.SetFloat("Music Volume", musicValue);}
	}

	public float  SetSfx
	{
		get{ sfxValue = SoundManager.GetVolumeSFX(); return sfxValue;}
		set{sfxValue = value; SoundManager.SetVolumeSFX(sfxValue); PlayerPrefs.SetFloat("SFX Volume", sfxValue);}
	}

	public bool  MuteMusic
	{
		get{ muteMusic = SoundManager.MuteMusic(); return muteMusic;}
		set{ muteMusic = value; SoundManager.MuteMusic(muteMusic); 	 PlayerPrefs.SetBool("Music Mute", muteMusic);
		}
	}

	public bool  MuteSFX
	{
		get{ muteSFX = SoundManager.MuteSFX(); return muteSFX;}
		set{muteSFX = value; SoundManager.MuteSFX(muteSFX); 	   PlayerPrefs.SetBool("SFX Mute", muteSFX);
		}
	}

	

	// Use this for initialization
	void Start () {
	
		//musicValue = 	PlayerPrefs.GetFloat("Music Volume");
		//sfxValue = 		PlayerPrefs.GetFloat("SFX Volume");
	}
	

}
