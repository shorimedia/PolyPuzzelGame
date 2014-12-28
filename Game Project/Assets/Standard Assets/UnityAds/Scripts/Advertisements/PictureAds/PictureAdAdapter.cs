namespace UnityEngine.Advertisements {
  using System;
  using System.Collections.Generic;

  internal class PictureAdAdapter : Adapter {
		PictureAdsManager _manager;
    public PictureAdAdapter(string adapterId) : base(adapterId) {}

    public override void Initialize(string zoneId, string adapterId, Dictionary<string, object> configuration) {
      string network = null;
      string platform = null;
      
      triggerEvent(EventType.initStart, EventArgs.Empty);

			if (configuration != null && configuration.ContainsKey(@"network"))
			network = (string)configuration[@"network"];

			platform = DeviceInfo.currentPlatform();
			if (network == null || network.Length == 0) {
				switch(platform) {
					case @"ios":
							network = @"picture_ios";
						break;
					case @"android":
							network = @"picture_android";
						break;
					default: 
							network = @"picture_editor";
						break;
				}
			}

			_manager = new PictureAdsManager(network);
			_manager.setPictureAdDidClosedDelegate(onPictureAdDidClosed);
			_manager.setPictureAdWillBeClosed(onPictureAdWillBeClosed);
			_manager.setPictureAdFailedDelegate(onPictureAdFailed);
			_manager.setPictureAdReadyDelegate(onPictureAdReady);
			_manager.setPictureAdWillBeShownDelegate(onPictureAdWillBeShown);
			_manager.setPictureAdDidOpenDelegate(onPictureAdDidOpen);
			_manager.setPictureAdClicked(onPictureAdClicked);
			_manager.init();
    }

		void onPictureAdClicked() {
			triggerEvent(EventType.adClicked, EventArgs.Empty);
		}

		void onPictureAdDidOpen() {
			triggerEvent(EventType.adDidOpen, EventArgs.Empty);
		}

		void onPictureAdWillBeShown() {
			triggerEvent(EventType.adWillOpen, EventArgs.Empty);
		}

		void onPictureAdReady() {
			triggerEvent(EventType.initComplete, EventArgs.Empty);
			triggerEvent(EventType.adAvailable, EventArgs.Empty);
		}
    
    void onPictureAdFailed() {
			triggerEvent(EventType.initFailed, EventArgs.Empty);
      triggerEvent(EventType.error, EventArgs.Empty);
    }

		void onPictureAdWillBeClosed() {
			triggerEvent(EventType.adWillClose, EventArgs.Empty);
		}
    
    void onPictureAdDidClosed() {
      triggerEvent(EventType.adFinished, EventArgs.Empty);
      triggerEvent(EventType.adDidClose, EventArgs.Empty);
    }

    public override void RefreshAdPlan() { Utils.LogDebug("Got refresh ad plan request for picture ads"); }
    public override void StartPrecaching() {}
    public override void StopPrecaching() {}

    public override bool isReady(string zoneId, string adapterId) {
			return _manager.isAdAvailable();
    }

    public override void Show(string zoneId, string adapterId, ShowOptions options = null) {
			_manager.showAd();
    }

    public override bool isShowing() {
			return _manager.isShowingAd();
    }
  }
}
