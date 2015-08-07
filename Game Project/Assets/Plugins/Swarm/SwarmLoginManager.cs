using UnityEngine;
using System.Collections;
using System;

/**
 * 
 * @author SwarmConnect
 * The SwarmLoginManager "Proxy" class masking the Android Java code.
 *
 */
public class SwarmLoginManager : MonoBehaviour {
	public const int LOGIN_STARTED = 1;
	public const int LOGIN_CANCELED = 2;
	public const int USER_LOGGED_IN = 3;
	public const int USER_LOGGED_OUT = 4;
	
	#if UNITY_ANDROID
		#if !UNITY_EDITOR
			/**
			 * Reference the SwarmUnityInterface class
			 */
			public static IntPtr swarmUnityInterface = AndroidJNI.FindClass("com/swarmconnect/SwarmUnityInterface");
			
			/**
			 * Reference the necessary SwarmUnityInterface methods
			 */
			public static IntPtr addLoginListenerMethod = AndroidJNI.GetStaticMethodID(swarmUnityInterface, "addLoginListener", "(Ljava/lang/String;)V");
		#endif
	#endif
	
	private System.Action<int> callbackAction;
	
	/**
	 * Adds a login listener to report the status of a login.
	 * 
	 * @param action The action to be performed (delegate to be called) when the callback is complete (the user data is returned).
	 * 
	 * SwarmLoginListener enables the developer to listen for various events during the login process.
	 * loginCancelled is called if the user intentionally cancels the login.
	 * loginStarted is called when the login process has started (when a login dialog is displayed to the user).
	 * userLoggedIn is called when the user has successfully logged in.
	 * userLoggedOut is called when the user logs out for any reason.
	 */	
	public static void addLoginListener(System.Action<int> action) {
		
		#if UNITY_ANDROID
			#if !UNITY_EDITOR
				string objName = "SwarmLoginManagerAddListener."+DateTime.Now.Ticks;
				GameObject gameObj = new GameObject(objName);
				DontDestroyOnLoad(gameObj);
				SwarmLoginManager component = gameObj.AddComponent<SwarmLoginManager>();
				component.callbackAction = action;
		
				AndroidJavaObject callback = new AndroidJavaObject("java.lang.String", objName);
				
				jvalue[] args = new jvalue[1];
				args[0].l = callback.GetRawObject();
				
				AndroidJNI.CallStaticVoidMethod(swarmUnityInterface, addLoginListenerMethod, args);
			#endif
		#endif		
	}
	
	
	/*
	 * Callbacks for Java --> C#
	 */
	
	/*
	 * This callback is called if the user intentionally cancels logging in.
	 */
	public void loginCanceled(string junk) {
		if (callbackAction != null) {
			callbackAction(LOGIN_CANCELED);
		}
	}
	
	/*
	 * This callback is called when the login process has started (when a login dialog is displayed to the user).
	 */	
	public void loginStarted(string junk) {
		if (callbackAction != null) {
			callbackAction(LOGIN_STARTED);
		}
	}
	
	/*
	 * This callback is called when the user has successfully logged in. 
	 */
	public void userLoggedIn(string junk) {
		if (callbackAction != null) {
			callbackAction(USER_LOGGED_IN);
		}
	}
	
	/*
	 * This callback is calledwhen the user logs out for any reason.
	 */	
	public void userLoggedOut(string junk) {
		if (callbackAction != null) {
			callbackAction(USER_LOGGED_OUT);
		}
	}
	
}