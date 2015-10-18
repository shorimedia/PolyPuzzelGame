using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class GooglePlay_Menu : MonoBehaviour {

	public Text LoginLabel;
    public Toggle CheckMark;

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
	

		PlayGamesPlatform.Activate();
		//LoginLabel.text = "Sign in with Google Play";

        // authenticate user:
        GoogleSignIn();

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
                CheckMark.isOn = true;

			}else{

				Debug.Log("Login failed");
                LoginLabel.text = "Sign in with Google Play";
                CheckMark.isOn = false;
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
