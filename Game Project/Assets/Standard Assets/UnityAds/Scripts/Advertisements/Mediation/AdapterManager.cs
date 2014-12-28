namespace UnityEngine.Advertisements {
  using System;
  using System.Collections.Generic;
  using System.Reflection;

  internal class AdapterManager {

    private string _zoneId = null;
    private List<KeyValuePair<string, Adapter>> _adapters = new List<KeyValuePair<string, Adapter>>();
    private Dictionary<string, IntervalManager> _adapterIntervals = new Dictionary<string, IntervalManager>();
    private Dictionary<string, KeyValuePair<long, long>> _adapterRefreshFreqs = new Dictionary<string, KeyValuePair<long, long>>();
    private Dictionary<string, List<long>> _adapterConsumeTimes = new Dictionary<string, List<long>>();

    public AdapterManager(string zoneId, List<object> data) {
      new VideoAdAdapter("VideoAdAdapter");
      new PictureAdAdapter("PictureAdAdapter");

      _zoneId = zoneId;
      foreach(object temp in data) {
        if(temp != null) {
          Dictionary<string, object> adapterConfig = (Dictionary<string, object>)temp;
          string adapterId = (string)adapterConfig["id"];
          string nameSpace = (string)adapterConfig["namespace"];
          string className = (string)adapterConfig["className"];
          Dictionary<string, object> parameters = Utils.Optional<Dictionary<string, object>>(adapterConfig, "parameters");
          long refreshAdPlanFreq = (long)adapterConfig["refreshAdPlanFreq"];
          
          Type adapterType = Type.GetType(nameSpace + "." + className);
          if(adapterType != null) {
            Adapter adapter = (Adapter)Activator.CreateInstance(adapterType, new object[] {adapterId});

            adapter.Subscribe(Adapter.EventType.adAvailable, (object sender, EventArgs e) => {
              Event.EventManager.sendMediationHasFillEvent(Engine.Instance.AppId, zoneId, adapterId);
            });

            adapter.Initialize(zoneId, adapterId, parameters);
            _adapters.Add(new KeyValuePair<string, Adapter>(adapterId, adapter));
            _adapterIntervals.Add(adapterId, null);
            _adapterRefreshFreqs.Add(adapterId, new KeyValuePair<long, long>((long)Math.Round(Time.realtimeSinceStartup), refreshAdPlanFreq));
            _adapterConsumeTimes.Add(adapterId, new List<long>());
          }
        }
      }
    }

    public Adapter SelectAdapter() {
      HashSet<string> nonCappedAdapters = NonCappedAdapters();
      foreach(KeyValuePair<string, Adapter> entry in _adapters) {
        string adapterId = entry.Key;
        Adapter adapter = entry.Value;

        if(!_adapterIntervals[adapterId].IsAvailable()) {
          Event.EventManager.sendMediationCappedEvent(Engine.Instance.AppId, _zoneId, adapterId, _adapterIntervals[adapterId].NextAvailable());
        }

        if(!adapter.isReady(_zoneId, adapterId)) {
          long lastRefreshTime = _adapterRefreshFreqs[adapterId].Key;
          long refreshFreq = _adapterRefreshFreqs[adapterId].Value;
          if((long)Math.Round(Time.realtimeSinceStartup) >= lastRefreshTime + refreshFreq) {
            adapter.RefreshAdPlan();
            _adapterRefreshFreqs[adapterId] = new KeyValuePair<long, long>((long)Math.Round(Time.realtimeSinceStartup), refreshFreq);
          }
        }

        if(nonCappedAdapters.Contains(adapterId) && adapter.isReady(_zoneId, adapterId)) {
          _adapterIntervals[adapterId].Consume();
          _adapterConsumeTimes[adapterId].Add(ConfigManager.Instance.serverTimestamp + (long)Math.Round(Time.realtimeSinceStartup));

          if(AllAdaptersConsumed()) {
            ConfigManager.Instance.RequestAdSources();
          }

          Utils.LogDebug ("Selecting adapter '" + adapterId + "' from zone '" + _zoneId + "'");
          return adapter;
        }
      }
      return null;
    }

    public void UpdateIntervals(List<object> adSources) {
      foreach(object entry in adSources) {
        Dictionary<string, object> data = (Dictionary<string, object>)entry;
        string adapterId = (string)data["id"];
        if(_adapterIntervals.ContainsKey(adapterId)) {
          _adapterConsumeTimes[adapterId].Clear();
          _adapterIntervals[adapterId] = new IntervalManager((List<object>)data["adIntervals"]);
          Utils.LogDebug("Got intervals " + _adapterIntervals[adapterId] + " for " + adapterId);
        }
      }
    }

    public Dictionary<string, List<long>> GetConsumeTimes(long lastServerTimestamp) {
      Dictionary<string, List<long>> adjustedConsumeTimes = new Dictionary<string, List<long>>();
      foreach(KeyValuePair<string, List<long>> entry in _adapterConsumeTimes) {
        List<long> adjustedTimes = new List<long>();
        foreach(long consumeTime in entry.Value) {
          adjustedTimes.Add(consumeTime - ConfigManager.Instance.localTimestamp - lastServerTimestamp);
        }
        adjustedConsumeTimes.Add(entry.Key, adjustedTimes);
      }
      return adjustedConsumeTimes;
    }

    private HashSet<string> NonCappedAdapters() {
      HashSet<string> nonCappedAdapters = new HashSet<string>();
      foreach(KeyValuePair<string, IntervalManager> entry in _adapterIntervals) {
        IntervalManager adapterIntervals = entry.Value;
        if(adapterIntervals.IsAvailable()) {
          nonCappedAdapters.Add(entry.Key);
        }
      }
      return nonCappedAdapters;
    }

    private bool AllAdaptersConsumed() {
      foreach(KeyValuePair<string, IntervalManager> entry in _adapterIntervals) {
        IntervalManager adapterIntervals = entry.Value;
        if(!adapterIntervals.IsEmpty()) {
          return false;
        }
      }
      return true;
    }

    public bool IsReady() {
      HashSet<string> nonCappedAdapters = NonCappedAdapters();
      foreach(KeyValuePair<string, Adapter> entry in _adapters) {
        if(nonCappedAdapters.Contains(entry.Key) && entry.Value.isReady(_zoneId, entry.Key)) {
          return true;
        }
      }
      return false;
    }

  }

}
