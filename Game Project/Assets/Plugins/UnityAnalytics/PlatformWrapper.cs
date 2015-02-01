#if UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_4 
#define UNITY_COMPATLEVEL_UNITY4
//#elif ((UNITY_4_5 || UNITY_4_6) && (!(UNITY_WP8 || UNITY_METRO)) )
#elif ((UNITY_4_5 || UNITY_4_6) && UNITY_STANDALONE )
#define UNITY_COMPATLEVEL_UNITY4
#endif


#if UNITY_IPHONE || UNITY_ANDROID || (UNITY_STANDALONE && UNITY_EDITOR)
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.Cloud.Analytics
{
	internal static class PlatformWrapper
	{
		public static IPlatformWrapper platform
		{
			get {
				#if UNITY_ANDROID && !UNITY_EDITOR
				return new AndroidWrapper();
				#elif UNITY_IPHONE && !UNITY_EDITOR
				return new iOSWrapper();
				#else
				return new BasePlatformWrapper();
				#endif
			}
		}
	}
	
	internal class BasePlatformWrapper : IPlatformWrapper, IWWWFactory
	{
		private System.Random m_Random;

		internal BasePlatformWrapper()
		{
			m_Random = new System.Random();
		}

		#region IPlatformWrapper
		public virtual string appVersion
		{
			get { return null; }
		}
		
		public virtual string appBundleIdentifier
		{
			get { return null; }
		}
		
		public virtual string appInstallMode
		{
			get { return null; }
		}
		
		public virtual bool isRootedOrJailbroken
		{
			get { return false; }
		}
		#endregion

		#region IApplication
		public virtual string deviceMake
		{
			get { return Application.platform.ToString(); }
		}

		public virtual bool isNetworkReachable
		{
			get { return Application.internetReachability != NetworkReachability.NotReachable; }
		}

		public virtual bool isWebPlayer
		{
			get { return Application.isWebPlayer; }
		}

		public virtual bool isEditor
		{
			get { return Application.isEditor; }
		}

		public virtual int levelCount
		{
			get { return Application.levelCount; }
		}

		public virtual int loadedLevel
		{
			get { return Application.loadedLevel; }
		}

		public virtual string loadedLevelName
		{
			get { return Application.loadedLevelName; }
		}

		public virtual string persistentDataPath
		{
			get { return Application.persistentDataPath; }
		}

		public virtual string platformName
		{
			get {
				switch (Application.platform)
				{
					// OSXEditor	In the Unity editor on Mac OS X.
					case RuntimePlatform.OSXEditor:
						return "editor-mac";

					// OSXPlayer	In the player on Mac OS X.
					case RuntimePlatform.OSXPlayer:
						return "mac";

					// WindowsPlayer	In the player on Windows.
					case RuntimePlatform.WindowsPlayer:
						return "win";

					// OSXWebPlayer	In the web player on Mac OS X.
					case RuntimePlatform.OSXWebPlayer:
						return "web-mac";

					// OSXDashboardPlayer	In the Dashboard widget on Mac OS X.
					case RuntimePlatform.OSXDashboardPlayer:
						return "dash-mac";

					// WindowsWebPlayer	In the web player on Windows.
					case RuntimePlatform.WindowsWebPlayer:
						return "web-win";

					// WindowsEditor	In the Unity editor on Windows.
					case RuntimePlatform.WindowsEditor:
						return "editor-win";

					// IPhonePlayer	In the player on the iPhone.
					case RuntimePlatform.IPhonePlayer:
						return "ios";

					// XBOX360	In the player on the XBOX360.
					case RuntimePlatform.XBOX360:
						return "xbox360";

					// PS3	In the player on the Play Station 3.
					case RuntimePlatform.PS3:
						return "ps3";

					// Android	In the player on Android devices.
					case RuntimePlatform.Android:
						return "android";

					// LinuxPlayer	In the player on Linux.
					case RuntimePlatform.LinuxPlayer:
						return "linux";
/*
					// FlashPlayer	Flash Player.
					case RuntimePlatform.FlashPlayer:
						return "flash";

					// WP8Player	In the player on Windows Phone 8 device.
					case RuntimePlatform.WP8Player:
						return "win8";

					// PSP2	In the player on the PS Vita.
					case RuntimePlatform.PSP2:
						return "psp2";

					// PS4	In the player on the Playstation 4.
					case RuntimePlatform.PS4:
						return "ps4";

					// PSMPlayer	In the player on the PSM.
					case RuntimePlatform.PSMPlayer:
						return "psm";

					// XboxOne	In the player on Xbox One.
					case RuntimePlatform.XboxOne:
						return "xboxone";

					// SamsungTVPlayer	In the player on Samsung Smart TV.
					case RuntimePlatform.SamsungTVPlayer:
						return "tv";
*/
				}
				return "unity";
			}
		}

		public virtual string unityVersion
		{
			get { return Application.unityVersion; }
		}
		#endregion

		#region ISystemInfo
		public long GetLongRandom()
		{
			var buffer = new byte[8];
			m_Random.NextBytes(buffer);
			return (long)(System.BitConverter.ToUInt64(buffer, 0) & System.Int64.MaxValue);
		}
		#endregion

		#region ISystemInfo
		public virtual string deviceModel
		{
			get { return SystemInfo.deviceModel; }
		}

		public virtual string deviceUniqueIdentifier
		{
			get { 
				#if UNITY_ANDROID && !UNITY_EDITOR
				return "";
				#else
				return SystemInfo.deviceUniqueIdentifier; 
				#endif
			}
		}

		public virtual string operatingSystem
		{
			get { return SystemInfo.operatingSystem; }
		}

		public virtual string processorType
		{
			get { return SystemInfo.processorType; }
		}

		public virtual int systemMemorySize
		{
			get { return SystemInfo.systemMemorySize; }
		}
		#endregion

		#if UNITY_COMPATLEVEL_UNITY4
		public IWWW newWWW(string url, byte[] body, Dictionary<string, string> headers)
		{
			WWW www = new WWW(url, body, DictToHash(headers));
			return new UnityWWW(www);
		}
		
		private Hashtable DictToHash(Dictionary<string, string> headers)
		{
			var result = new Hashtable();
			foreach (var kvp in headers)
				result[kvp.Key] = kvp.Value;
			return result;
		}
		#else
		public IWWW newWWW(string url, byte[] body, Dictionary<string, string> headers)
		{
			WWW www = new WWW(url, body, headers);
			return new UnityWWW(www);
		}
		#endif
		
	}
	
}
#endif