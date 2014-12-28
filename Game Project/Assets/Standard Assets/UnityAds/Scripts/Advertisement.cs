namespace UnityEngine.Advertisements {
  using System;

  public static class Advertisement {

    public enum DebugLevel {
      NONE = 0,
      ERROR = 1,
      WARNING = 2,
      INFO = 4,
      DEBUG = 8
    }
	
    static private DebugLevel _debugLevel = Debug.isDebugBuild ? DebugLevel.ERROR | DebugLevel.WARNING | DebugLevel.INFO | DebugLevel.DEBUG : DebugLevel.ERROR | DebugLevel.WARNING | DebugLevel.INFO;
	
    static public DebugLevel debugLevel {
      get {
        return _debugLevel;
      }
	
      set {
        _debugLevel = value;
        UnityEngine.Advertisements.UnityAds.setLogLevel(_debugLevel);
      }
    }

    static public bool isSupported {
      get {
        return 
          Application.isEditor ||
          Application.platform == RuntimePlatform.IPhonePlayer || 
          Application.platform == RuntimePlatform.Android;
      }
    }

    static public bool isInitialized {
      get {
        return Engine.Instance.isInitialized;
      }
    }

    static public void Initialize(string appId, bool testMode = false) {
      Engine.Instance.Initialize(appId, testMode);
    }

    static public void Show(string zoneId = null, ShowOptions options = null) {
      Engine.Instance.Show(zoneId, options);
    }

    static public bool allowPrecache { 
      get {
        return Engine.Instance.allowPrecache;
      }
      set {
        Engine.Instance.allowPrecache = value;
      }
    }

    static public bool isReady(string zoneId = null) {
      return Engine.Instance.isReady(zoneId);
    }

    static public bool isShowing { 
      get {
        return Engine.Instance.isShowing();
      }
    }

    static public bool UnityDeveloperInternalTestMode {
		get; set;
    }

  }

}
