namespace UnityEngine.Advertisements {
  using System;
  using System.Collections.Generic;
  using System.Text;
  using UnityEngine;

  internal static class DeviceInfo {
    private static DeviceInfoPlatform impl = null;
    private static bool init = false;
    private static string json = null;

    private static DeviceInfoPlatform getImpl() {
      if(!init) {
#if UNITY_EDITOR
        impl = new DeviceInfoEditor();
#elif UNITY_ANDROID
        impl = new DeviceInfoAndroid();
#elif UNITY_IPHONE
        impl = new DeviceInfoIos();
#else
        // Platform not supported, default to editor
        impl = new DeviceInfoEditor();
#endif
        init = true;
      }

      return impl;
    }

    public static string currentPlatform() {
      return getImpl().name();
    }

    public static string advertisingIdentifier() {
		return getImpl().getAdvertisingIdentifier();
    }

		public static bool noTrack() {
			return getImpl().getNoTrack();
		}

    public static string deviceVendor() {
      return getImpl().getVendor();
    }
  
    public static string deviceModel() {
      return getImpl().getModel();
    }

    public static string osVersion() {
      return getImpl().getOSVersion();
    }

    public static string screenSize() {
      return getImpl().getScreenSize();
    }

    public static string screenDpi() {
      return getImpl().getScreenDpi();
    }

    public static string deviceID() {
      return getImpl().getDeviceId();
    }
  
    public static string bundleID() {
      return getImpl().getBundleId();
    }

		private static Dictionary <string, object> mainInfoDict() {
			Dictionary<string, object> dict = new Dictionary<string, object>();
			addDeviceInfo(dict, "platform", currentPlatform());
			addDeviceInfo(dict, "unity", Application.unityVersion);
			addDeviceInfo(dict, "aid", advertisingIdentifier());
			addDeviceInfo(dict, "notrack", noTrack());
			addDeviceInfo(dict, "make", deviceVendor());
			addDeviceInfo(dict, "model", deviceModel());
			addDeviceInfo(dict, "os", osVersion());
			addDeviceInfo(dict, "screen", screenSize());
			addDeviceInfo(dict, "dpi", screenDpi());
			addDeviceInfo(dict, "did", deviceID());
			addDeviceInfo(dict, "bundle", bundleID());
      addDeviceInfo(dict, "test", Engine.Instance.testMode);
      addDeviceInfo(dict, "sdk", Settings.sdkVersion);
			return dict;
		}

    public static string getJson() {
      if(json == null) {
        json = MiniJSON.Json.Serialize(mainInfoDict());
      }
      return json;
    }

		public static string adRequestJSONPayload(string network) {
			Dictionary<string, object> dict = new Dictionary<string, object>();
			addDeviceInfo(dict, "network", network);
			dict["info"] = mainInfoDict();
			return MiniJSON.Json.Serialize(dict);
		}

    private static void addDeviceInfo(Dictionary<string, object> dict, string key, object value) {
      if(value != null) {
        if(!(value is string)) {
          dict.Add(key, value);
        } else if(value is string && ((string)value).Length > 0) {
          dict.Add(key, value);
        }
      }
    }
  }
}