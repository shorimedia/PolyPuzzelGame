using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PlayerPrefs = PreviewLabs.PlayerPrefs;

//
// Script Name: SoundSettings
//Script by: Victor L Josey
// Description: Save and set sound options in game. Used with sound manager
// (c) 2015 Shoori Studios LLC  All rights reserved.

public class SoundSettings : MonoBehaviour {

	public float musicValue = 1f;
	public float sfxValue = 1f;

	private bool  muteMusic = false;
	private bool  muteSFX =   true;

    public Toggle SFXToggle;
    public Toggle MusicToogle;
	
	public float  SetMusic
	{
		get{ musicValue = SoundManager.GetVolumeMusic(); return musicValue;}
        set { musicValue = value; SoundManager.SetVolumeMusic(musicValue); PlayerPrefs.SetFloat("Music Volume", musicValue); PlayerPrefs.Flush(); }
	}

	public float  SetSfx
	{
		get{ sfxValue = SoundManager.GetVolumeSFX(); return sfxValue;}
        set { sfxValue = value; SoundManager.SetVolumeSFX(sfxValue); PlayerPrefs.SetFloat("SFX Volume", sfxValue); PlayerPrefs.Flush(); }
	}

	public bool  MuteMusic
	{
		get{ muteMusic = SoundManager.MuteMusic(); return muteMusic;}
        set
        {
            muteMusic = value; SoundManager.MuteMusic(value); PlayerPrefs.SetBool("Music Mute", value); PlayerPrefs.Flush();
		}
	}

	public bool  MuteSFX
	{
		get{ muteSFX = SoundManager.MuteSFX(); return muteSFX;}
        set
        {
            muteSFX = value; SoundManager.MuteSFX(value); PlayerPrefs.SetBool("SFX Mute", value); PlayerPrefs.Flush();
		}
	}

	

	// Use this for initialization
	void Start () 
    {
        musicValue = SoundManager.GetVolumeMusic(); 
        sfxValue = SoundManager.GetVolumeSFX();

        // Load saved SFX settings and set toogle
        MuteSFX = PlayerPrefs.GetBool("SFX Mute");
        SFXToggle.isOn = PlayerPrefs.GetBool("SFX Mute");

        MusicToogle.isOn = PlayerPrefs.GetBool("Music Mute");

	}
	

}
