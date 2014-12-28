#if UNITY_IPHONE

namespace UnityEngine.Advertisements {
  using System.Runtime.InteropServices;
  using UnityEngine;
  using System.Collections;

  internal class DeviceInfoIos : DeviceInfoPlatform {
    public override string name() {
      return "ios";
    }
  
    public override string getAdvertisingIdentifier() {
      return iPhone.advertisingIdentifier;
    }
    
    [DllImport ("__Internal")]
    public static extern bool _GetNoTrackFlag();
//      AdvertsingTrackingEnabled == false => doNotTrack = true
//      AdvertisingTrackingEnabled == true => doNotTrack = false
    public override bool getNoTrack() {
      return _GetNoTrackFlag();
    }
  
    public override string getVendor() {
      return "Apple";
    }
  
    public override string getModel() {
      return SystemInfo.deviceModel;
    }
  
    public override string getOSVersion() {
      return SystemInfo.operatingSystem;
    }
  
    public override string getScreenSize() {
      double inches = Mathf.Sqrt(Mathf.Pow(Screen.currentResolution.width / Screen.dpi, 2) + Mathf.Pow(Screen.currentResolution.height / Screen.dpi, 2));
      return string.Format("{0:0.00}", inches);
    }
  
    public override string getScreenDpi() {
      return Mathf.Round(Screen.dpi).ToString();
    }

    [DllImport ("__Internal")]
    public static extern string _GetCFBundleID();

    public override string getBundleId() {
      return _GetCFBundleID();
    }
  }
}

#endif
