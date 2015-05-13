using UnityEngine;
using System.Collections;

public class TestSFX : MonoBehaviour {

 public void PlaySFXTest()
	{
		SoundManager.PlaySFX(SoundManager.Load("Transistion_Retro_GameStart_02"));
	}
}
