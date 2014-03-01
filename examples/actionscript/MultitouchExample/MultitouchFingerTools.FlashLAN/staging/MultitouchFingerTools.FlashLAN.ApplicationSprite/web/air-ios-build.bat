echo off
rem http://help.adobe.com/en_US/flex/mobileapps/WS19f279b149e7481c-24dc70c812b9cbf7285-8000.html
rem http://forum.starling-framework.org/topic/wrong-wmode-with-adobeair-32-desktop
rem call createapk.bat


set e=%cd%
set swf=MultitouchFingerTools.FlashLAN.ApplicationSprite.swf
set ipa=air.ipa
set xml=air-descriptor.xml

"X:\jsc.internal.svn\keystore\carlo-lenovo\air13.com.abstractatech-package.and.sign.ios.bat" %e% %swf% %ipa% %xml%
