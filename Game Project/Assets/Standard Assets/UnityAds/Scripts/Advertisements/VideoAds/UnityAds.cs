namespace UnityEngine.Advertisements {
  using UnityEngine;
  using System.Collections;
  using System.Collections.Generic;

  internal class UnityAds : MonoBehaviour {

    private static UnityAds sharedInstance;
    private static bool _adsShow = false;

    private static HashSet<string> _campaignsAvailable = new HashSet<string>();
    
    private static string _rewardItemNameKey = "";
    private static string _rewardItemPictureKey = "";
    
    public delegate void UnityAdsCampaignsAvailable();
    public static UnityAdsCampaignsAvailable OnCampaignsAvailable;
  
    public delegate void UnityAdsCampaignsFetchFailed();
    public static UnityAdsCampaignsFetchFailed OnCampaignsFetchFailed;
  
    public delegate void UnityAdsShow();
    public static UnityAdsShow OnShow;
    
    public delegate void UnityAdsHide();
    public static UnityAdsHide OnHide;
  
    public delegate void UnityAdsVideoCompleted(string rewardItemKey, bool skipped);
    public static UnityAdsVideoCompleted OnVideoCompleted;
    
    public delegate void UnityAdsVideoStarted();
    public static UnityAdsVideoStarted OnVideoStarted;
    
    public static UnityAds SharedInstance {
      get {
        if(!sharedInstance) {
          sharedInstance = (UnityAds)FindObjectOfType(typeof(UnityAds));
        }

        if(!sharedInstance) {
          GameObject singleton = new GameObject() { hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector };
          sharedInstance = singleton.AddComponent<UnityAds>();
          singleton.name = "UnityAdsPluginBridgeObject";
          DontDestroyOnLoad(singleton);
        }

        return sharedInstance;
      }
    }

    public void Init(string gameId, bool testModeEnabled) {
      #if (UNITY_IPHONE || UNITY_ANDROID || UNITY_EDITOR)
      UnityAdsExternal.init(gameId, testModeEnabled, SharedInstance.gameObject.name);
      #endif
    }
    
    public void Awake () {
      if(gameObject == SharedInstance.gameObject) {
        DontDestroyOnLoad(gameObject);
      }
      else {
        Destroy (gameObject);
      }
    }
  
    /* Static Methods */
    
    public static bool isSupported () {
      return UnityAdsExternal.isSupported();
    }
    
    public static string getSDKVersion () {
      return UnityAdsExternal.getSDKVersion();
    }
    
    public static bool canShowAds (string network) {
      return _campaignsAvailable.Contains (network) && UnityAdsExternal.canShowAds (network);
    }

    public static void setLogLevel(Advertisement.DebugLevel logLevel) {
      UnityAdsExternal.setLogLevel(logLevel);
    }

    public static bool canShow () {
      return _campaignsAvailable.Count > 0;
    }
    
    public static bool hasMultipleRewardItems () {
      if (_campaignsAvailable.Count > 0)
        return UnityAdsExternal.hasMultipleRewardItems();
      
      return false;
    }
    
    public static List<string> getRewardItemKeys () {
      List<string> retList = new List<string>();
      
      if (_campaignsAvailable.Count > 0) {
        string keys = UnityAdsExternal.getRewardItemKeys();
        retList = new List<string>(keys.Split(';'));
      }
      
      return retList;
    }
    
    public static string getDefaultRewardItemKey () {
      if (_campaignsAvailable.Count > 0) {
        return UnityAdsExternal.getDefaultRewardItemKey();
      }
      
      return "";
    }
    
    public static string getCurrentRewardItemKey () {
      if (_campaignsAvailable.Count > 0) {
        return UnityAdsExternal.getCurrentRewardItemKey();
      }
      
      return "";
    }
    
    public static bool setRewardItemKey (string rewardItemKey) {
      if (_campaignsAvailable.Count > 0) {
        return UnityAdsExternal.setRewardItemKey(rewardItemKey);
      }
      
      return false;
    }
    
    public static void setDefaultRewardItemAsRewardItem () {
      if (_campaignsAvailable.Count > 0) {
        UnityAdsExternal.setDefaultRewardItemAsRewardItem();
      }
    }
    
    public static string getRewardItemNameKey () {
      if (_rewardItemNameKey == null || _rewardItemNameKey.Length == 0) {
        fillRewardItemKeyData();
      }
      
      return _rewardItemNameKey;
    }
    
    public static string getRewardItemPictureKey () {
      if (_rewardItemPictureKey == null || _rewardItemPictureKey.Length == 0) {
        fillRewardItemKeyData();
      }
      
      return _rewardItemPictureKey;
    }
    
    public static Dictionary<string, string> getRewardItemDetailsWithKey (string rewardItemKey) {
      Dictionary<string, string> retDict = new Dictionary<string, string>();
      string rewardItemDataString = "";
      
      if (_campaignsAvailable.Count > 0) {
        rewardItemDataString = UnityAdsExternal.getRewardItemDetailsWithKey(rewardItemKey);
        
        if (rewardItemDataString != null) {
          List<string> splittedData = new List<string>(rewardItemDataString.Split(';'));
          Utils.LogDebug("UnityAndroid: getRewardItemDetailsWithKey() rewardItemDataString=" + rewardItemDataString);
          
          if (splittedData.Count == 2) {
            retDict.Add(getRewardItemNameKey(), splittedData.ToArray().GetValue(0).ToString());
            retDict.Add(getRewardItemPictureKey(), splittedData.ToArray().GetValue(1).ToString());
          }
        }
      }
      
      return retDict;
    }

    public static void setNetworks(HashSet<string> networks) {
      UnityAdsExternal.setNetworks(networks);
    }

    public static void setNetwork(string network) {
      UnityAdsExternal.setNetwork(network);
    }
    
    public static bool show (string zoneId = null) {
      return show (zoneId, "", null);  
    }
    
    public static bool show (string zoneId, string rewardItemKey) {
      return show (zoneId, rewardItemKey, null);  
    }
    
    public static bool show (string zoneId, string rewardItemKey, Dictionary<string, string> options) {
      if (!_adsShow && _campaignsAvailable.Count > 0) {      
        if (SharedInstance) {              
          string optionsString = parseOptionsDictionary(options);
          
          if (UnityAdsExternal.show(zoneId, rewardItemKey, optionsString)) {        
            if (OnShow != null)
              OnShow();
            
            _adsShow = true;
            return true;
          }
        }
      }
      
      return false;
    }
   
    public static void hide () {
      if (_adsShow) {
        UnityAdsExternal.hide();
      }
    }
  
    private static void fillRewardItemKeyData () {
      string keyData = UnityAdsExternal.getRewardItemDetailsKeys();
      
      if (keyData != null && keyData.Length > 2) {
        List<string> splittedKeyData = new List<string>(keyData.Split(';'));
        _rewardItemNameKey = splittedKeyData.ToArray().GetValue(0).ToString();
        _rewardItemPictureKey = splittedKeyData.ToArray().GetValue(1).ToString();
      }
    }
    
    private static string parseOptionsDictionary(Dictionary<string, string> options) {
      string optionsString = "";
      if(options != null) {
        bool added = false;
        if(options.ContainsKey("noOfferScreen")) {
          optionsString += (added ? "," : "") + "noOfferScreen:" + options["noOfferScreen"];
          added = true;
        }
        if(options.ContainsKey("openAnimated")) {
          optionsString += (added ? "," : "") + "openAnimated:" + options["openAnimated"];
          added = true;
        }
        if(options.ContainsKey("sid")) {
          optionsString += (added ? "," : "") + "sid:" + options["sid"];
          added = true;
        }
        if(options.ContainsKey("muteVideoSounds")) {
          optionsString += (added ? "," : "") + "muteVideoSounds:" + options["muteVideoSounds"];
          added = true;
        }
        if(options.ContainsKey("useDeviceOrientationForVideo")) {
          optionsString += (added ? "," : "") + "useDeviceOrientationForVideo:" + options["useDeviceOrientationForVideo"];
          added = true;
        }
      }
      return optionsString;
    }
  
    /* Events */
    
    public void onHide () {
      _adsShow = false;
      if (OnHide != null)
        OnHide();
      
      Utils.LogDebug("onHide");
    }
    
    public void onShow () {
      Utils.LogDebug("onShow");
    }
    
    public void onVideoStarted () {
      if (OnVideoStarted != null)
        OnVideoStarted();

      Utils.LogDebug("onVideoStarted");
    }
    
    public void onVideoCompleted (string parameters) {
      if (parameters != null) {
        List<string> splittedParameters = new List<string>(parameters.Split(';'));
        string rewardItemKey = splittedParameters.ToArray().GetValue(0).ToString();
        bool skipped = splittedParameters.ToArray().GetValue(1).ToString() == "true" ? true : false;
        
        if (OnVideoCompleted != null)
          OnVideoCompleted(rewardItemKey, skipped);
      
        Utils.LogDebug("onVideoCompleted: " + rewardItemKey + " - " + skipped);
      }
    }
    
    public void onFetchCompleted (string network) {
      _campaignsAvailable.Add(network);
      if (OnCampaignsAvailable != null)
        OnCampaignsAvailable();
        
      Utils.LogDebug("onFetchCompleted - " + network);
    }
  
    public void onFetchFailed () {
      if (OnCampaignsFetchFailed != null)
        OnCampaignsFetchFailed();
      
      Utils.LogDebug("onFetchFailed");
    }
  }
}
