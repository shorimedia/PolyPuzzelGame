namespace UnityEngine.Advertisements {
  using System;
  using UnityEngine;
  using System.Collections;
  using System.Collections.Generic;
  using System.IO;
  using UnityEngine.Advertisements;
  using UnityEngine.Advertisements.Event;
  using UnityEngine.Advertisements.HTTPLayer;

  internal class PictureAdsFrameManager : MonoBehaviour {
  	Dictionary <ImageOrientation , Dictionary <ImageType, Texture2D > > textures;
    Dictionary <ImageOrientation , Dictionary <ImageType, Rect > > texturesRects;
    bool adIsShowing = false;
    bool _isClosed = true;
    ImageOrientation screenOrientation = ImageOrientation.Landscape;
		Texture2D blackTex = null;
    float offset = 0;
		float increase = 0;
		float currentTime = 0;
		float previousTime = 0;
		float animationDuration = 0.5f;
    public PictureAdsManager manager;
    PictureAd _ad = null;

		public PictureAdsFrameManager() {
			textures = new Dictionary <ImageOrientation , Dictionary <ImageType, Texture2D > >();
			texturesRects = new Dictionary <ImageOrientation , Dictionary <ImageType, Rect > >();
		}
		
		public bool adIsClosed() {
			return _isClosed;
		}
		
		public void initAd(PictureAd ad) {
			_ad = ad;
			updateRects(ad);
			updateTextures(ad);
			_isClosed = false;
		}
		
		public bool isShowingAd() {
			return adIsShowing;
		}
		
		public void showAd() {
			currentTime = previousTime = Time.realtimeSinceStartup;
			adIsShowing = true;
		}

    Texture2D textureFromBytes(byte[] bytes) {
      Texture2D texture2D = new Texture2D(1, 1);
      texture2D.LoadImage(bytes);
      return texture2D;
    }

    byte[] textureBytesForFrame(string imageURL) {
      byte[] array = null;
      using(FileStream fileStream = File.OpenRead(imageURL)) {
        int num = (int)fileStream.Length;
        array = new byte[num];
        int num2 = 0;
        int num3;
        while((num3 = fileStream.Read (array, num2, num - num2)) > 0) {
          num2 += num3;
        }
      }
      return array;
    }

    Rect rectWithPrecentage(int precentageSize, ImageOrientation imageOrientation) {
      bool isLandscape = (bool)(imageOrientation == ImageOrientation.Landscape);
      float heightProportion = Screen.width > Screen.height ? (float)9 / (float)16 : (float)16 / (float)9;
      float widthProportion = (float)1 / (float)heightProportion;
      int majorSizeValue = (Screen.width > Screen.height ? Screen.width : Screen.height) * precentageSize / 100;
      float finalTextureHeight = (float)majorSizeValue * (Screen.width > Screen.height ? (float)heightProportion : (float)widthProportion);
      finalTextureHeight = (finalTextureHeight > 
        ((Screen.width > Screen.height ? Screen.height : Screen.width) * precentageSize / 100) ?
        ((Screen.width > Screen.height ? Screen.height : Screen.width) * precentageSize / 100) : finalTextureHeight);
      Rect landscapeRect = new Rect(((Screen.width > Screen.height ? Screen.width : Screen.height) - majorSizeValue) / 2,
                                     ((Screen.width > Screen.height ? Screen.height : Screen.width) - finalTextureHeight) / 2,
                                     majorSizeValue,
                                     finalTextureHeight);

      Rect portraitRect = new Rect(((Screen.width > Screen.height ? Screen.height : Screen.width) - finalTextureHeight) / 2,
                                    ((Screen.width > Screen.height ? Screen.width : Screen.height) - majorSizeValue) / 2,
                                    finalTextureHeight,
                                    majorSizeValue);
      return (isLandscape ? landscapeRect : portraitRect);
    }

    void showPictureAd(int windowID) {
      getScreenOrientation();
	  	Color tmp = GUI.color;
	  	GUI.color = new Color(1,1,1,offset);
      GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blackTex);
      GUI.DrawTexture(texturesRects[screenOrientation][ImageType.Frame], textures[screenOrientation][ImageType.Frame]);
      GUI.DrawTexture(texturesRects[screenOrientation][ImageType.Base], textures[screenOrientation][ImageType.Base]);
      GUI.DrawTexture(texturesRects[screenOrientation][ImageType.Close], textures[screenOrientation][ImageType.Close]);

      if(Advertisement.UnityDeveloperInternalTestMode) {
        int stagingWidth = Screen.width / 10 * 2;
        int stagingHeight = Screen.height / 10 * 2;
        Texture2D stagingBg = new Texture2D(stagingWidth, stagingHeight);
        for(int i = 0; i < stagingWidth; i++) {
          for(int j = 0; j < stagingHeight; j++) {
            stagingBg.SetPixel(i, j, Color.black);
          }
        }
        stagingBg.Apply();

        GUIStyle stagingLabel = new GUIStyle();
        stagingLabel.normal.textColor = Color.red;
        stagingLabel.normal.background = stagingBg;
        stagingLabel.alignment = TextAnchor.MiddleCenter;

        GUI.Label(new Rect(Screen.width / 10 * 4, Screen.height / 10 * 4, Screen.width / 10 * 2, Screen.height / 10 * 2), "INTERNAL UNITY TEST BUILD\nDO NOT USE IN PRODUCTION", stagingLabel);
      }

	  	GUI.color = tmp;
		}
    
    void OnGUI() {
			if(!adIsShowing) return;
			Color tmp = GUI.color;
			GUI.color = new Color(1,1,1,0);
			GUI.ModalWindow(0, new Rect(0, 0, Screen.width, Screen.height), showPictureAd, "");
			GUI.color = tmp;
    }

    bool mouseInRect(Rect rect) {
      return Input.mousePosition.x >= rect.x && Input.mousePosition.x <= rect.x + rect.width
        && (Screen.height - Input.mousePosition.y) >= rect.y && (Screen.height - Input.mousePosition.y) <= rect.y + rect.height;
    }

		void showAnimation() {
			if (offset + increase < 1f)
				offset += increase;
			else
				offset = 1f;
		}

		void hideAnimation() {
			if (offset - increase > 0)
				offset -= increase;
			else {
				adIsShowing = false;
				textures.Clear();
				texturesRects.Clear();
				manager.pictureAdDidClosed();
				offset = 0.0f;
			}
		}

    void Update() {
			if(!adIsShowing)
				return;
			float distinct = currentTime - previousTime;
			increase = distinct/animationDuration;
			if(!_isClosed)
				showAnimation ();
			else
				hideAnimation ();
      
      if(Input.GetMouseButtonDown(0) && offset == 1f) {
        if(mouseInRect(texturesRects[screenOrientation][ImageType.CloseActiveArea])) {
          EventManager.sendCloseEvent(Engine.Instance.AppId, _ad.id, false);
          _isClosed = true;
          manager.pictureAdWillBeClosed();
          return;
       	}

        if(mouseInRect(texturesRects[screenOrientation][ImageType.Base])) {
          EventManager.sendClickEvent(Engine.Instance.AppId, _ad.id);
					manager.pictureAdClicked();
          HTTPRequest request = new HTTPRequest("POST", _ad.clickActionUrl);
          request.addHeader("Content-Type", "application/json");
          request.setPayload(DeviceInfo.adRequestJSONPayload(manager.network));
          request.execute((HTTPResponse response) => {
            if(response != null && response.data != null) {
              string jsonString = System.Text.Encoding.UTF8.GetString(response.data, 0, response.dataLength);
              object jsonObject = MiniJSON.Json.Deserialize(jsonString);
              if(jsonObject != null && jsonObject is Dictionary<string, object>) {
                Dictionary<string, object> json = (Dictionary<string, object>)jsonObject;
                string redirectLocation = (string)json["clickUrl"];
                if(redirectLocation != null)
                  Application.OpenURL(redirectLocation);
              }
            }
          });
          return;
        }
      }
			previousTime = currentTime;
			currentTime = Time.realtimeSinceStartup;
    }

    void initRect(ImageOrientation imageOrientation, ImageType imageType, Rect rect) {
      texturesRects[imageOrientation][imageType] = rect;
    }

    void initTexture(ImageOrientation imageOrientation, ImageType imageType, PictureAd ad) {
      textures[imageOrientation][imageType] = textureFromBytes(textureBytesForFrame(ad.getLocalImageURL(imageOrientation, imageType)));
    }

    void initRectsForOrientation(PictureAd ad, ImageOrientation imageOrientation) {
      Rect frameRect = rectWithPrecentage(ad.getImageSpace(ImageType.Frame), imageOrientation);
      Rect baseRect = rectWithPrecentage(ad.getImageSpace(ImageType.Base), imageOrientation);
      Rect closeButtonRect = rectWithPrecentage(ad.getImageSpace(ImageType.Close), imageOrientation);
      float maxV = Math.Max(closeButtonRect.width, closeButtonRect.height);
      closeButtonRect = new Rect((baseRect.x + baseRect.width) - closeButtonRect.width / 2, baseRect.y - closeButtonRect.height / 2, maxV, maxV);
      Rect closeActiveAreaRect = new Rect((float)(closeButtonRect.x - closeButtonRect.width * 0.3), 
                                           (float)(closeButtonRect.y - closeButtonRect.height * 0.3),
                                           (float)(closeButtonRect.width + closeButtonRect.width * 0.3),
                                           (float)(closeButtonRect.height + closeButtonRect.height * 0.3));
      initRect(imageOrientation, ImageType.Base, baseRect);
      initRect(imageOrientation, ImageType.Frame, frameRect);
      initRect(imageOrientation, ImageType.Close, closeButtonRect);
      initRect(imageOrientation, ImageType.CloseActiveArea, closeActiveAreaRect);
    }

    void initTextureForOrientation(PictureAd ad, ImageOrientation imageOrientation) {
	  	blackTex = new Texture2D(1, 1, TextureFormat.ARGB32, false);
	  	blackTex.SetPixel(0, 0, new Color(0.0f, 0.0f, 0.0f, 0.7f));
	  	blackTex.Apply();
      initTexture(imageOrientation, ImageType.Base, ad);
      initTexture(imageOrientation, ImageType.Frame, ad);
      initTexture(imageOrientation, ImageType.Close, ad);
    }

    void updateRects(PictureAd ad) {
      texturesRects.Clear();
      texturesRects[ImageOrientation.Landscape] = new Dictionary <ImageType, Rect >();
      texturesRects[ImageOrientation.Portrait] = new Dictionary <ImageType, Rect >();
      initRectsForOrientation(ad, ImageOrientation.Landscape);
      initRectsForOrientation(ad, ImageOrientation.Portrait);
      getScreenOrientation();
    }

    ImageOrientation getScreenOrientation() {
      return screenOrientation = Screen.width > Screen.height ? ImageOrientation.Landscape : ImageOrientation.Portrait;
    }

    void updateTextures(PictureAd ad) {
      textures.Clear();
      textures[ImageOrientation.Landscape] = new Dictionary <ImageType, Texture2D >();
      textures[ImageOrientation.Portrait] = new Dictionary <ImageType, Texture2D >();
      initTextureForOrientation(ad, ImageOrientation.Landscape);
      initTextureForOrientation(ad, ImageOrientation.Portrait);
    }
  }
}
