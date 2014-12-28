namespace UnityEngine.Advertisements {
  using UnityEngine;
  using System.Collections;
  using System.Collections.Generic;
  using System.IO;
  using UnityEngine.Advertisements.Event;
  using UnityEngine.Advertisements.HTTPLayer;

	internal class PictureAdsManager {	
    PictureAdsFrameManager framesManager = null;
    PictureAdsRequestsManager requestManager = null;
    PictureAd currentAd = null;
		bool jsonDownloaded = false;
		bool resourcesAreDownloaded = false;
		string _network = null;

		public delegate void PictureAdWillBeShown();
		public delegate void PictureAdReady();
		public delegate void PictureAdFailed();
		public delegate void PictureAdDidOpen();
		public delegate void PictureAdWillBeClosed();
		public delegate void PictureAdDidClosed();
		public delegate void PictureAdClicked();

		PictureAdDidClosed _pictureAdDidClosedDelegate;
		PictureAdWillBeShown _pictureAdWillBeShownDelegate;
		PictureAdReady _pictureAdReadyDelegate;
		PictureAdFailed _pictureAdFailedDelegate;
		PictureAdDidOpen _pictureAdDidOpenDelegate;
		PictureAdWillBeClosed _pictureAdWillBeClosed;
		PictureAdClicked _pictureAdClicked;

		public void setPictureAdClicked(PictureAdClicked action) {
			_pictureAdClicked = action;
		}

		public void setPictureAdWillBeClosed(PictureAdWillBeClosed action) {
			_pictureAdWillBeClosed = action;
		}

		public void setPictureAdDidClosedDelegate (PictureAdDidClosed action) {
			_pictureAdDidClosedDelegate = action;
    }

		public void setPictureAdWillBeShownDelegate (PictureAdWillBeShown action) {
			_pictureAdWillBeShownDelegate = action;
		}

		public void setPictureAdReadyDelegate (PictureAdReady action) {
			_pictureAdReadyDelegate = action;
		}

    public void setPictureAdFailedDelegate (PictureAdFailed action) {
      _pictureAdFailedDelegate = action;
    }

		public void setPictureAdDidOpenDelegate (PictureAdDidOpen action) {
			_pictureAdDidOpenDelegate = action;
		}
    
    public PictureAdsManager(string network) {
			requestManager = PictureAdsRequestsManager.sharedInstance();
			_network = network;
    }

    public void init() {
			EventManager.sendAdreqEvent(Engine.Instance.AppId);
      currentAd = null;
	  	jsonDownloaded = false;
	  	resourcesAreDownloaded = false;
      if (requestManager != null)
	  		requestManager.downloadJson(_network, this);
   	}

		public void pictureAdWillBeClosed() {
			_pictureAdWillBeClosed();
		}

		public void pictureAdClicked() {
			_pictureAdClicked();
		}
    
    public void pictureAdDidClosed() {
			framesManager = null;
			GameObject framesManagerHolder = GameObject.Find(@"UnityAdsFramesManagerHolder");
			GameObject.Destroy(framesManagerHolder);
			_pictureAdDidClosedDelegate();
    }

		public void pictureAdFailed() {
			framesManager = null;
			GameObject framesManagerHolder = GameObject.Find(@"UnityAdsFramesManagerHolder");
			GameObject.Destroy(framesManagerHolder);
			_pictureAdFailedDelegate(); 
		}

		void removeLocalResources (PictureAd ad) {
			if (!ad.adIsValid()) return;
			System.IO.File.Delete(ad.getLocalImageURL(ImageOrientation.Landscape, ImageType.Close));
			System.IO.File.Delete(ad.getLocalImageURL(ImageOrientation.Landscape, ImageType.Frame));
			System.IO.File.Delete(ad.getLocalImageURL(ImageOrientation.Landscape, ImageType.Base));
			System.IO.File.Delete(ad.getLocalImageURL(ImageOrientation.Portrait, ImageType.Close));
			System.IO.File.Delete(ad.getLocalImageURL(ImageOrientation.Portrait, ImageType.Base));
			System.IO.File.Delete(ad.getLocalImageURL(ImageOrientation.Portrait, ImageType.Frame));
		}

		public void resourcesAvailableDelegate () {
			resourcesAreDownloaded = true;
			_pictureAdReadyDelegate();
		}

    public void jsonAvailableDelegate(string jsonData) {
	  	jsonDownloaded = true;
      currentAd = PictureAdsParser.parseJSONString(jsonData, Application.temporaryCachePath + "/");
			if(currentAd == null || !currentAd.adIsValid()) {pictureAdFailed();return;}
      	requestManager.downloadResourcesForAd(_network, this, currentAd);
    }

    bool areResourcesReady() {
		  return jsonDownloaded && resourcesAreDownloaded;
    }

    public bool isAdAvailable() {
			return areResourcesReady() ? (currentAd.adIsValid() && currentAd.resourcesAreValid() && (framesManager != null ? framesManager.adIsClosed() : true) && !isShowingAd()) : false;
    }
    
    public bool isShowingAd() {
			return (framesManager != null ? framesManager.isShowingAd() : false);
    }

    public string network {
      get {
        return _network;
      }
    }

    public void showAd() {
			GameObject framesManagerHolder = GameObject.Find(@"UnityAdsFramesManagerHolder");
			if (framesManagerHolder == null) {
				framesManagerHolder = new GameObject(@"UnityAdsFramesManagerHolder");
				framesManager = framesManagerHolder.AddComponent<PictureAdsFrameManager>();
				framesManager.manager = this;
			}

      if(isAdAvailable()) {
        if(framesManager.adIsClosed()) {
          framesManager.initAd(currentAd);
	        EventManager.sendViewEvent(Engine.Instance.AppId, currentAd.id);
					_pictureAdWillBeShownDelegate();
	        framesManager.showAd();
					_pictureAdDidOpenDelegate();
				}
      }
    }
  }
}
