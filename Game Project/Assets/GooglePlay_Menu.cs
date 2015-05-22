using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class GooglePlay_Menu : MonoBehaviour {

	public Text LoginLabel;

	private bool isLogin = false;



	public bool IsLoggedin
	{
		get{return isLogin;}
		set{isLogin = value;}
	}


	public void SetLogin()
	{

		if(isLogin)
		{
			GoogleSignIn(); 
		}else{

			LoginLabel.text = "Sign in with Google Play";
			GoogleSignOut(); 
		}

	}



	// Use this for initialization
	void Start () {
	
//		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
//			// enables saving game progress.
//			.EnableSavedGames()
//				// registers a callback to handle game invitations received while the game is not running.
//				.WithInvitationDelegate(<callback method>)
//				// registers a callback for turn based match notifications received while the
//				// game is not running.
//				.WithMatchDelegate(<callback method>)
//				.Build();
//		
//		PlayGamesPlatform.InitializeInstance(config);
//		// recommended for debugging:
//		PlayGamesPlatform.DebugLogEnabled = true;
		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate();
		LoginLabel.text = "Sign in with Google Play";

	}
	
	// Sign In to Google play
	public void GoogleSignIn () 
	{

		// authenticate user:
		Social.localUser.Authenticate((bool success) => {
			// handle success or failure
			if(success)
			{
				Debug.Log("You've Succeddfully logged in");
				LoginLabel.text = "You are signed in with Google Play";

			}else{

				Debug.Log("Login failed");
			}


		});
	
	}

	// Sign Out to Google play
	public void GoogleSignOut () 
	{
		((PlayGamesPlatform)Social.Active).SignOut();

	}


	public void ShowLeaderboards()
	{
		Social.ShowLeaderboardUI();
	}



	public void ShowAchievements()
	{
		Social.ShowAchievementsUI();
	}

	
}
