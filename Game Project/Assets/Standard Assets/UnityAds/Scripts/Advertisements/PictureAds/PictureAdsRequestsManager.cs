namespace UnityEngine.Advertisements {
  using System;
  using UnityEngine;
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine.Advertisements;
  using UnityEngine.Advertisements.HTTPLayer;
  using UnityEngine.Advertisements.Event;

	internal class PictureAdsRequest {
		public delegate void jsonAvailable(string jsonData);
		public delegate void resourcesAvailable();
		public delegate void operationCompleteDelegate();
		jsonAvailable _jsonAvailable;
		resourcesAvailable _resourcesAvailable;
		operationCompleteDelegate _operationCompleteDelegate;
		Dictionary <string, ImageType> imageTypes;
		Dictionary <string, ImageOrientation> imageOrientations;
		int downloadedResourcesCount = 0;
	  int[] retryDelays = { 15, 30, 90, 240 };
		string network = null;
		public PictureAd ad;
		public PictureAdsRequest(string network) {
			this.network = network;
			imageTypes = new Dictionary<string, ImageType>();
			imageOrientations = new Dictionary<string, ImageOrientation>();
		}
		
		public void setJsonAvailableDelegate(jsonAvailable action) {
			_jsonAvailable = action;
		}
		
		public void setResourcesAvailableDelegate(resourcesAvailable action) {
			_resourcesAvailable = action;
		}

		public void setOperationCompleteDelegate(operationCompleteDelegate action) {
			_operationCompleteDelegate = action;
		}
		
		public void downloadAssetsForPictureAd(PictureAd ad) {
			downloadedResourcesCount = 0;
			executeRequestForResource(ad, ImageOrientation.Landscape, ImageType.Base);
			executeRequestForResource(ad, ImageOrientation.Landscape, ImageType.Frame);
			executeRequestForResource(ad, ImageOrientation.Landscape, ImageType.Close);
			executeRequestForResource(ad, ImageOrientation.Portrait, ImageType.Base);
			executeRequestForResource(ad, ImageOrientation.Portrait, ImageType.Frame);
		}
		
		public void downloadJson() {
			string requestURLString = requestURL();
			HTTPRequest jsonRequest = new HTTPRequest ("POST", requestURLString);
			jsonRequest.addHeader ("Content-Type", "application/json");
			string jsonInfo = jsonPayload();
			jsonRequest.setPayload (jsonInfo);
			HTTPManager.sendRequest(jsonRequest, HTTPJsonCallback, retryDelays, 20 * 60);
		}

		private void HTTPJsonCallback(HTTPResponse response) {
			if(response.dataLength == 0) return;
			string jsonString = System.Text.Encoding.UTF8.GetString(response.data, 0, response.dataLength);
			EventManager.sendAdplanEvent(Engine.Instance.AppId);
			_jsonAvailable(jsonString);
			_operationCompleteDelegate();
		}
		
		string requestURL () {
			string gameID = Engine.Instance.AppId;
			return Settings.pictureAdsEndpoint + @"/v2/picture/" + gameID + "/campaigns";
		}
		
		void executeRequestForResource(PictureAd ad, ImageOrientation imageOrientation, ImageType imageType) {
			string filePath = ad.getLocalImageURL(imageOrientation, imageType);
			if (System.IO.File.Exists(filePath)) {
				downloadedResourcesCount++;
				if(downloadedResourcesCount == PictureAd.expectedResourcesCount) {
					_resourcesAvailable ();
					_operationCompleteDelegate();
				}
				return;
			}
			string url = ad.getRemoteImageURL(imageOrientation, imageType);
			HTTPRequest pictureURLRequest = new HTTPRequest(url);
			imageTypes[url] = imageType;
			imageOrientations[url] = imageOrientation;
			HTTPManager.sendFileRequest(pictureURLRequest, HTTPFileCallback, retryDelays, 20 * 60);
		}

		private void HTTPFileCallback(HTTPResponse pictureURLRequestResponse) {
			downloadedResourcesCount ++;
			if(pictureURLRequestResponse.dataLength != 0)
				System.IO.File.WriteAllBytes(ad.getLocalImageURL(imageOrientations[pictureURLRequestResponse.url], imageTypes[pictureURLRequestResponse.url]), pictureURLRequestResponse.data);
			
			if(downloadedResourcesCount == PictureAd.expectedResourcesCount) {
				_resourcesAvailable ();
				_operationCompleteDelegate();
			}
		}
		
		string jsonPayload() {
			return DeviceInfo.adRequestJSONPayload(this.network);
		}
	}

 	internal class PictureAdsRequestsManager {
		static PictureAdsRequestsManager _inst;
		Stack <PictureAdsRequest> _requestsForJSON;
		Stack <PictureAdsRequest> _requestsForResources;

		PictureAdsRequestsManager () {
			_requestsForJSON = new Stack <PictureAdsRequest>();
			_requestsForResources = new Stack <PictureAdsRequest>();
		}

		public static PictureAdsRequestsManager sharedInstance() {
			if (_inst == null) _inst = new PictureAdsRequestsManager();
			return _inst;
		}

		public void downloadJson(string network, PictureAdsManager manager) {
			PictureAdsRequest request = new PictureAdsRequest(network);
			request.setJsonAvailableDelegate(manager.jsonAvailableDelegate);
			request.setOperationCompleteDelegate(jsonOperationComplete);
			_requestsForJSON.Push(request);
			if(_requestsForJSON.Count == 1)
				RequestsForJSONLoop();
		}

		public void downloadResourcesForAd(string network, PictureAdsManager manager, PictureAd ad) {
			PictureAdsRequest request = new PictureAdsRequest(network);
			request.setResourcesAvailableDelegate(manager.resourcesAvailableDelegate);
			request.setOperationCompleteDelegate(resourcesOperationComplete);
			request.ad = ad;
			_requestsForResources.Push(request);
			if(_requestsForResources.Count == 1)
				RequestsForResourcesLoop();
		}

		void jsonOperationComplete() {
			if(_requestsForJSON.Count == 0) return;
			RequestsForJSONLoop();
		}
		
		void resourcesOperationComplete() {
			if(_requestsForResources.Count == 0) return;
			RequestsForResourcesLoop();
		}

		void RequestsForJSONLoop() {
			if(_requestsForJSON.Count == 0) return;
			PictureAdsRequest request = _requestsForJSON.Pop();
			if (request != null)
				request.downloadJson();
		}

		void RequestsForResourcesLoop() {
			if(_requestsForResources.Count == 0) return;
			PictureAdsRequest request = _requestsForResources.Pop();
			if (request != null)
				request.downloadAssetsForPictureAd(request.ad);
		}
  }
}
