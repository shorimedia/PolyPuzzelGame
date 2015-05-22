using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinPopup : MonoBehaviour {

	public Text ScoreTxt;
	public Text LeaderScoreTxt;

	public Text TimeTxt;
	public Text LeaderTimeTxt;

	public Text PegNumberTxt;


public enum WinState
	{
		Normal,
		Best,
		Leader

	}

	public WinState winState = WinState.Normal;

public void SetWinState()
	{

		switch(winState)
		{
		case WinState.Normal: break;
		case WinState.Best : break;
		case WinState.Leader : break;

		}

	}


	public void SetTxt(string Sc, string Lsc, string Tm, string Ltm, string Pn)
	{

		ScoreTxt.text = Sc;
		LeaderScoreTxt.text = Lsc;
		
		TimeTxt.text = Tm;
		LeaderTimeTxt.text = Ltm;
		
		PegNumberTxt.text = Pn;

	}

	
}
