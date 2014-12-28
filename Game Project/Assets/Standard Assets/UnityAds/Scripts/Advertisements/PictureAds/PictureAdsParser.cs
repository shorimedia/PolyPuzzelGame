namespace UnityEngine.Advertisements {
  using System;
  using System.Collections.Generic;
  using UnityEngine.Advertisements.MiniJSON;
  using UnityEngine;

  internal class PictureAdsParser {
    static string game__KEY = @"game";
    static string landscapeFramePicture__KEY = @"landscapeFramePicture";
    static string portraitFramePicture__KEY = @"portraitFramePicture";
    static string closeButtonPicture__KEY = @"closeButtonPicture";
    static string closeButtonDelay__KEY = @"closeButtonDelay";
    static string campaign__KEY = @"campaign";
    static string landscapePicture__KEY = @"landscapePicture";
    static string portraitPicture__KEY = @"portraitPicture";
    static string id__KEY = @"id";
    static string clickActionUrl__KEY = @"clickUrl";
    static string hasMoreCampaigns__KEY = @"hasMoreCampaigns";
    static string frameSpace__KEY = @"frameSpace";
    static string closeSpace__KEY = @"closeSpace";
    static string baseSpace__KEY = @"baseSpace";
    const int defaultFrameSpace = 90;
    const int defaultCloseButtonSpace = 5;
    const int defaultBaseSpace = 80;
    const int defaultCloseButtonDelay = 0;


		public static PictureAd parseJSONString(string jsonString, string localCachePath) {
			PictureAd pictureAd = new PictureAd();
			pictureAd.setImageSpace(ImageType.Base, defaultBaseSpace);
			pictureAd.setImageSpace(ImageType.Close, defaultCloseButtonSpace);
			pictureAd.setImageSpace(ImageType.Frame, defaultFrameSpace);
			pictureAd.closeButtonDelay = defaultCloseButtonDelay;
			
			if(jsonString == null || jsonString.Length == 0) return pictureAd;
			
			var dict = Json.Deserialize(jsonString) as Dictionary<string, object>;
			
			if(dict == null) return pictureAd;
			
			if(!dict.ContainsKey("data")) return pictureAd;
			
			Dictionary<string, object> dict2 = (Dictionary<string, object>)dict["data"];
			
			foreach(string assetKey in dict2.Keys) {
				if(assetKey == game__KEY) {
					var gameDict = (Dictionary <string, object>)dict2[assetKey];
					if(gameDict == null) return pictureAd;
					
					if(gameDict.ContainsKey(landscapeFramePicture__KEY))
						setupPathsForAd(pictureAd, localCachePath, (string)gameDict[landscapeFramePicture__KEY], ImageOrientation.Landscape, ImageType.Frame);
					
					if(gameDict.ContainsKey(portraitFramePicture__KEY))
						setupPathsForAd(pictureAd, localCachePath, (string)gameDict[portraitFramePicture__KEY], ImageOrientation.Portrait, ImageType.Frame);
					
					if(gameDict.ContainsKey(closeButtonPicture__KEY)) {
						setupPathsForAd(pictureAd, localCachePath, (string)gameDict[closeButtonPicture__KEY], ImageOrientation.Landscape, ImageType.Close);
						pictureAd.setImageURL(localPathForResource(localCachePath,(string)gameDict[closeButtonPicture__KEY]), 
						                      ImageURLType.Local, ImageOrientation.Portrait, ImageType.Close);
					}
					
					if(gameDict.ContainsKey(closeButtonDelay__KEY))
						pictureAd.closeButtonDelay = stringToInt((string)gameDict[closeButtonDelay__KEY].ToString());
					
					setImageSpace(pictureAd, ImageType.Frame, gameDict);
					setImageSpace(pictureAd, ImageType.Close, gameDict);
				}
				
				if(assetKey == campaign__KEY) {
					var campaignDict = (Dictionary <string, object>)dict2[assetKey];
					if(campaignDict == null) return pictureAd;
					
					if(campaignDict.ContainsKey(landscapePicture__KEY))
						setupPathsForAd(pictureAd, localCachePath, (string)campaignDict[landscapePicture__KEY], ImageOrientation.Landscape, ImageType.Base);
					
					if(campaignDict.ContainsKey(portraitPicture__KEY))
						setupPathsForAd(pictureAd, localCachePath, (string)campaignDict[portraitPicture__KEY], ImageOrientation.Portrait, ImageType.Base);
					
					if(campaignDict.ContainsKey(id__KEY))
						pictureAd.id = (string)campaignDict[id__KEY];
					
					if(campaignDict.ContainsKey(clickActionUrl__KEY))
						pictureAd.clickActionUrl = (string)campaignDict[clickActionUrl__KEY];
					
					setImageSpace(pictureAd, ImageType.Base, campaignDict);
				}
				
				if (assetKey == hasMoreCampaigns__KEY)
					bool.TryParse((string)dict2[assetKey].ToString(), out pictureAd.hasMoreCampaigns);
				
			}
			
			return pictureAd;
		}

		static int stringToInt(string v) {
      int value = 0;
      int.TryParse(v, out value);
      return value;
    }

		static void setImageSpace(PictureAd ad, ImageType imageType, Dictionary <string, object> dict) {
      string key = null;
      switch(imageType) {
        case ImageType.Base:
          key = baseSpace__KEY;
          break;

        case ImageType.Frame:
          key = frameSpace__KEY;
          break;

        case ImageType.Close:
          key = closeSpace__KEY;
          break;
      }
      if(dict == null || !dict.ContainsKey(key)) return;
      ad.setImageSpace(imageType, stringToInt((string)dict[key].ToString()));
    }

		static string localPathForResource (string localCachePath, string remotePath) {
			string[] strings = remotePath.Split('/');
			String tmp = strings[strings.Length - 2] + strings[strings.Length - 1];
			return localCachePath + tmp;
		}

		static void setupPathsForAd(PictureAd ad, string localCachePath, string remotePath, ImageOrientation orientation, ImageType imageType) {
			ad.setImageURL(remotePath, ImageURLType.Remote, orientation, imageType);
			ad.setImageURL(localPathForResource(localCachePath, remotePath), ImageURLType.Local, orientation, imageType);
		}
  }
}
