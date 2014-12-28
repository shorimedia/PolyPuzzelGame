namespace UnityEngine.Advertisements {
  using System;
  using System.Collections.Generic;

  public abstract class Adapter {

    public enum EventType {
      initStart,
      initFailed,
      initComplete,
      
      adAvailable,
      adWillOpen,
      adDidOpen,
      adWillClose,
      adDidClose,
      adStarted,
      adSkipped,
      adFinished,
      adClicked,
      
      error
    }

    private string _adapterId = null;
    private Dictionary<EventType, Delegate> _events = new Dictionary<EventType, Delegate>();

    public virtual void Subscribe(EventType eventType, EventHandler handler) {
      lock(_events) {
        _events[eventType] = (EventHandler)_events[eventType] + handler;
      }
    }

    public virtual void Unsubscribe(EventType eventType, EventHandler handler) {
      lock(_events) {
        _events[eventType] = (EventHandler)_events[eventType] - handler;
      }
    }

    protected Adapter(string adapterId) {
      _adapterId = adapterId;
      foreach(EventType eventType in (EventType[])Enum.GetValues(typeof(EventType))) {
        _events.Add(eventType, null);
      }
    }

    public virtual string Id {
      get {
        return _adapterId;
      }
    }

    abstract public void Initialize(string zoneId, string adapterId, Dictionary<string, object> configuration);

    abstract public void RefreshAdPlan(); 

    abstract public void StartPrecaching();

    abstract public void StopPrecaching();

    abstract public bool isReady(string zoneId, string adapterId);

    abstract public void Show(string zoneId, string adapterId, ShowOptions options = null);

    abstract public bool isShowing();

    protected virtual void triggerEvent(EventType eventType, EventArgs e) {
      EventHandler handler = (EventHandler)_events[eventType];
      if(handler != null) {
        handler(this, e);
      }
    }
  }

}

