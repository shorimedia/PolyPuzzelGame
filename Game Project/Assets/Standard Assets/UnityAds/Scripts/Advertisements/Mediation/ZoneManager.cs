namespace UnityEngine.Advertisements {
  using System;
  using System.Collections.Generic;

  internal class ZoneManager {

    private Zone defaultZone = null;
    private Dictionary<string, Zone> _zones = new Dictionary<string, Zone>();
    static private readonly ZoneManager _sharedInstance = new ZoneManager();
    
    static public ZoneManager Instance {
      get {
        return _sharedInstance;
      }
    }
    
    private ZoneManager() {
    }

    public void ResetZones(List<object> zones) {
      _zones.Clear();

      foreach(object temp in zones) {
        Zone zone = new Zone((Dictionary<string, object>)temp);
        
        if(zone.isDefault) {
          defaultZone = zone;
        }
        
        _zones.Add(zone.Id, zone);
      }
    }

    public Zone GetDefaultZone() {
      return defaultZone;
    }

    public Zone GetZone(string zoneId) {
      if(zoneId == null) {
        return defaultZone;
      } else if(_zones.ContainsKey(zoneId)) {
        return _zones[zoneId];
      } else {
        return null;
      }
    }

    public bool IsReady(string zoneId) {
      if(zoneId == null && defaultZone != null) {
        return defaultZone.IsReady();
			} else if(zoneId != null && _zones.ContainsKey(zoneId)) {
        return _zones[zoneId].IsReady();
      } else {
        return false;
      }
    }

    public List<Zone> GetZones() {
      return new List<Zone>(_zones.Values);
    }

    public List<string> GetZoneIds() {
      return new List<string>(_zones.Keys);
    }

    public void UpdateIntervals(Dictionary<string, object> adSources) {
      foreach(KeyValuePair<string, object> entry in adSources) {
        if(_zones.ContainsKey(entry.Key)) {
          _zones[entry.Key].UpdateIntervals((Dictionary<string, object>)entry.Value);
        }
      }
    }

    public Dictionary<string, Dictionary<string, List<long>>> GetConsumeTimes(long lastServerTimestamp) {
      Dictionary<string, Dictionary<string, List<long>>> consumeTimes = new Dictionary<string, Dictionary<string, List<long>>>();
      foreach(KeyValuePair<string, Zone> entry in _zones) {
        consumeTimes.Add(entry.Key, entry.Value.GetConsumeTimes(lastServerTimestamp));
      }
      return consumeTimes;
    }

  }

}
