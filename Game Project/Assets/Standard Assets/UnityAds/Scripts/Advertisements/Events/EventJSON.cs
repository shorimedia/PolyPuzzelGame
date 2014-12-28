namespace UnityEngine.Advertisements.Event {

  using UnityEngine;
  using System.Collections;
  using System.Collections.Generic;

  internal class EventJSON {
    Dictionary<string,object> data;

    public EventJSON(string json) {
      object tmp = MiniJSON.Json.Deserialize(json);

      if(tmp is Dictionary<string,object>) {
        data = (Dictionary<string,object>)tmp;
      }
    }

    public bool hasString(string key) {
      if(data == null) {
        return false;
      }

      if(data.ContainsKey(key)) {
        return true;
      }

      return false;
    }

    public string getString(string key) {
      return (string)data[key];
    }

    public bool hasInt(string key) {
      if(data == null) {
        return false;
      }

      if(data.ContainsKey(key)) {
        return true;
      }

      return false;
    }

    public int getInt(string key) {
      long tmp = (long)data[key];

      return (int)tmp;
    }

    public bool hasBool(string key) {
      if(data == null) {
        return false;
      }

      if(data.ContainsKey(key)) {
        return true;
      }

      return false;
    }

    public bool getBool(string key) {
      return (bool)data[key];
    }
  }
}
