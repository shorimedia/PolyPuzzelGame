using UnityEngine;
using System.Collections;
using System;

/**
 * 
 * @author SwarmConnect
 * The Swarm "Proxy" class masking the Android Java code.
 *
 */
public class Swarm : MonoBehaviour {
	
	#if UNITY_ANDROID
		#if !UNITY_EDITOR
			/**
			 * Reference the SwarmUnityInterface class
			 */
			public static IntPtr swarmUnityInterface = AndroidJNI.FindClass("com/swarmconnect/SwarmUnityInterface");
		
			/**
			 * Reference the necessary SwarmUnityInterface methods
			 */
			public static IntPtr initMethod = AndroidJNI.GetStaticMethodID(swarmUnityInterface, "init", "(ILjava/lang/String;)V");
			public static IntPtr showLoginMethod = AndroidJNI.GetStaticMethodID(swarmUnityInterface, "showLogin", "()V");
			public static IntPtr showAchievementsMethod = AndroidJNI.GetStaticMethodID(swarmUnityInterface, "showAchievements", "()V");
			public static IntPtr showLeaderboardsMethod = AndroidJNI.GetStaticMethodID(swarmUnityInterface, "showLeaderboards", "()V");
			public static IntPtr showStoreMethod = AndroidJNI.GetStaticMethodID(swarmUnityInterface, "showStore", "()V");
			public static IntPtr showDashboardMethod = AndroidJNI.GetStaticMethodID(swarmUnityInterface, "showDashboard", "()V");
			public static IntPtr isEnabledMethod = AndroidJNI.GetStaticMethodID(swarmUnityInterface, "isEnabled", "()Z");
			public static IntPtr isLoggedInMethod = AndroidJNI.GetStaticMethodID(swarmUnityInterface, "isLoggedIn", "()Z");
			public static IntPtr isInitializedMethod = AndroidJNI.GetStaticMethodID(swarmUnityInterface, "isInitialized", "()Z");
			public static IntPtr disableNotificationPopupsMethod = AndroidJNI.GetStaticMethodID(swarmUnityInterface, "disableNotificationPopups", "()V");	
			public static IntPtr enableNotificationPopupsMethod = AndroidJNI.GetStaticMethodID(swarmUnityInterface, "enableNotificationPopups", "()V");	
			public static IntPtr enableAlternativeMarketCompatabilityMethod = AndroidJNI.GetStaticMethodID(swarmUnityInterface, "enableAlternativeMarketCompatability", "()V");
	
		#endif
	#endif
	
	/**
	 * Initializes Swarm and shows the login screen or logs user in (if previously logged in).
	 * 
	 * @param  appId The application Id number.
	 * @param  appAuth The specific authorization key for the app.
	 */	
	public static void init (int appID, string appAuth){
		
		#if UNITY_ANDROID
			#if !UNITY_EDITOR
				Debug.Log("Swarm method pointer: "+initMethod);
		
				AndroidJavaObject authString = new AndroidJavaObject("java.lang.String", appAuth);
				jvalue[] args = new jvalue[2];
				args[0].i = appID;
				args[1].l = authString.GetRawObject();
				
				AndroidJNI.CallStaticVoidMethod(swarmUnityInterface, initMethod, args);
			#endif
		#endif
	}
	
	/**
	 * Show the Swarm Login screen in a pop up. 
	 */		
	public static void showLogin (){
		
		#if UNITY_ANDROID
			#if !UNITY_EDITOR
				jvalue[] args = new jvalue[0];
		
				AndroidJNI.CallStaticVoidMethod(swarmUnityInterface, showLoginMethod, args);
			#endif
		#endif
	}		
	
	/**
	 * Show the Swarm Achievements screen in a pop up. 
	 */		
	public static void showAchievments (){
		
		#if UNITY_ANDROID
			#if !UNITY_EDITOR
				jvalue[] args = new jvalue[0];
		
				AndroidJNI.CallStaticVoidMethod(swarmUnityInterface, showAchievementsMethod, args);
			#endif
		#endif
	}
	
