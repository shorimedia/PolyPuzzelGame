namespace UnityEngine.Advertisements {
  using System;
  using System.Collections.Generic;
  using UnityEngine.Advertisements.MiniJSON;
  using HTTPLayer;

  internal class ConfigManager {
    
    public string configId { get; private set; }
    
    public long adSourceTtl { get; private set; }

    public long serverTimestamp { get; private set; }

    public long localTimestamp { get; private set; }

    public IntervalManager globalIntervals { get; private set; }

    public bool isInitialized { get; private set; }

    static private readonly ConfigManager _sharedInstance = new ConfigManager();

		int[] retryDelays = { 15, 30, 90, 240 };
    
    static public ConfigManager Instance {
      get {
        return _sharedInstance;
      }
    }
    
    private ConfigManager() {
    }

    public bool IsReady() {
      if(globalIntervals != null) {
        if((long)Math.Round(Time.realtimeSinceStartup) >= localTimestamp + adSourceTtl && !_requestingAdSources) {
          Utils.LogDebug("Ad Source TTL expired");
          RequestAdSources();
        }
        return globalIntervals.IsAvailable();
      }
      return false;
    }

    private bool _requestingConfig = false;

    public void RequestConfig() {
      if(_requestingConfig) {
        return;
      }
      _requestingConfig = true;
      string configRequestUrl = Settings.mediationEndpoint + "/v1/games/" + Engine.Instance.AppId + "/config";
      //string configRequestUrl = Settings.serverEndpoint + "/testConfig.json";
      HTTPRequest request = new HTTPRequest("POST", configRequestUrl);
      request.addHeader("Content-Type", "application/json");
      if(configId != null) {
        request.addHeader("If-None-Match", configId);
      }
      request.setPayload(DeviceInfo.getJson());

      Utils.LogDebug("Requesting new config from " + configRequestUrl);
			HTTPManager.sendRequest(request, HandleConfigResponse, retryDelays, 20 * 60);
    }

    private void HandleConfigResponse(HTTPResponse response) {
      if(response.error) {
        if(!string.IsNullOrEmpty(response.errorMsg)) {
          Utils.LogDebug("ConfigManager config request error: " + response.errorMsg);
        } else {
          Utils.LogDebug("ConfigManager config request error: unknown error");
        }
        return;
      }

      Dictionary<string, object> data;
      try {
        data = ParseResponse(response);
      }
      catch(Exception) {
        return;
      }

      if(data == null) {
        return;
      }

      Utils.LogDebug("Received config response");

      if(response.headers != null && response.headers.ContainsKey("ETAG")) {
        string etag = response.headers["ETAG"];
        configId = etag.Substring(3, etag.Length - 4);
      }

      List<object> zones = (List<object>)data["zones"];
      GatherNetworks(zones);
      ZoneManager.Instance.ResetZones(zones);
      adSourceTtl = (long)data["adSourceTtl"];
      serverTimestamp = (long)data["serverTimestamp"];

      RequestAdSources();

      _requestingConfig = false;
    }

    private void GatherNetworks(List<object> zones) {
      HashSet<string> networks = new HashSet<string>();
      foreach(object zone in zones) {
        List<object> adapters = (List<object>)((Dictionary<string, object>)zone)["adapters"];
        foreach(object adapter in adapters) {
          Dictionary<string, object> adapterJson = (Dictionary<string, object>)adapter;
          string className = (string)adapterJson["className"];
          if(className.Equals("VideoAdAdapter")) {
            Dictionary<string, object> parameters = (Dictionary<string, object>)adapterJson["parameters"];
            string network = (string)parameters["network"];
            networks.Add(network);
          }
        }
      }
      UnityAds.setNetworks(networks);
    }

    private bool _requestingAdSources = false;

    public void RequestAdSources(List<string> zoneIds = null) {
      if(_requestingAdSources) {
        return;
      }
      _requestingAdSources = true;
      string adSourcesRequestUrl = Settings.mediationEndpoint + "/v1/games/" + Engine.Instance.AppId + "/adSources";
      //string adSourcesRequestUrl = Settings.serverEndpoint + "/testAdSources.json";

      adSourcesRequestUrl = Utils.addUrlParameters(adSourcesRequestUrl, new Dictionary<string, object>() {
        {"zones", zoneIds != null ? String.Join(",", zoneIds.ToArray()) : null},
        {"config", configId}
      });

      HTTPRequest request = new HTTPRequest("POST", adSourcesRequestUrl);
      request.addHeader("Content-Type", "application/json");
      string payload = MiniJSON.Json.Serialize(new Dictionary<string, object>() {
        {"lastServerTimestamp", ConfigManager.Instance.serverTimestamp},
        {"adTimes", ZoneManager.Instance.GetConsumeTimes(ConfigManager.Instance.serverTimestamp)}
      });
      request.setPayload(payload);

      Utils.LogDebug("Requesting new ad sources from " + adSourcesRequestUrl + " with payload: " + payload);
      Event.EventManager.sendMediationAdSourcesEvent(Engine.Instance.AppId);
			HTTPManager.sendRequest(request, HandleAdSourcesResponse, retryDelays, 20 * 60);
    }

    private void HandleAdSourcesResponse(HTTPResponse response) {
      Dictionary<string, object> data;
      try {
        data = ParseResponse(response);
      }
      catch(Exception) {
        return;
      }

      if(data == null) {
        return;
      }

      Utils.LogDebug("Received ad sources response");

      globalIntervals = new IntervalManager((List<object>)data["adIntervals"]);

      Utils.LogDebug("Got " + globalIntervals + " intervals for global");

      serverTimestamp = (long)data["serverTimestamp"];
      localTimestamp = (long)Math.Round(Time.realtimeSinceStartup);

      ZoneManager.Instance.UpdateIntervals((Dictionary<string, object>)data["adSources"]);

      _requestingAdSources = false;
      isInitialized = true;
    }

    private Dictionary<string, object> ParseResponse(HTTPResponse response) {
			// TODO fix exception ArgumentNullException: Argument cannot be null. when connection is down 
			if (response == null) return null;
      string jsonString = System.Text.Encoding.UTF8.GetString(response.data, 0, response.dataLength);
      object jsonObject = MiniJSON.Json.Deserialize(jsonString);
      if(jsonObject != null && jsonObject is Dictionary<string, object>) {
        Dictionary<string, object> json = (Dictionary<string, object>)jsonObject;
        long statusCode = (long)json["status"];
        if(statusCode != 200) {
          string errorMessage = (string)json["error"];
          throw new Exception(errorMessage);
        }
        Dictionary<string, object> data = (Dictionary<string, object>)json["data"];
        return data;
      }
      return null;
    }

  }

}

