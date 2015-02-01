#if UNITY_ANDROID && !UNITY_EDITOR
using UnityEngine;
using System.Collections;
using System.Text;
using System.Security.Cryptography;

namespace UnityEngine.Cloud.Analytics
{
	internal class AndroidWrapper : BasePlatformWrapper
	{
		public override string appVersion
		{
			get {
				string appVer = null;
				using(var appUtilClass = new AndroidJavaClass("com.unityengine.cloud.AppUtil"))
				{
					appVer = appUtilClass.CallStatic<string>("getAppVersion");
				}
				return appVer;
			}
		}

		public override string appBundleIdentifier
		{
			get {
				string appBundleId = null;
				using(var appUtilClass = new AndroidJavaClass("com.unityengine.cloud.AppUtil"))
				{
					appBundleId = appUtilClass.CallStatic<string>("getAppPackageName");
				}
				return appBundleId;
			}
		}

		public override string appInstallMode
		{
			get {
				string appInstallMode = null;
				using(var appUtilClass = new AndroidJavaClass("com.unityengine.cloud.AppUtil"))
				{
					appInstallMode = appUtilClass.CallStatic<string>("getAppInstallMode");
				}
				return appInstallMode;
			}
		}
		
		public override bool isRootedOrJailbroken
		{
			get {
				bool isBroken = false;
				using(var appUtilClass = new AndroidJavaClass("com.unityengine.cloud.AppUtil"))
				{
					isBroken = appUtilClass.CallStatic<bool>("isDeviceRooted");
				}
				return isBroken;
			}
		}

		private string Md5Hex(string input){
			System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
			byte[] bytes = ue.GetBytes(input);

			// encrypt bytes
			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] hashBytes = md5.ComputeHash(bytes);

			// Convert the encrypted bytes back to a string (base 16)
			string hashString = "";

			for (int i = 0; i < hashBytes.Length; i++)
			{
				hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
			}

			return hashString.PadLeft(32, '0');
		}

		public override string deviceUniqueIdentifier
		{
			get 
			{ 
				try 
				{
					AndroidJavaClass clsUnity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
					AndroidJavaObject objActivity = clsUnity.GetStatic<AndroidJavaObject>("currentActivity");
					AndroidJavaObject objResolver = objActivity.Call<AndroidJavaObject>("getContentResolver");
					AndroidJavaClass clsSecure = new AndroidJavaClass("android.provider.Settings$Secure");
					string ANDROID_ID = clsSecure.GetStatic<string>("ANDROID_ID");
					string androidId = clsSecure.CallStatic<string>("getString", objResolver, ANDROID_ID);

					return Md5Hex(androidId);
				} 
				catch (UnityEngine.AndroidJavaException)
				{
					return "";
				}
				catch (System.SystemException)
				{
					return "";
				}
			}
		}
	}
}
#endif

