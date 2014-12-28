//
//  UnityAdsUnityWrapper.m
//  Copyright (c) 2013 Unity Technologies. All rights reserved.
//

#import "UnityAdsUnityWrapper.h"
#if UNITY_VERSION >= 420
#import "UnityAppController.h"
#else
#import "AppController.h"
#endif

static UnityAdsUnityWrapper *unityAds = NULL;
static NSString * currentNetwork = NULL;

void UnitySendMessage(const char* obj, const char* method, const char* msg);
void UnityPause(bool pause);

extern "C" {
  NSString* UnityAdsCreateNSString (const char* string) {
    return string ? [NSString stringWithUTF8String: string] : [NSString stringWithUTF8String: ""];
  }
  
  char* UnityAdsMakeStringCopy (const char* string) {
    if (string == NULL)
      return NULL;
    char* res = (char*)malloc(strlen(string) + 1);
    strcpy(res, string);
    return res;
  }
}

@interface UnityAdsUnityWrapper () <UnityAdsDelegate>
@property (nonatomic, strong) NSString* gameObjectName;
@property (nonatomic, strong) NSString* gameId;
@end

@implementation UnityAdsUnityWrapper

- (id)initWithGameId:(NSString*)gameId testModeOn:(bool)testMode debugModeOn:(bool)debugMode withGameObjectName:(NSString*)gameObjectName {
  self = [super init];
  
  if (self != nil) {
    self.gameObjectName = gameObjectName;
    self.gameId = gameId;
    
    [[UnityAds sharedInstance] setDelegate:self];
    [[UnityAds sharedInstance] setDebugMode:debugMode];
    [[UnityAds sharedInstance] setTestMode:testMode];
    [[UnityAds sharedInstance] startWithGameId:gameId andViewController:UnityGetGLViewController()];
  }
  
  return self;
}

- (void)unityAdsVideoCompleted:(NSString *)rewardItemKey skipped:(BOOL)skipped {
  NSString *parameters = [NSString stringWithFormat:@"%@;%@", rewardItemKey, skipped ? @"true" : @"false"];
  UnitySendMessage(UnityAdsMakeStringCopy([self.gameObjectName UTF8String]), "onVideoCompleted", [parameters UTF8String]);
}

- (void)unityAdsWillShow {
}

- (void)unityAdsDidShow {
  UnitySendMessage(UnityAdsMakeStringCopy([self.gameObjectName UTF8String]), "onShow", "");
  UnityPause(true);
}

- (void)unityAdsWillHide {
}

- (void)unityAdsDidHide {
  UnityPause(false);
  UnitySendMessage(UnityAdsMakeStringCopy([self.gameObjectName UTF8String]), "onHide", "");
}

- (void)unityAdsWillLeaveApplication {
}

- (void)unityAdsVideoStarted {
  UnitySendMessage(UnityAdsMakeStringCopy([self.gameObjectName UTF8String]), "onVideoStarted", "");
}

- (void)unityAdsFetchCompleted:(NSString *)network {
  UnitySendMessage(UnityAdsMakeStringCopy([self.gameObjectName UTF8String]), "onFetchCompleted", [network UTF8String]);
}

- (void)unityAdsFetchFailed {
  UnitySendMessage(UnityAdsMakeStringCopy([self.gameObjectName UTF8String]), "onFetchFailed", "");
}


