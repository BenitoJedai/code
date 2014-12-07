@echo off
rem http://help.adobe.com/en_US/flex/mobileapps/WS19f279b149e7481c-24dc70c812b9cbf7285-8000.html
rem http://forum.starling-framework.org/topic/wrong-wmode-with-adobeair-32-desktop
rem call createapk.bat



set e=%cd%
set swf=AIRStageWebViewExperiment.ApplicationSprite.swf
set ipa=air.ipa
set xml=air-descriptor.xml

echo building ipa...
echo once installed either do a search or double tap on ipad menu to open!

:: ipad 6 FPS
call "X:\jsc.internal.svn\keystore\carlo-lenovo\air13.com.abstractatech-package.and.sign.ios.bat" %e% %swf% %ipa% %xml%
:: rem 24 fps
rem call "X:\jsc.internal.svn\keystore\carlo-lenovo\air13.com.abstractatech-package.and.sign.ios.adhoc.bat" %e% %swf% %ipa% %xml%




echo off
rem http://help.adobe.com/en_US/flex/mobileapps/WS19f279b149e7481c-24dc70c812b9cbf7285-8000.html
rem http://forum.starling-framework.org/topic/wrong-wmode-with-adobeair-32-desktop
rem call createapk.bat
rem http://blogs.adobe.com/airodynamics/2012/07/17/installinguninstalling-ios-air-applications-on-ios-devices-using-adt/
rem https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140301

echo installing
call C:\util\air13_sdk_win\bin\adt -installApp -platform ios -package air.ipa

rem echo launching - not supported for ios yet?
rem http://help.adobe.com/en_US/air/build/WS901d38e593cd1bac1e63e3d128fc240122-7ff8.html
rem set appid=jsc-solutions.hellojsc
rem call C:\util\air13_sdk_win\bin\adt -launchApp  -platform ios %appid%

