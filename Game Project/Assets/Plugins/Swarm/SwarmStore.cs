using UnityEngine;
using System.Collections;
using System;

/**
 * 
 * @author SwarmConnect
 * The SwarmStore "Proxy" class masking the Android Java code.
 *
 */
public class SwarmStore : MonoBehaviour {
	
	public static int PURCHASE_FAILED = 0;
	public static int PURCHASE_SUCCEEDED = 1;
	
	public enum CoinProvider {
		TAPJOY = 1,
		PAYPAL = 2,
		WALLET = 3
	}
	
	#if UNITY_ANDROID
		#if !UNITY_EDITOR
			/**
			 * Reference the SwarmUnityInterface class
			 */
			public static IntPtr swarmUnityInterface = AndroidJNI.FindClass("com/swarmconnect/SwarmUnityInterface");
		
			/**
			 * Reference the necessary SwarmUnityInterface methods
			 */
			public static IntPtr purchaseItemListingMethod = AndroidJNI.GetStaticMethodID(swarmUnityInterface, "purchaseItemListing", "(ILjava/lang/String;)V");
			public static IntPtr addCoinProviderMethod = AndroidJNI.GetStaticMethodID (swarmUnityInterface, "addCoinProvider", "(Ljava/lang/String;)V");
			public static IntPtr removeCoinProviderMethod = AndroidJNI.GetStaticMethodID (swarmUnityInterface, "removeCoinProvider", "(Ljava/lang/String;)V");
	
		#endif
	#endif
	
	private System.Action<int> callbackAction;
		
	/**
	 * Purchase an item based on a listing id number.
	 * 
	 * @param listingId The id number of the item listing of which to purchase.
	 * @param action The action to be performed (delegate to be called) when the callback is complete (the user data is returned).
	 */	
	public static void purchaseItemListing (int listingId, System.Action<int> action) {

		#if UNITY_ANDROID
			#if !UNITY_EDITOR		
				string objName = "SwarmStorePurchaseItemListing."+listingId+"."+DateTime.Now.Ticks;
				GameObject gameObj = new GameObject(objName);
				DontDestroyOnLoad(gameObj);
				SwarmStore component = gameObj.AddComponent<SwarmStore>();
				component.callbackAction = action;
			
				AndroidJavaObject callback = new AndroidJavaObject("java.lang.String", objName);
				
				jvalue[] args = new jvalue[2];
				args[0].i = listingId;
				args[1].l = callback.GetRawObject();		
		
				AndroidJNI.CallStaticVoidMethod(swarmUnityInterface, purchaseItemListingMethod, args);
			#endif
		#endif
	}
	
	/* Add a coin provider.  Options are PAYPAL, TAPJOY, and WALLET (Google Wallet). Note that the default providers are TAPJOY and WALLET */
	public static void addCoinProvider(CoinProvider provider) {
	
		#if UNITY_ANDROID
			#if !UNITY_EDITOR
				AndroidJavaObject jprovider = new AndroidJavaObject("java.lang.String", provider.ToString());
		
				jvalue[] args = new jvalue[1];
				args[0].l = jprovider.GetRawObject();
				AndroidJNI.CallStaticVoidMethod (swarmUnityInterface, addCoinProviderMethod, args);		
			#endif
		#endif
	}
	
	/* Remove a coin providerOptions are PAYPAL, TAPJOY, and WALLET (Google Wallet). Note that the default providers are Tapjoy and Google Wallet */
	public static void removeCoinProvider(CoinProvider provider) {
	
		#if UNITY_ANDROID
			#if !UNITY_EDITOR
				AndroidJavaObject jprovider = new AndroidJavaObject("java.lang.String", provider.ToString());
		
				jvalue[] args = new jvalue[1];
				args[0].l = jprovider.GetRawObject();
				AndroidJNI.CallStaticVoidMethod (swarmUnityInterface, removeCoinProviderMethod, args);		
			#endif
		#endif
	}	
	
	
	/**
	 * This callback is called when the item is purchased.
	 * 
	 * @param result Returns "Purchase Successful" if the purchase is successful, returns "Purchase Failed" otherwise
	 */ 
	public void itemPurchased(string statusCode) {

		if (callbackAction != null) {
			
			if (statusCode != null) {
				
				if (statusCode == "SUCCESS") {
					callbackAction(PURCHASE_SUCCEEDED);
				} else {
					callbackAction(PURCHASE_FAILED);
				}
			} 
		}
		
		GameObject.Destroy(this.gameObject);
	}
}