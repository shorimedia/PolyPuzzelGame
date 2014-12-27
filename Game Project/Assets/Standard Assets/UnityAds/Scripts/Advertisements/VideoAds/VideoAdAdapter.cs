namespace UnityEngine.Advertisements {
  using System;
  using System.Collections.Generic;
  using ShowOptionsExtended = Optional.ShowOptionsExtended;

  internal class VideoAdAdapter : Adapter {

    private bool _isShowing = false;
    private static Dictionary<string, Dictionary<string, object>> _configurations = new Dictionary<string, Dictionary<string, object>>();

    public VideoAdAdapter(string adapterId) : base(adapterId) {}

    public override void Initialize(string zoneId, string adapterId, Dictionary<string, object> configuration) {
      UnityAds.OnCampaignsAvailable += UnityAdsCampaignsAvailable;
      UnityAds.OnCampaignsFetchFailed += UnityAdsCampaignsFetchFailed;
        
      triggerEvent(EventType.initStart, EventArgs.Empty);
        
      UnityAds.SharedInstance.Init(Engine.Instance.AppId, Engine.Instance.testMode);

      _configurations.Add(zoneId + adapterId, configuration);
    }

    public override void RefreshAdPlan() {}
    public override void StartPrecaching() {}
    public override void StopPrecaching() {}
    
    public override bool isReady(string zoneId, string adapterId) {
      Dictionary<string, object> configuration = _configurations[zoneId + adapterId];
      if(configuration != null && configuration.ContainsKey("network")) {
        return UnityAds.canShowAds((string)configuration["network"]);
      }
      return false;
    }
    
    public override void Show(string zoneId, string adapterId, ShowOptions options = null) {
      if (options != null && options.pause == false) {
        Utils.LogWarning ("Video ads will always pause engine, ignoring pause=false in ShowOptions");
      }

      Dictionary<string, object> configuration = _configurations[zoneId + adapterId];
      if(configuration != null && configuration.ContainsKey("network")) {
        UnityAds.setNetwork((string)configuration["network"]);
      }
      string videoZoneId = null;
      string rewardItem = "";
      if(configuration != null && configuration.ContainsKey("zone")) {
        videoZoneId = (string)configuration["zone"];
      }
      if(configuration != null && configuration.ContainsKey("rewardItem")) {
        rewardItem = (string)configuration["rewardItem"];
      }

      UnityAds.OnShow += UnityAdsShow;
      UnityAds.OnHide += UnityAdsHide;
      UnityAds.OnVideoCompleted += UnityAdsVideoCompleted;
      UnityAds.OnVideoStarted += UnityAdsVideoStarted;

      ShowOptionsExtended extendedOptions = options as ShowOptionsExtended;
      if(extendedOptions != null && extendedOptions.gamerSid != null && extendedOptions.gamerSid.Length > 0) {
        if(!UnityAds.show(videoZoneId, rewardItem, new Dictionary<string, string>() {{"sid", extendedOptions.gamerSid}})) {
          triggerEvent(EventType.error, EventArgs.Empty);
        }
      } else {
        if(!UnityAds.show(videoZoneId, rewardItem)) {
          triggerEvent(EventType.error, EventArgs.Empty);
        }
      }
    }
    
    public override bool isShowing() {
      return _isShowing;
    }
    
    private void UnityAdsCampaignsAvailable() {
      Utils.LogDebug("UNITY ADS: CAMPAIGNS READY!");
      triggerEvent(EventType.initComplete, EventArgs.Empty);
      triggerEvent(EventType.adAvailable, EventArgs.Empty);
    }
    
    private void UnityAdsCampaignsFetchFailed() {
      Utils.LogDebug("UNITY ADS: CAMPAIGNS FETCH FAILED!");
      triggerEvent(EventType.initFailed, EventArgs.Empty);
    }
    
    private void UnityAdsShow() {
      Utils.LogDebug("UNITY ADS: SHOW!"); 
      _isShowing = true;
      triggerEvent(EventType.adWillOpen, EventArgs.Empty);
      triggerEvent(EventType.adDidOpen, EventArgs.Empty);
    }
    
    private void UnityAdsHide() {
      Utils.LogDebug("UNITY ADS: HIDE!");
      _isShowing = false;

      UnityAds.OnShow -= UnityAdsShow;
      UnityAds.OnHide -= UnityAdsHide;
      UnityAds.OnVideoCompleted -= UnityAdsVideoCompleted;
      UnityAds.OnVideoStarted -= UnityAdsVideoStarted;

      triggerEvent(EventType.adWillClose, EventArgs.Empty);
      triggerEvent(EventType.adDidClose, EventArgs.Empty);
    }
    
    private void UnityAdsVideoCompleted(string rewardItemKey, bool skipped) {
      Utils.LogDebug("UNITY ADS: VIDEO COMPLETE : " + rewardItemKey + " - " + skipped);
      if(skipped) {
        triggerEvent(EventType.adSkipped, EventArgs.Empty);
      } else {
        triggerEvent(EventType.adFinished, EventArgs.Empty);
      }
    }
    
    private void UnityAdsVideoStarted() {
      Utils.LogDebug("UNITY ADS: VIDEO STARTED!");
      triggerEvent(EventType.adStarted, EventArgs.Empty);
    }
    
  }
  
}
