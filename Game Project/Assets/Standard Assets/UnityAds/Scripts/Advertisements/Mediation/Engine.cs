namespace UnityEngine.Advertisements {
  using System;
  using System.Collections;
  using System.Collections.Generic;

  internal class Engine {

    private static float _savedTimeScale;
    private static float _savedAudioVolume;
    private static bool _isShowing = false;
    private static bool _testMode = false;
    static private readonly Engine _sharedInstance = new Engine();

    static public Engine Instance {
      get {
        return _sharedInstance;
      }
    }

    private Engine() {
    }

    private bool _initialized = false;

    // Engine is considered to be successfully initialized when we have successfully received configuration from server
    public bool isInitialized {
      get {
        return ConfigManager.Instance.isInitialized;
      }
    }

    public string AppId { get; private set; }

    public bool allowPrecache {
      get {
        return true;
      }
      set {
          
      }
    }

    public void Initialize(string appId, bool testMode) {
      if(_initialized) {
        return;
      }

      // Prevent double inits in all situations
      _initialized = true;

      _testMode = testMode;

      if(Advertisement.UnityDeveloperInternalTestMode) {
        Settings.enableUnityDeveloperInternalTestMode();
      }

      Event.EventManager.sendStartEvent(appId);

      AppId = appId;
      ConfigManager.Instance.RequestConfig();
    
      Event.EventManager.sendMediationInitEvent(appId);
    }

    public void Show(string zoneId, ShowOptions options) {
      if(!_initialized) {
        if(options.resultCallback != null) {
          options.resultCallback(ShowResult.Failed);
        }
        return;
      }

			if(!ConfigManager.Instance.IsReady()) {
				if (ConfigManager.Instance.globalIntervals != null)
	      	Event.EventManager.sendMediationCappedEvent(Engine.Instance.AppId, null, "global", ConfigManager.Instance.globalIntervals.NextAvailable());
          if(options.resultCallback != null) {
            options.resultCallback(ShowResult.Failed);
          }
					return;
			}

      Zone zone = ZoneManager.Instance.GetZone(zoneId);
      if(zone == null) {
        if(options.resultCallback != null) {
          options.resultCallback(ShowResult.Failed);
        }
        return;
      }

      Adapter adapter = zone.SelectAdapter();
      if(adapter == null) {
        if(options.resultCallback != null) {
          options.resultCallback(ShowResult.Failed);
        }
        return;
      }

      Utils.LogDebug("Consuming global ad slot");
      ConfigManager.Instance.globalIntervals.Consume();

      if(ConfigManager.Instance.globalIntervals.IsEmpty()) {
        Utils.LogDebug("Global ad interval list empty");
        ConfigManager.Instance.RequestAdSources();
      }

      EventHandler finishedHandler = null;
      EventHandler skippedHandler = null;
      EventHandler failedHandler = null;

      EventHandler closeHandler = null;
      closeHandler = (object sender, EventArgs e) => {
        _isShowing = false;
        adapter.Unsubscribe(Adapter.EventType.adDidClose, closeHandler);

        if(finishedHandler != null) {
          adapter.Unsubscribe(Adapter.EventType.adFinished, finishedHandler);
        }
        if(skippedHandler != null) {
          adapter.Unsubscribe(Adapter.EventType.adSkipped, skippedHandler);
        }
        if(failedHandler != null) {
          adapter.Unsubscribe(Adapter.EventType.error, failedHandler);
        }
      };
      adapter.Subscribe(Adapter.EventType.adDidClose, closeHandler);

      if(options != null) {
        if(options.pause) {
          EventHandler showHandler = null;
          showHandler = (object sender, EventArgs e) => {
            PauseGame();
            adapter.Unsubscribe(Adapter.EventType.adWillOpen, showHandler);
          };
          adapter.Subscribe(Adapter.EventType.adWillOpen, showHandler);
        }

        finishedHandler = (object sender, EventArgs e) => {
          _isShowing = false;
					if(options.resultCallback != null)
          options.resultCallback(ShowResult.Finished);
          if(options.pause) {
            ResumeGame();
          }
          adapter.Unsubscribe(Adapter.EventType.adFinished, finishedHandler);
          finishedHandler = null;
        };
        adapter.Subscribe(Adapter.EventType.adFinished, finishedHandler);      

        skippedHandler = (object sender, EventArgs e) => {
          _isShowing = false;
					if(options.resultCallback != null)
          options.resultCallback(ShowResult.Skipped);
          if(options.pause) {
            ResumeGame();
          }
          adapter.Unsubscribe(Adapter.EventType.adSkipped, skippedHandler);
          skippedHandler = null;
        };
        adapter.Subscribe(Adapter.EventType.adSkipped, skippedHandler);      

        failedHandler = (object sender, EventArgs e) => {
          _isShowing = false;
					if(options.resultCallback != null)
          options.resultCallback(ShowResult.Failed);
          if(options.pause) {
            ResumeGame();
          }
          adapter.Unsubscribe(Adapter.EventType.error, failedHandler);
          failedHandler = null;
        };
        adapter.Subscribe(Adapter.EventType.error, failedHandler);
      }

      Event.EventManager.sendMediationShowEvent(AppId, zone.Id, adapter.Id);
      adapter.Show(zone.Id, adapter.Id, options);
      _isShowing = true;
    }

    public bool isReady(string zoneId) {
      return ConfigManager.Instance.IsReady() && ZoneManager.Instance.IsReady(zoneId);
    }
  
    public bool isShowing() {
      return _isShowing;
    }

    public bool testMode {
      get {
        return _testMode;
      }
      private set {
        _testMode = value;
      }
    }

    private static void PauseGame() {
      _savedAudioVolume = AudioListener.volume;
      AudioListener.pause = true;
      AudioListener.volume = 0;
      _savedTimeScale = Time.timeScale;
      Time.timeScale = 0;
    }
    
    private static void ResumeGame() {
      Time.timeScale = _savedTimeScale;
      AudioListener.volume = _savedAudioVolume;
      AudioListener.pause = false;
    }
  }
}
