namespace UnityEngine.Advertisements {

  internal static class Settings {

    public static string mediationEndpoint = "http://admediator.unityads.unity3d.com";
    public static string pictureAdsEndpoint = "http://adserver.unityads.unity3d.com";
    public static string eventEndpoint = "http://events.unityads.unity3d.com/v1/event";
    public static string sdkVersion = "1.0.3";

    public static void enableUnityDeveloperInternalTestMode() {
      mediationEndpoint = "http://admediator.staging.unityads.unity3d.com";
      pictureAdsEndpoint = "http://adserver.staging.unityads.unity3d.com";
      eventEndpoint = "http://events.staging.unityads.unity3d.com/v1/event";
    }

  }

}