namespace UnityEngine.Advertisements {
  using System;
  using System.Collections.Generic;
  using UnityEngine;

  internal class IntervalManager {

    private List<long> _intervals = null;

    public IntervalManager(List<object> intervals) {
      _intervals = new List<long>();
      foreach(object interval in intervals) {
        _intervals.Add((long)interval);
      }
    }

    public bool IsEmpty() {
      return _intervals.Count == 0;
    }

    public bool IsAvailable() {
      if(!IsEmpty()) {
        long nextSlot = _intervals[0];
        if(Math.Abs(Math.Round(Time.realtimeSinceStartup) - ConfigManager.Instance.localTimestamp) >= nextSlot) {
          return true;
        }
      }
      return false;
    }

    public long NextAvailable() {
      if(!IsEmpty()) {
        return _intervals[0];
      }
      return 0;
    }

    public void Consume() {
      if(!IsEmpty() && IsAvailable()) {
        _intervals.RemoveAt(0);
      }
    }

    public override string ToString() {
      return Utils.Join(_intervals, ",");
    }

  }

}