extern "C" {
  void init (const char *gameId, bool testMode, bool debugMode, const char *gameObjectName) {
    if (unityAds == NULL) {
      unityAds = [[UnityAdsUnityWrapper alloc] initWithGameId:UnityAdsCreateNSString(gameId) testModeOn:testMode debugModeOn:debugMode withGameObjectName:UnityAdsCreateNSString(gameObjectName)];
    }
  }
  
	bool show (const char * rawZoneId, const char * rawRewardItemKey, const char * rawOptionsString) {
    NSString * zoneId = UnityAdsCreateNSString(rawZoneId);
    NSString * rewardItemKey = UnityAdsCreateNSString(rawRewardItemKey);
    NSString * optionsString = UnityAdsCreateNSString(rawOptionsString);
    
    NSMutableDictionary *optionsDictionary = nil;
    if([optionsString length] > 0) {
      optionsDictionary = [[NSMutableDictionary alloc] init];
      [[optionsString componentsSeparatedByString:@","] enumerateObjectsUsingBlock:^(id rawOptionPair, NSUInteger idx, BOOL *stop) {
        NSArray *optionPair = [rawOptionPair componentsSeparatedByString:@":"];
        [optionsDictionary setValue:optionPair[1] forKey:optionPair[0]];
      }];
    }
    
    if ([[UnityAds sharedInstance] canShowAds:currentNetwork] && [[UnityAds sharedInstance] canShow]) {
      
      if([rewardItemKey length] > 0) {
        [[UnityAds sharedInstance] setZone:zoneId withRewardItem:rewardItemKey];
      } else {
        [[UnityAds sharedInstance] setZone:zoneId];
      }
      
      return [[UnityAds sharedInstance] show:optionsDictionary];
    }
    
    return false;
  }
	
	void hide () {
    [[UnityAds sharedInstance] hide];
  }
	
	bool isSupported () {
    return [UnityAds isSupported];
  }
	
	const char* getSDKVersion () {
    return UnityAdsMakeStringCopy([[UnityAds getSDKVersion] UTF8String]);
  }
  
	bool canShowAds (const char * rawNetwork) {
    return [[UnityAds sharedInstance] canShowAds:UnityAdsCreateNSString(rawNetwork)];
  }
  
	bool canShow () {
    return [[UnityAds sharedInstance] canShow];
  }
  
  void setNetworks(const char * rawNetworks) {
    NSString * networks = UnityAdsCreateNSString(rawNetworks);
    [[UnityAds sharedInstance] setNetworks:networks];
  }
  
  void setNetwork(const char * rawNetwork) {
    currentNetwork = UnityAdsCreateNSString(rawNetwork);
    [[UnityAds sharedInstance] setNetwork:currentNetwork];
  }
	
	bool hasMultipleRewardItems () {
    return [[UnityAds sharedInstance] hasMultipleRewardItems];
  }
	
	const char* getRewardItemKeys () {
    NSArray *keys = [[UnityAds sharedInstance] getRewardItemKeys];
    NSString *keyString = @"";
    
    for (NSString *key in keys) {
      if ([keyString length] <= 0) {
        keyString = [NSString stringWithFormat:@"%@", key];
      }
      else {
        keyString = [NSString stringWithFormat:@"%@;%@", keyString, key];
      }
    }
    
    return UnityAdsMakeStringCopy([keyString UTF8String]);
  }
  
	const char* getDefaultRewardItemKey () {
    return UnityAdsMakeStringCopy([[[UnityAds sharedInstance] getDefaultRewardItemKey] UTF8String]);
  }
  
	const char* getCurrentRewardItemKey () {
    return UnityAdsMakeStringCopy([[[UnityAds sharedInstance] getCurrentRewardItemKey] UTF8String]);
  }
  
	bool setRewardItemKey (const char *rewardItemKey) {
    return [[UnityAds sharedInstance] setRewardItemKey:UnityAdsCreateNSString(rewardItemKey)];
  }
	
	void setDefaultRewardItemAsRewardItem () {
    [[UnityAds sharedInstance] setDefaultRewardItemAsRewardItem];
  }
  
	const char* getRewardItemDetailsWithKey (const char *rewardItemKey) {
    if (rewardItemKey != NULL) {
      NSDictionary *details = [[UnityAds sharedInstance] getRewardItemDetailsWithKey:UnityAdsCreateNSString(rewardItemKey)];
      return UnityAdsMakeStringCopy([[NSString stringWithFormat:@"%@;%@", [details objectForKey:kUnityAdsRewardItemNameKey], [details objectForKey:kUnityAdsRewardItemPictureKey]] UTF8String]);
    }
    return UnityAdsMakeStringCopy("");
  }
  
  const char *getRewardItemDetailsKeys () {
    return UnityAdsMakeStringCopy([[NSString stringWithFormat:@"%@;%@", kUnityAdsRewardItemNameKey, kUnityAdsRewardItemPictureKey] UTF8String]);
  }
  
  void setDebugMode(bool debugMode) {
    [[UnityAds sharedInstance] setDebugMode:debugMode];
  }

  void enableUnityDeveloperInternalTestMode () {
  	[[UnityAds sharedInstance] enableUnityDeveloperInternalTestMode];
  }

}

@end
