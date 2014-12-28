namespace UnityEngine.Advertisements {
  using System;
  using System.Collections.Generic;

	public enum ImageType {
		Frame = 0,
		Base,
		Close,
		CloseActiveArea,
	}
	
	public enum ImageURLType {
		Local = 0,
		Remote,
	}
	
	public enum ImageOrientation {
		Landscape = 0,
		Portrait
	}

  internal class PictureAd {
		private Dictionary <ImageURLType, Dictionary <ImageOrientation, Dictionary <ImageType, string> > > imageURLs;
    private Dictionary <ImageType, int> imageSpaces;
		public const int expectedResourcesCount = 5;
    public string id = null;
    public string clickActionUrl = null;
    public int closeButtonDelay = -1;
    public bool hasMoreCampaigns = false;
    public PictureAd() {
    	imageSpaces = new Dictionary <ImageType, int>();
      imageURLs = new Dictionary <ImageURLType, Dictionary <ImageOrientation, Dictionary <ImageType, string> > >();
      imageURLs[ImageURLType.Local] = new Dictionary <ImageOrientation, Dictionary <ImageType, string> >();
      imageURLs[ImageURLType.Remote] = new Dictionary <ImageOrientation, Dictionary <ImageType, string> >();
      imageURLs[ImageURLType.Local][ImageOrientation.Portrait] = new Dictionary <ImageType, string>();
      imageURLs[ImageURLType.Local][ImageOrientation.Landscape] = new Dictionary <ImageType, string>();
      imageURLs[ImageURLType.Remote][ImageOrientation.Portrait] = new Dictionary <ImageType, string>();
      imageURLs[ImageURLType.Remote][ImageOrientation.Landscape] = new Dictionary <ImageType, string>();
    }

    public void setImageURL(string url, ImageURLType imageURLType, ImageOrientation pictureOrientation, ImageType imageType) { 
    	if(url == null || imageURLs == null || !imageURLs.ContainsKey(imageURLType)) return;
      	var imageURLTypeDict = imageURLs[imageURLType];
      if(imageURLTypeDict == null || !imageURLTypeDict.ContainsKey(pictureOrientation)) return;
      	var pictureOrientationDict = imageURLTypeDict[pictureOrientation];
			if(pictureOrientationDict == null) return;
				pictureOrientationDict[imageType] = url;
			System.Console.WriteLine(pictureOrientationDict[imageType]);
    }

    public string getImageURL(ImageURLType imageURLType, ImageOrientation pictureOrientation, ImageType imageType) {
     	if(imageURLs == null || !imageURLs.ContainsKey(imageURLType)) return null;
				var imageURLTypeDict = imageURLs[imageURLType];
     	if(imageURLTypeDict == null || !imageURLTypeDict.ContainsKey(pictureOrientation)) return null;
				var pictureOrientationDict = imageURLTypeDict[pictureOrientation];
    	if(pictureOrientationDict == null || !pictureOrientationDict.ContainsKey(imageType)) return null;
      	return pictureOrientationDict[imageType];
    	}

    public string getLocalImageURL(ImageOrientation pictureOrientation, ImageType imageType) {
      return getImageURL(ImageURLType.Local, pictureOrientation, imageType);
    }

    public string getRemoteImageURL(ImageOrientation pictureOrientation, ImageType imageType) {
      return getImageURL(ImageURLType.Remote, pictureOrientation, imageType);
    }

    public void setImageSpace(ImageType imageType, int space) {
      if (imageSpaces == null) return;
      	imageSpaces[imageType] = space;
    }

    public int getImageSpace(ImageType imageType) {
      if (imageSpaces == null || !imageSpaces.ContainsKey(imageType)) return -1;
     	 return imageSpaces[imageType];
    }

		public bool resourcesAreValid() {
			bool filesExists = 
				  System.IO.File.Exists(getLocalImageURL(ImageOrientation.Landscape, ImageType.Base)) &&
					System.IO.File.Exists(getLocalImageURL(ImageOrientation.Landscape, ImageType.Frame)) &&
					System.IO.File.Exists(getLocalImageURL(ImageOrientation.Landscape, ImageType.Close)) &&
					System.IO.File.Exists(getLocalImageURL(ImageOrientation.Portrait, ImageType.Base)) &&
					System.IO.File.Exists(getLocalImageURL(ImageOrientation.Portrait, ImageType.Frame)) &&
					System.IO.File.Exists(getLocalImageURL(ImageOrientation.Portrait, ImageType.Close));
			return filesExists;
		}
		
		public bool adIsValid() {
			return 
				isValidStringField(id) && 
					isValidStringField(clickActionUrl) && 
					closeButtonDelay >= 0 && 
					urlsAreValid() && 
					spacesAreValid();
		}

		bool spacesAreValid() {
			return getImageSpace(ImageType.Base) > 0 && getImageSpace(ImageType.Frame) > 0 && getImageSpace(ImageType.Close) > 0; 
		}

		bool isValidStringField (string field) {
			return field != null && field.Length != 0;
		}

		bool isValidLocalURL (string url) {
			return isValidStringField(url);
		}

		bool isValidRemoteURL (string url) {
			Uri result;
			return Uri.TryCreate(url, UriKind.Absolute, out result);
		}

		bool urlsAreValid () {
			bool remoteURLsAreValid = 
				isValidRemoteURL(getRemoteImageURL(ImageOrientation.Landscape, ImageType.Base)) &&
				isValidRemoteURL(getRemoteImageURL(ImageOrientation.Landscape, ImageType.Frame)) && 
				isValidRemoteURL(getRemoteImageURL(ImageOrientation.Landscape, ImageType.Close)) && 
				isValidRemoteURL(getRemoteImageURL(ImageOrientation.Portrait, ImageType.Base)) && 
				isValidRemoteURL(getRemoteImageURL(ImageOrientation.Portrait, ImageType.Frame));

			bool localURLsAreValid = 
				isValidLocalURL(getLocalImageURL(ImageOrientation.Landscape, ImageType.Base)) &&
				isValidLocalURL(getLocalImageURL(ImageOrientation.Landscape, ImageType.Frame)) && 
				isValidLocalURL(getLocalImageURL(ImageOrientation.Landscape, ImageType.Close)) && 
				isValidLocalURL(getLocalImageURL(ImageOrientation.Portrait, ImageType.Base)) && 
				isValidLocalURL(getLocalImageURL(ImageOrientation.Portrait, ImageType.Frame)) && 
				isValidLocalURL(getLocalImageURL(ImageOrientation.Portrait, ImageType.Close));
			return remoteURLsAreValid && localURLsAreValid;
		}
  }
}
