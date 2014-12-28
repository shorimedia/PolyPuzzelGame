#if UNITY_ANDROID

namespace UnityEngine.Advertisements {
  using UnityEngine;
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine.Advertisements;

  internal class UnityAdsAndroid : UnityAdsPlatform {
	private static AndroidJavaObject unityAds;
	private static AndroidJavaObject unityAdsUnity;
	private static AndroidJavaObject currentActivity;
	private static bool wrapperInitialized = false;

	private AndroidJavaObject getAndroidWrapper() {
		if(!wrapperInitialized) {
			wrapperInitialized = true;
			unityAdsUnity = new AndroidJavaObject("com.unity3d.ads.android.unity3d.UnityAdsUnityWrapper");
		}

		return unityAdsUnity;
	}

	public override void init (string gameId, bool testModeEnabled, string gameObjectName) {
		Utils.LogDebug("UnityAndroid: init(), gameId=" + gameId + ", testModeEnabled=" + testModeEnabled + ", gameObjectName=" + gameObjectName);
		
		if(Advertisement.UnityDeveloperInternalTestMode) {
			getAndroidWrapper().Call("enableUnityDeveloperInternalTestMode");
		}

		currentActivity = (new AndroidJavaClass("com.unity3d.player.UnityPlayer")).GetStatic<AndroidJavaObject>("currentActivity");
		getAndroidWrapper().Call("init", gameId, currentActivity, testModeEnabled, (int) Advertisement.debugLevel, gameObjectName);
	}
		
	public override bool show (string zoneId, string rewardItemKey, string options) {
    Utils.LogDebug ("UnityAndroid: show()");
		return getAndroidWrapper().Call<bool>("show", zoneId, rewardItemKey, options);
	}
		
	public override void hide () {
      Utils.LogDebug ("UnityAndroid: hide()");
		getAndroidWrapper().Call("hide");
	}
		
	public override bool isSupported () {
      Utils.LogDebug ("UnityAndroid: isSupported()");
		return getAndroidWrapper().Call<bool>("isSupported");
	}
		
	public override string getSDKVersion () {
      Utils.LogDebug ("UnityAndroid: getSDKVersion()");
		return getAndroidWrapper().Call<string>("getSDKVersion");
	}
		
	public override bool canShowAds (string network) {
      return getAndroidWrapper().Call<bool>("canShowAds", network);
	}
		
	public override bool canShow () {
      Utils.LogDebug ("UnityAndroid: canShow()");
		return getAndroidWrapper().Call<bool>("canShow");
	}
		
	public override bool hasMultipleRewardItems () {
      Utils.LogDebug ("UnityAndroid: hasMultipleRewardItems()");
		return getAndroidWrapper().Call<bool>("hasMultipleRewardItems");
	}
		
	public override string getRewardItemKeys () {
      Utils.LogDebug ("UnityAndroid: getRewardItemKeys()");
		return getAndroidWrapper().Call<string>("getRewardItemKeys");
	}
		
	public override string getDefaultRewardItemKey () {
      Utils.LogDebug ("UnityAndroid: getDefaultRewardItemKey()");
		return getAndroidWrapper().Call<string>("getDefaultRewardItemKey");
	}
		
	public override string getCurrentRewardItemKey () {
      Utils.LogDebug ("UnityAndroid: getCurrentRewardItemKey()");
		return getAndroidWrapper().Call<string>("getCurrentRewardItemKey");
	}
		
	public override bool setRewardItemKey (string rewardItemKey) {
      Utils.LogDebug("UnityAndroid: setRewardItemKey() rewardItemKey=" + rewardItemKey);
		return getAndroidWrapper().Call<bool>("setRewardItemKey", rewardItemKey);
	}
		
	public override void setDefaultRewardItemAsRewardItem () {
      Utils.LogDebug ("UnityAndroid: setDefaultRewardItemAsRewardItem()");
		getAndroidWrapper().Call("setDefaultRewardItemAsRewardItem");
	}
		
	public override string getRewardItemDetailsWithKey (string rewardItemKey) {
      Utils.LogDebug ("UnityAndroid: getRewardItemDetailsWithKey() rewardItemKey=" + rewardItemKey);
		return getAndroidWrapper().Call<string>("getRewardItemDetailsWithKey", rewardItemKey);
	}
		
	public override string getRewardItemDetailsKeys () {
      Utils.LogDebug ("UnityAndroid: getRewardItemDetailsKeys()");
		return getAndroidWrapper().Call<string>("getRewardItemDetailsKeys");
	}

  public override void setNetworks(HashSet<string> networks) {
      string networksString = Utils.Join(networks, ",");
      Utils.LogDebug("UnityAndroid: setNetworks: " + networksString);
      getAndroidWrapper().CallStatic("setNetworks", networksString);
  }

  public override void setNetwork(string network) {
      Utils.LogDebug("UnityAndroid: setNetwork()");
		getAndroidWrapper().Call("setNetwork", network);
  }

	public override void setLogLevel(Advertisement.DebugLevel logLevel) {
		Utils.LogDebug("UnityAndroid: setLogLevel()");
		getAndroidWrapper().Call("setLogLevel", (int) logLevel);
	}
  }
}

#endif
