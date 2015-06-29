using UnityEngine;
using System.Collections;

public class PegAudio : MonoBehaviour {

	public string activatePegSFX,deactivatePegSFX, destroyPegSFX, movePegSFX, openPegSFX, uncapaibleSFX;
	public string plusPoints, minusPoints,  bigPoints;

	// Use this for initialization
	void Start () {
	
	}
	


	public void  PlayPegSoundFX(string sfx)
	{

		switch(sfx)
		{
		case "Activate" : SoundManager.PlaySFX(activatePegSFX); break;
		case "De-activate" : SoundManager.PlaySFX(deactivatePegSFX); break;
		case "Destroy" : SoundManager.PlaySFX(destroyPegSFX); break;
		case "Move" : SoundManager.PlaySFX(movePegSFX); break;
		case "Open" :  SoundManager.PlaySFX(openPegSFX); break;
		case "Uncap" : SoundManager.PlaySFX(uncapaibleSFX); break;

		case "PlusPoints" : SoundManager.PlaySFX(plusPoints); break;
		case "MinusPoints" : SoundManager.PlaySFX(minusPoints); break;
		case "BigPoints" : SoundManager.PlaySFX(bigPoints); break;

		}

	}









}
