@echo off
rem http://help.adobe.com/en_US/flex/mobileapps/WS19f279b149e7481c-24dc70c812b9cbf7285-8000.html
rem http://forum.starling-framework.org/topic/wrong-wmode-with-adobeair-32-desktop
rem call createapk.bat


set e=%cd%
set swf=FlashHeatZeeker.TestDriversTouch.ApplicationSprite.swf
set ipa=air.ipa
set xml=air-descriptor.xml

echo build

:: ipad ? FPS
rem call "X:\jsc.internal.svn\keystore\carlo-lenovo\air13.com.abstractatech-package.and.sign.ios.bat" %e% %swf% %ipa% %xml%
:: rem ipad 5 fps
call "X:\jsc.internal.git\keystore\carlo-lenovo\air13.com.abstractatech-package.and.sign.ios.adhoc.bat" %e% %swf% %ipa% %xml%




echo off
rem http://help.adobe.com/en_US/flex/mobileapps/WS19f279b149e7481c-24dc70c812b9cbf7285-8000.html
rem http://forum.starling-framework.org/topic/wrong-wmode-with-adobeair-32-desktop
rem call createapk.bat
rem http://blogs.adobe.com/airodynamics/2012/07/17/installinguninstalling-ios-air-applications-on-ios-devices-using-adt/
rem https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140301

echo install
C:\util\AIRSDK_Compiler\bin\adt -installApp -platform ios -package air.ipa

rem No Devices Detected