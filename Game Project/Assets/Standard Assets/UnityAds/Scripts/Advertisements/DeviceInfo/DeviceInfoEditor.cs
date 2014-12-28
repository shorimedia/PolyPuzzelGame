namespace UnityEngine.Advertisements {
  using UnityEngine;
  using System.Collections;

  internal class DeviceInfoEditor : DeviceInfoPlatform {
    public override string name() {
      return "editor";
    }
  }
}