namespace UnityEngine.Advertisements {

  using UnityEngine;
  using System.Collections;

  internal abstract class DeviceInfoPlatform {
    public abstract string name();

    // The default methods return empty strings. This means that the attribute is not supported. Platform specific overrides should be
    // written only if the attribute is supported and meaningful value can be returned.
    public virtual string getAdvertisingIdentifier() {
      return "";
    }

		public virtual bool getNoTrack() {
			return false;
		}

    public virtual string getVendor() {
      return "";
    }

    public virtual string getModel() {
      return "";
    }

    public virtual string getOSVersion() {
      return "";
    }

    public virtual string getScreenSize() {
      return "";
    }

    public virtual string getScreenDpi() {
      return "";
    }

    public virtual string getDeviceId() {
      return "";
    }

    public virtual string getBundleId() {
      return "";
    }
  }
}