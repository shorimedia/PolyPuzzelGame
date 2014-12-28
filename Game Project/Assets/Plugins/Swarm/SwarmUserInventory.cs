using UnityEngine;
using System.Collections;
using System;

public class SwarmUserInventory : MonoBehaviour {
	
	#if UNITY_ANDROID
		#if !UNITY_EDITOR
			/**
			 * Reference the SwarmUnityInterface class
			 */
			public static IntPtr swarmUnityInterface = AndroidJNI.FindClass("com/swarmconnect/SwarmUnityInterface");
			
			/**
			 * Reference the necessary SwarmUnityInterface methods
			 */
			public static IntPtr getItemQuantityMethod = AndroidJNI.GetStaticMethodID(swarmUnityInterface, "getItemQuantity", "(ILjava/lang/String;)V");
			public static IntPtr consumeItemMethod = AndroidJNI.GetStaticMethodID(swarmUnityInterface, "consumeItem", "(I)V");
		#endif
	#endif
	
	private System.Action<int> callbackAction;
	
	
	/** 
	 * Get the quantity of a an item based on the item id number.
	 * 
	 * @param itemId The id of the item to check the quantity of.
	 * @param action The action to be performed (delegate to be called) when the callback is complete (the user data is returned).
	 */			
	public static void getItemQuantity (int itemId, System.Action<int> action) {
	
		#if UNITY_ANDROID
			#if !UNITY_EDITOR	
				string objName = "SwarmUserInventoryGetItemQuantity."+DateTime.Now.Ticks;
				GameObject gameObj = new GameObject(objName);
				DontDestroyOnLoad(gameObj);
		
				SwarmUserInventory component = gameObj.AddComponent<SwarmUserInventory>();
				component.callbackAction = action;
		
				AndroidJavaObject callback = new AndroidJavaObject("java.lang.String", objName);
		
				jvalue[] args = new jvalue[2];
				args[0].i = itemId;
				args[1].l = callback.GetRawObject();		
		
				AndroidJNI.CallStaticVoidMethod(swarmUnityInterface, getItemQuantityMethod, args);
			#endif
		#endif		
	}
	
	
	/**
	 * This callback is called when the item quantity data is returned from the server
	 */
	public void gotItemQuantity(string quantity) {

		if (callbackAction != null) {
			
			try {
				callbackAction(Convert.ToInt32(quantity));
			} catch (Exception e) {
				Debug.Log(e.StackTrace);
				callbackAction(0);
			}
		}
		
		GameObject.Destroy(this.gameObject);
	}
	
	
	/**
	 * Consume the item with the requested id number.
	 * 
	 * @param itemId The id number of the item to consume.
	 */		
	public static void consumeItem (int itemId) {
		
		#if UNITY_ANDROID
			#if !UNITY_EDITOR		
				jvalue[] args = new jvalue[1];
				args[0].i = itemId;		
				
				AndroidJNI.CallStaticVoidMethod(swarmUnityInterface, consumeItemMethod, args);
			#endif
		#endif
	}
	
}
