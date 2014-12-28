using UnityEngine;
using System.Collections;
using System;

/**
 * 
 * @author SwarmConnect
 * The SwarmLeaderboard "Proxy" class masking the Android Java code.
 *
 */
public class SwarmLeaderboard : MonoBehaviour {
	
	#if UNITY_ANDROID
		#if !UNITY_EDITOR
			/**
			 * Reference the SwarmUnityInterface class
			 */
			public static IntPtr swarmUnityInterface = AndroidJNI.FindClass("com/swarmconnect/SwarmUnityInterface");
			
			/**
			 * Reference the necessary SwarmUnityInterface methods
			 */
			public static IntPtr submitScoreMethod = AndroidJNI.GetStaticMethodID(swarmUnityInterface, "submitScore", "(IF)V");
			public static IntPtr submitScoreGetRankMethod = AndroidJNI.GetStaticMethodID(swarmUnityInterface, "submitScore", "(IFLjava/lang/String;)V");
			public static IntPtr showLeaderboardMethod = AndroidJNI.GetStaticMethodID(swarmUnityInterface, "showLeaderboard", "(I)V");
			public static IntPtr submitScoreAndShowLeaderboardMethod = AndroidJNI.GetStaticMethodID(swarmUnityInterface, "submitScoreAndShowLeaderboard", "(IF)V");
		#endif
	#endif
	
	
	private System.Action<int> callbackAction;
	
	
	/**
	 * Submit a score to the leaderboard for ranking.
	 * 
	 * @param leadeboardId The Id number of the leaderboard you want to submit to.
	 * @param score The player's score to be submitted.
	 */		
	public static void submitScore (int leaderboardId, float score) {
		
		#if UNITY_ANDROID
			#if !UNITY_EDITOR		
				jvalue[] args = new jvalue[2];
				args[0].i = leaderboardId;
				args[1].f = score;
				
				AndroidJNI.CallStaticVoidMethod(swarmUnityInterface, submitScoreMethod, args);
			#endif
		#endif
	}

	/**
	 * Submit a score to the leaderboard for ranking and get the user's rank back asynchronously.
	 * 
	 * @param leadeboardId The Id number of the leaderboard you want to submit to.
	 * @param score The player's score to be submitted.
	 * @param action The action to be performed (delegate to be called) when the callback is complete (the user data is returned).
	 */		
	public static void submitScoreGetRank (int leaderboardId, float score, System.Action<int> action) {
		
		#if UNITY_ANDROID
			#if !UNITY_EDITOR
				string objName = "SwarmLeaderboard."+leaderboardId+"."+score+"."+DateTime.Now.Ticks;
				GameObject gameObj = new GameObject(objName);
				DontDestroyOnLoad(gameObj);
				SwarmLeaderboard component = gameObj.AddComponent<SwarmLeaderboard>();
				component.callbackAction = action;
		
				AndroidJavaObject callback = new AndroidJavaObject("java.lang.String", objName);
		
				jvalue[] args = new jvalue[3];
				args[0].i = leaderboardId;
				args[1].f = score;
				args[2].l = callback.GetRawObject();

				AndroidJNI.CallStaticVoidMethod(swarmUnityInterface, submitScoreGetRankMethod, args);
			#endif
		#endif
	}
	
	/**
	 * Display the leaderboard with the specified Id number.
	 * 
	 * @param leadeboardId The Id number of the leaderboard you want to display.
	 */	
	public static void showLeaderboard (int leaderboardId) {
		
		#if UNITY_ANDROID
			#if !UNITY_EDITOR		
				jvalue[] args = new jvalue[1];
				args[0].i = leaderboardId;
		
				AndroidJNI.CallStaticVoidMethod(swarmUnityInterface, showLeaderboardMethod, args);
			#endif
		#endif	
	}
	
	/**
	 * Submit a score to the leaderboard for ranking.
	 * 
	 * @param leadeboardId The Id number of the leaderboard you want to submit to.
	 * @param score The player's score to be submitted.
	 */		
	public static void submitScoreAndShowLeaderboard (int leaderboardId, float score){
		
		#if UNITY_ANDROID
			#if !UNITY_EDITOR			
				jvalue[] args = new jvalue[2];
				args[0].i = leaderboardId;
				args[1].f = score;
				
				AndroidJNI.CallStaticVoidMethod(swarmUnityInterface, submitScoreAndShowLeaderboardMethod, args);
			#endif
		#endif	
	}	
	
	/**
	 * This callback is called when the rank data is returned from the server
	 */
	public void gotRank(string rank) {
		
		if (callbackAction != null) {
			
			try {
				callbackAction(Convert.ToInt32(rank));
			} catch (Exception e) {
				Debug.Log(e.StackTrace);
				callbackAction(0);
			}
		}
		
		GameObject.Destroy(this.gameObject);
	}
	
}
