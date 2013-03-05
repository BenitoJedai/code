echo off
rem http://help.adobe.com/en_US/flex/mobileapps/WS19f279b149e7481c-24dc70c812b9cbf7285-8000.html
rem http://forum.starling-framework.org/topic/wrong-wmode-with-adobeair-32-desktop
rem call createapk.bat


set e=%cd%
set swf=FlashHeatZeeker.UnitPedTouch.ApplicationSprite.swf
set apk=air.com.abstractatech.gamification.fhzup.apk
set xml=air.com.abstractatech.gamification.fhzup.xml

"X:\jsc.internal.svn\keystore\asus\air.com.abstractatech-package.and.sign.bat" %e% %swf% %apk% %xml%
