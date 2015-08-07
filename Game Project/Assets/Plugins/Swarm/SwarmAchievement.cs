using UnityEngine;
using System.Collections;
using System;

/**
 * 
 * @author SwarmConnect
 * The SwarmAchievement "Proxy" class masking the Android Java code.
 *
 */
public class SwarmAchievement : MonoBehaviour {
	
	#if UNITY_ANDROID
		#if !UNITY_EDITOR	
			/**
			 * Reference the SwarmUnityInterface class
			 */
			public static IntPtr swarmUnityInterface = AndroidJNI.FindClass("com/swarmconnect/SwarmUnityInterface");
			
			/**
			 * Reference the necessary SwarmUnityInterface methods
			 */
			public static IntPtr unlockMethod = AndroidJNI.GetStaticMethodID(swarmUnityInterface, "unlockAchievement", "(I)V");
			public static IntPtr unlockAndShowMethod = AndroidJNI.GetStaticMethodID (swarmUnityInterface, "unlockAchievementAndShowAchievements", "(I)V");
		#endif
	#endif
	
	/**
	 * Unlock an achievement using the achievement Id number. 
	 * 
	 * @param achievementId The Id number of the achievement to unlock.
	 */		
	public static void unlockAchievement (int achievementID){
		
		#if UNITY_ANDROID
			#if !UNITY_EDITOR
				jvalue[] args = new jvalue[1];
				args[0].i = achievementID;	
				
				AndroidJNI.CallStaticVoidMethod(swarmUnityInterface, unlockMethod, args);
			#endif
		#endif
	}
	
	/**
	 * Unlock an achievement using the achievement Id number and immediately show the achievements list
	 * 
	 * @param achievementId The Id number of the achievement to unlock.
	 */
	 public static void unlockAchievementAndShowAchievements (int achievementID) {
		
		#if UNITY_ANDROID
			#if !UNITY_EDITOR
				jvalue[] args = new jvalue[1];
				args[0].i = achievementID;	
				
				AndroidJNI.CallStaticVoidMethod(swarmUnityInterface, unlockAndShowMethod, args);
			#endif
		#endif	
	}
	
}
