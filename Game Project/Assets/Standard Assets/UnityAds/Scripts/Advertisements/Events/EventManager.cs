namespace UnityEngine.Advertisements.Event {

  using UnityEngine;
  using System.Collections;
  using System.Collections.Generic;

  internal class EventManager {
    private static EventManager impl;
    private static bool initialized = false;
    private string endpointUrl;
    private static int[] retryDelays = { 15, 30, 90, 240 };
    private static object initLock = new object();

    private static EventManager getImpl() {
      lock(initLock) {
        if(!initialized) {
          impl = new EventManager();
          Event.init();
          initialized = true;
        }
      }

      return impl;
    }

    public static void sendEvent(bool retryable, string eventObject) {
      getImpl().executeEvent(retryable, eventObject, DeviceInfo.getJson(), dummyCallback);
    }

    public static void sendEvent(bool retryable, string eventObject, System.Action<bool> callback) {
      getImpl().executeEvent(retryable, eventObject, DeviceInfo.getJson(), callback);
    }

    // FIXME: Replace this with a proper solution
    private static void dummyCallback(bool result) {
    }

	public static void sendStartEvent(string appId) {
	  string eventJson = MiniJSON.Json.Serialize(new Dictionary<string,object> () {
		  {"category", "ucmsdk"},
      {"action", "start"},
        {"application", appId}
	  });
			
	  sendEvent(false, eventJson);
	}

    public static void sendAdreqEvent(string gameId) {
      string eventJson = MiniJSON.Json.Serialize(new Dictionary<string,object>() {
        {"category", "ucasdk"},
        {"action", "adreq"},
        {"application", gameId}
      });

      sendEvent(false, eventJson);
    }

    public static void sendAdplanEvent(string gameId) {
      string eventJson = MiniJSON.Json.Serialize(new Dictionary<string,object>() {
        {"category", "ucasdk"},
        {"action", "adplan"},
        {"application", gameId}
      });
      
      sendEvent(false, eventJson);
    }

    public static void sendViewEvent(string gameId, string campaignId) {
      string eventJson = MiniJSON.Json.Serialize(new Dictionary<string,object>() {
        {"category", "ucasdk"},
        {"action", "view"},
        {"application", gameId},
        {"source", "picture"},
        {"target", campaignId}
      });
      
      sendEvent(true, eventJson);
    }

    public static void sendClickEvent(string gameId, string campaignId) {
      string eventJson = MiniJSON.Json.Serialize(new Dictionary<string,object>() {
        {"category", "ucasdk"},
        {"action", "click"},
        {"application", gameId},
        {"source", "picture"},
        {"target", campaignId}
      });
      
      sendEvent(true, eventJson);
    }

    public static void sendCloseEvent(string gameId, string campaignId, bool autoClosed) {
      string eventJson = MiniJSON.Json.Serialize(new Dictionary<string,object>() {
        {"category", "ucasdk"},
        {"action", "close"},
        {"application", gameId},
        {"source", autoClosed ? "picture_auto" : "picture_user"},
        {"target", campaignId}
      });
      
      sendEvent(false, eventJson);
    }

    public static void sendMediationInitEvent(string appId) {
      string eventJson = MiniJSON.Json.Serialize(new Dictionary<string,object>() {
        {"category", "ucmsdk"},
        {"action", "init"},
        {"application", appId}
      });
      
      sendEvent(false, eventJson);
    }

    public static void sendMediationAdSourcesEvent(string appId) {
      string eventJson = MiniJSON.Json.Serialize(new Dictionary<string,object>() {
        {"category", "ucmsdk"},
        {"action", "adsources"},
        {"application", appId}
      });
      
      sendEvent(false, eventJson);
    }

    public static void sendMediationHasFillEvent(string appId, string zoneId, string source) {
      string eventJson = MiniJSON.Json.Serialize(new Dictionary<string,object>() {
        {"category", "ucmsdk"},
        {"action", "hasfill"},
        {"application", appId},
        {"zone", zoneId},
        {"source", source}
      });
      
      sendEvent(false, eventJson);
    }

    public static void sendMediationCappedEvent(string appId, string zoneId, string source, long time) {
      string eventJson = MiniJSON.Json.Serialize(new Dictionary<string,object>() {
        {"category", "ucmsdk"},
        {"action", "capped"},
        {"application", appId},
        {"zone", zoneId},
        {"source", source},
        {"value", time}
      });
      
      sendEvent(false, eventJson);
    }

    public static void sendMediationShowEvent(string appId, string zoneId, string adapterId) {
      string eventJson = MiniJSON.Json.Serialize(new Dictionary<string,object>() {
        {"category", "ucmsdk"},
        {"action", "show"},
        {"application", appId},
        {"zone", zoneId},
        {"source", adapterId}
      });
      
      sendEvent(false, eventJson);
    }

    private void executeEvent(bool retryable, string eventObject, string infoObject, System.Action<bool> callback) {
      Event newEvent = new Event(Settings.eventEndpoint, retryDelays, retryable, eventObject, infoObject);
      newEvent.execute(callback);
    }
  }
}
