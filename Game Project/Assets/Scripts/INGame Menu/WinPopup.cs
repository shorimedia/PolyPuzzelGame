using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinPopup : MonoBehaviour {

	public Text ScoreTxt;
	public Text LeaderScoreTxt;
	public Text TimeTxt;
	public Text LeaderTimeTxt;
	public Text PegNumberTxt;

    public Image[] star = new Image[3];


	// Sounds
	public string OpenNormal, OpenBest, OpenLeader;


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
		case WinState.Normal: SoundManager.PlaySFX(OpenNormal); break;
		case WinState.Best : SoundManager.PlaySFX(OpenBest); break;
		case WinState.Leader : SoundManager.PlaySFX(OpenLeader); break;

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


    public void SetPegNum(int num)
    {
        PegNumberTxt.text = num.ToString();
    }


    public void SetStars(int starNum) // the image to show stars or lock
    {

        // starNum is number of pegs on board at the end of game

        if (starNum > 3)
        {
            starNum = 0;
        }



        switch (starNum)
        {
            case 0:
                star[0].enabled = false;
                star[1].enabled = false;
                star[2].enabled = false;
                break;
            case 3:
                star[0].enabled = true;
                star[1].enabled = false;
                star[2].enabled = false;
                break;
            case 2:
                star[0].enabled = true;
                star[1].enabled = true;
                star[2].enabled = false;
                break;
            case 1:
                star[0].enabled = true;
                star[1].enabled = true;
                star[2].enabled = true;
                break;

        }


    }

	
}
