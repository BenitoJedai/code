echo off
rem http://help.adobe.com/en_US/flex/mobileapps/WS19f279b149e7481c-24dc70c812b9cbf7285-8000.html
rem http://forum.starling-framework.org/topic/wrong-wmode-with-adobeair-32-desktop
rem call createapk.bat


set e=%cd%
set swf=FlashHeatZeeker.UnitHindTouch.ApplicationSprite.swf
set apk=air.apk
set xml=air.com.abstractatech.gamification.fhzuh.xml

:: https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201412/20141207
echo build
call "X:\jsc.internal.git\keystore\asus\air16.com.abstractatech-package.and.sign.bat" %e% %swf% %apk% %xml%







echo off
rem http://help.adobe.com/en_US/flex/mobileapps/WS19f279b149e7481c-24dc70c812b9cbf7285-8000.html
rem http://forum.starling-framework.org/topic/wrong-wmode-with-adobeair-32-desktop
rem call createapk.bat

echo install
"C:\util\android-sdk-windows\platform-tools\adb.exe"  install -r air.apk
