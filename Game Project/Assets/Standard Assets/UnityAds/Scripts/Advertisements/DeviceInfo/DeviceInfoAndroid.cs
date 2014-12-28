#if UNITY_ANDROID

namespace UnityEngine.Advertisements {

  using UnityEngine;
  using System.Collections;

  internal class DeviceInfoAndroid : DeviceInfoPlatform {
    private AndroidJavaObject androidImpl = null;

    public DeviceInfoAndroid() {
      androidImpl = new AndroidJavaObject("com.unity3d.ads.picture.DeviceInfo");
    }

    private T androidCall<T>(string method) {
      return androidImpl.Call<T> (method);
    }

    public override string name() {
      return "android";
    }

    public override string getAdvertisingIdentifier() {
      string adId = androidCall<string>("getAdvertisingTrackingId");

      return adId != null ? adId : "";
    }
	
	public override bool getNoTrack() {
		return androidCall<bool>("getLimitAdTracking");
	}

    public override string getVendor() {
      return androidCall<string>("getManufacturer");
    }
  
    public override string getModel() {
      return androidCall<string>("getModel");
    }
  
    public override string getOSVersion() {
      return androidCall<string>("getOSVersion");
    }
  
    public override string getScreenSize() {
      double inches = androidCall<double>("getScreenSize");

      return inches > 0 ? string.Format("{0:0.00}", inches) : "";
    }
  
    public override string getScreenDpi() {
      int dpi = androidCall<int>("getScreenDpi");

      return dpi > 0 ? dpi.ToString() : "";
    }
  
    public override string getDeviceId() {
      string id = androidCall<string>("getAndroidId");

      return id != null ? id : "";
    }
  
    public override string getBundleId() {
      string pkgName = androidCall<string>("getPackageName");

      return pkgName != null ? pkgName : "";
    }
  } 
}

#endif