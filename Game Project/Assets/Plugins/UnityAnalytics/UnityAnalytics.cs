#if UNITY_IPHONE || UNITY_ANDROID || (UNITY_STANDALONE && UNITY_EDITOR)
#define UNITY_ANALYTICS_SUPPORTED_PLATFORM
#endif

using System;
using System.Collections.Generic;

namespace UnityEngine.Cloud.Analytics
{
	public enum SexEnum
	{
		M,
		F,
		U
	}

	public enum AnalyticsResult
	{
		Ok,
		NotInitialized,
		AnalyticsDisabled,
		TooManyItems,
		SizeLimitReached,
		TooManyRequests,
		InvalidData,
		UnsupportedPlatform
	}

	public enum LogLevel
	{
		None,
		Error,
		Warning,
		Info
	}

	public static class UnityAnalytics
	{
		public static AnalyticsResult StartSDK(string appId)
		{
			#if UNITY_ANALYTICS_SUPPORTED_PLATFORM
			IUnityAnalyticsSession session = UnityAnalytics.GetSingleton();
			return (AnalyticsResult)session.StartWithAppId(appId);
			#else
			return AnalyticsResult.UnsupportedPlatform;
			#endif
		}

		public static void SetLogLevel(LogLevel logLevel, bool enableLogging=true)
		{
			#if UNITY_ANALYTICS_SUPPORTED_PLATFORM
			Logger.EnableLogging = enableLogging;
			Logger.SetLogLevel((int)logLevel);
			#endif
		}
		
		public static AnalyticsResult SetUserId(string userId)
		{
			#if UNITY_ANALYTICS_SUPPORTED_PLATFORM
			IUnityAnalyticsSession session = UnityAnalytics.GetSingleton();
			return (AnalyticsResult)session.SetUserId(userId);
			#else
			return AnalyticsResult.UnsupportedPlatform;
			#endif
		}

		public static AnalyticsResult SetUserGender(SexEnum gender)
		{
			#if UNITY_ANALYTICS_SUPPORTED_PLATFORM
			IUnityAnalyticsSession session = UnityAnalytics.GetSingleton();
			return (AnalyticsResult)session.SetUserGender( gender==SexEnum.M ? "M" : gender==SexEnum.F ? "F" : "U" );
			#else
			return AnalyticsResult.UnsupportedPlatform;
			#endif
		}

		public static AnalyticsResult SetUserBirthYear(int birthYear)
		{
			#if UNITY_ANALYTICS_SUPPORTED_PLATFORM
			IUnityAnalyticsSession session = UnityAnalytics.GetSingleton();
			return (AnalyticsResult)session.SetUserBirthYear(birthYear);
			#else
			return AnalyticsResult.UnsupportedPlatform;
			#endif
		}

		public static AnalyticsResult Transaction(string productId, decimal amount, string currency)
		{
			#if UNITY_ANALYTICS_SUPPORTED_PLATFORM
			IUnityAnalyticsSession session = UnityAnalytics.GetSingleton();
			return (AnalyticsResult)session.Transaction(productId, amount, currency, null, null);
			#else
			return AnalyticsResult.UnsupportedPlatform;
			#endif
		}

		public static AnalyticsResult Transaction(string productId, decimal amount, string currency, string receiptPurchaseData, string signature)
		{
			#if UNITY_ANALYTICS_SUPPORTED_PLATFORM
			IUnityAnalyticsSession session = UnityAnalytics.GetSingleton();
			return (AnalyticsResult)session.Transaction(productId, amount, currency, receiptPurchaseData, signature);
			#else
			return AnalyticsResult.UnsupportedPlatform;
			#endif
		}

		public static AnalyticsResult CustomEvent(string customEventName, IDictionary<string, object> eventData)
		{
			#if UNITY_ANALYTICS_SUPPORTED_PLATFORM
			IUnityAnalyticsSession session = UnityAnalytics.GetSingleton();
			return (AnalyticsResult)session.CustomEvent(customEventName, eventData);
			#else
			return AnalyticsResult.UnsupportedPlatform;
			#endif
		}
		#if UNITY_ANALYTICS_SUPPORTED_PLATFORM
		private static SessionImpl s_Implementation;
		private static IUnityAnalyticsSession GetSingleton()
		{
			if (s_Implementation == null) {
				Logger.loggerInstance = new UnityLogger();
				IPlatformWrapper platformWrapper = PlatformWrapper.platform;
				IFileSystem fileSystem = new FileSystem();
				ICoroutineManager coroutineManager = new UnityCoroutineManager();
				s_Implementation = new SessionImpl(platformWrapper, coroutineManager, fileSystem);
				GameObserver.CreateComponent(platformWrapper, s_Implementation);
			}
			return s_Implementation;
		}
		#endif
	}
}
