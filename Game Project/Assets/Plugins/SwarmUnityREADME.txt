*****************************
*                           *
* Swarm Unity Plugin & Demo *
*                           *
* Version: 1.1.2            *
*****************************



1.0 DESCRIPTION

Welcome to the Swarm Unity Plugin & Demo!  Swarm is a great way to quickly add social features such as Leaderboards, Achievements, Virtual Stores, Cloud Data Saves, and more.  It's a free service we provide to make your life easy.  We take care of all of that "other stuff" so you can focus on creating great content.



2.0 SUPPORTED PLATFORMS

Android is currently the only supported platform.  The Swarm Unity Plugin relies on some deeper features of Android and thus will only run on a physical Android device (i.e., won't run in the Unity Editor itself).



3.0 Contents

   A. SwarmUnity/Demo/ - contains a pre-compiled SwarmUnityDemo.apk that runs right out of the box (on an Android device) as well as all of the source

   B. SwarmUnity/Plugin/ - contains the all of the script files you need in order to start using Swarm's features in your own games. However, in order to use the plugin, you'll have to setup your developer account by visiting http://swarmconnect.com and clicking on the SIGN UP link at the top of the page.

   C. Documentation is in each folder to describe more about the folder's contents.
   

4.0 GETTING STARTED

We highly recommend that you visit the Swarm website (http://swarmconnect.com), where you get learn more about Swarm and the features that it offers you.  On the Swarm website, you'll be able to learn all about the platform, create your developer account, setup your Swarm Admin Panel, and find more documentation.  To create your developer account, click on the SIGN UP link at the top of the website.

Next, it's probably a good idea to install and run the SwarmUnityDemo.apk (found in the SwarmUnityDemo folder) to get a feel for what's available.  Then, take a peek at the documentation, follow the demo tutorial, or just jump right in and import the plugin into your own project.


5.0 RELEASE NOTES

	5.1 Version 1.1.2
		5.1.1 Added function to support launching apps in alternative markets outside of Google Play (call Swarm.enableAlternativeMarketCompatability() in status == SwarmLoginManager.USER_LOGGED_IN in SwarmLoginManager.addLoginListener(...))
		5.1.2 Added functions to add or remove coin providers (must be called after login or in the USER_LOGGED_IN case of the login listener).  See SwarmStore.addCoinProvider and SwarmStore.removeCoinProvider.

	5.2 Version 1.1.0

		5.2.1 Improved compatibility with the Prime31 Social Plugin
		5.2.2 Internationalization (Translations) can now be performed by adding additional strings.xml files in res/values/ or editing the existing one.
		5.2.3 Added xhdpi images so that Swarm screens look better on high resolution devices
		5.2.4 Improved UI of Swarm screens
		5.2.5 Minor fixes and enhancements added throughout

	5.3 Version 1.0.X
		5.3.1 Initial release

6.0 UPGRADE INSTRUCTIONS
	
If upgrading from Version 1.0.X, replace the existing contents of your Plugins/Android/ directory with the contents of the Plugins/Android/ directory conatined in the new version.


7.0 SUPPORT

For more information, please contact support@swarmconnect.com.
