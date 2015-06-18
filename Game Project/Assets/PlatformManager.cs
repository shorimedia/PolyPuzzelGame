using UnityEngine;
using System.Collections;

public class PlatformManager : MonoBehaviour {

	public enum GameStatus
	{
		Free,
		Full,
		Demo
	}


	public GameStatus gameStatus = GameStatus.Demo;


	public int maxLevelNum = 5;

	public string purchaseLink = "https://play.google.com/store/apps/details?id=com.supercell.clashofclans";


	public bool hasAds = false;

	public FadeScreen endScreen;
	public FadeScreen Background;

	// Use this for initialization
	void Start () 
	{

	}


	public bool SetGameStatus(int levelNum)
	{
		bool CanPlay = false;

		switch(gameStatus)
		{
		case GameStatus.Free : 
			hasAds = true;


			if( levelNum == maxLevelNum)
			{
				// End Game
				Background.IsOpen = true;
				endScreen.IsOpen = true;
				CanPlay = false;
			}else{
				CanPlay = true;
			}


			break;
		case GameStatus.Full :
			hasAds = false;
			CanPlay = true;
			break;
		case GameStatus.Demo : 
			hasAds = true;
			if( levelNum == maxLevelNum)
			{
				// End Game
				Background.IsOpen = true;
				endScreen.IsOpen = true;
				CanPlay = false;
			}else{
				CanPlay = true;
			}
			
			break;
		}


		return CanPlay;
	}



	public  void  PurchaseGame()
	{
		Application.OpenURL(purchaseLink);
	}



}
