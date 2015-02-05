using UnityEngine;
using System.Collections;
using PlayerPrefs = PreviewLabs.PlayerPrefs;

public class SolitaireSetMode : MonoBehaviour {

	public  void SetSolitaireMode(int pegNum)
	{
		PlayerPrefs.SetInt("SolitaireMode BroadSize", pegNum);
		Application.LoadLevel(2); // Solitaire Game Mode

	}

}
