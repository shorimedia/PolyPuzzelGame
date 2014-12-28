using UnityEngine;
using System.Collections;
using System;

/**
 * 
 * @author SwarmConnect
 * The SwarmActiveUser "Proxy" class masking the Android Java code.
 *
 */
public class SwarmActiveUser : MonoBehaviour {
	
	#if UNITY_ANDROID
		#if !UNITY_EDITOR	
			/**
			 * Reference the SwarmUnityInterface class
			 */
			public static IntPtr swarmUnityInterface = AndroidJNI.FindClass("com/swarmconnect/SwarmUnityInterface");
			
			/**
			 * Reference the necessary SwarmUnityInterface methods
			 */
			public static IntPtr saveCloudData = AndroidJNI.GetStaticMethodID(swarmUnityInterface, "saveCloudData", "(Ljava/lang/String;Ljava/lang/String;)V");
			public static IntPtr getCloudData = AndroidJNI.GetStaticMethodID(swarmUnityInterface, "getCloudData", "(Ljava/lang/String;Ljava/lang/String;)V");
			public static IntPtr getUsernameMethod = AndroidJNI.GetStaticMethodID(swarmUnityInterface, "getUsername", "()Ljava/lang/String;");	
		#endif
	#endif
	
	private System.Action<string> callbackAction;
	
	/**
	 * Save any data of type string to the user's cloud data
	 * 
	 * @param key The unique identifier of the object created on the server.
	 * @param data The string data that is being saved to the server.
	 */		
	public static void saveUserData (string key, string data) {
		
		#if UNITY_ANDROID
			#if !UNITY_EDITOR
				AndroidJavaObject jdata = new AndroidJavaObject("java.lang.String", data);
				AndroidJavaObject jkey = new AndroidJavaObject("java.lang.String", key);
				
				jvalue[] args = new jvalue[2];
				args[0].l = jkey.GetRawObject();
				args[1].l = jdata.GetRawObject();
				
				AndroidJNI.CallStaticVoidMethod(swarmUnityInterface, saveCloudData, args);		
			#endif
		#endif
	}

	/**
	 * Retrieve data of type string from the user's cloud data
	 * 
	 * @param key The unique identifier of the object created on the server.
	 * @param action The action to be performed (delegate to be called) when the callback is complete (the user data is returned).
	 */			
	public static void getUserData (string key, System.Action<string> action) {
		
		#if UNITY_ANDROID
			#if !UNITY_EDITOR		
				string objName = "SwarmActiveUserGetData."+key+"."+DateTime.Now.Ticks;
				GameObject gameObj = new GameObject(objName);
				DontDestroyOnLoad(gameObj);
				SwarmActiveUser component = gameObj.AddComponent<SwarmActiveUser>();
				component.callbackAction = action;
		
				AndroidJavaObject callback = new AndroidJavaObject("java.lang.String", objName);
				AndroidJavaObject jkey = new AndroidJavaObject("java.lang.String", key);
		
				jvalue[] args = new jvalue[2];
				args[0].l = jkey.GetRawObject();
				args[1].l = callback.GetRawObject();
				
				AndroidJNI.CallStaticVoidMethod(swarmUnityInterface, getCloudData, args);
			#endif
		#endif		
	}
	
	/*
	 * Get the username of the currently logged in user.
	 */
	public static string getUsername () {
		
		#if UNITY_ANDROID
			#if !UNITY_EDITOR
				jvalue[] args = new jvalue[0];
				
				return AndroidJNI.CallStaticStringMethod(swarmUnityInterface, getUsernameMethod, args);
			#endif
		#endif	
		
		return "";
	}
	
	
	/*
	 * This callback is called when data is returned from the server.
	 */	
	public void gotData(string data) {
		if (callbackAction != null) {
			callbackAction(data);
		}
		GameObject.Destroy(this.gameObject);
	}

}