	/**
	 * Show the Swarm Leaderboards screen in a pop up. 
	 */	
	public static void showLeaderboards (){
			
		#if UNITY_ANDROID
			#if !UNITY_EDITOR
				jvalue[] args = new jvalue[0];
		
				AndroidJNI.CallStaticVoidMethod(swarmUnityInterface, showLeaderboardsMethod, args);
			#endif
		#endif
	}
	
	/**
	 * Show the Swarm Store screen in a pop up. 
	 */	
	public static void showStore() {
		
		#if UNITY_ANDROID
			#if !UNITY_EDITOR
				jvalue[] args = new jvalue[0];
		
				AndroidJNI.CallStaticVoidMethod(swarmUnityInterface, showStoreMethod, args);
			#endif
		#endif
	}
	
	/**
	 * Show the Swarm Dashboard in a pop up.  
	 */		
	public static void showDashboard () {
		
		#if UNITY_ANDROID
			#if !UNITY_EDITOR		
				jvalue[] args = new jvalue[0];
		
				AndroidJNI.CallStaticVoidMethod(swarmUnityInterface, showDashboardMethod, args);	
			#endif
		#endif		
	}
	
	/**
	 * Returns true if Swarm is enabled (init has been called), returns false otherwise.
	 */		
	public static bool isEnabled () {
		#if UNITY_ANDROID
			#if !UNITY_EDITOR
				jvalue[] args = new jvalue[0];
		
				return AndroidJNI.CallStaticBooleanMethod(swarmUnityInterface, isEnabledMethod, args);
			#endif
		#endif	
		
		return false;
	}
	
	/**
	 * Returns true if the user is logged in, returns false otherwise.
	 */		
	public static bool isLoggedIn () {
		
		#if UNITY_ANDROID
			#if !UNITY_EDITOR
				jvalue[] args = new jvalue[0];
		
				return AndroidJNI.CallStaticBooleanMethod(swarmUnityInterface, isLoggedInMethod, args);
			#endif
		#endif
		
		return false;
	}	

	/**
	 * Returns true if Swarm is initialized, returns false otherwise.
	 */		
	public static bool isInitialized () {
		
		#if UNITY_ANDROID
			#if !UNITY_EDITOR
				jvalue[] args = new jvalue[0];
		
				return AndroidJNI.CallStaticBooleanMethod(swarmUnityInterface, isInitializedMethod, args);
			#endif
		#endif
		
		return false;
	}
	
	/**
	 * Disable notification popups (i.e, popups shown when a score is submitted, an achievement is earned, etc.)
	 */		
	public static void disableNotificationPopups (){
		
		#if UNITY_ANDROID
			#if !UNITY_EDITOR
				jvalue[] args = new jvalue[0];
		
				AndroidJNI.CallStaticVoidMethod(swarmUnityInterface, disableNotificationPopupsMethod, args);
			#endif
		#endif
	}	
	
	/**
	 * Enable notification popups (i.e, popups shown when a score is submitted, an achievement is earned, etc.)
	 */		
	public static void enableNotificationPopups (){
		
		#if UNITY_ANDROID
			#if !UNITY_EDITOR
				jvalue[] args = new jvalue[0];
		
				AndroidJNI.CallStaticVoidMethod(swarmUnityInterface, enableNotificationPopupsMethod, args);
			#endif
		#endif
	}		
	
	/**
	 * Enable alternative market compatibility (example: Amazon Marketplace).  Calling this function make
	 * your game or app compatible with the requirements for launching on non-google play markets.
	 */
	public static void enableAlternativeMarketCompatability() {
		
		#if UNITY_ANDROID
			#if !UNITY_EDITOR
				jvalue[] args = new jvalue[0];
		
				AndroidJNI.CallStaticVoidMethod(swarmUnityInterface, enableAlternativeMarketCompatabilityMethod, args);
			#endif
		#endif		
	}
}
