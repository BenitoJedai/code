echo off
rem http://help.adobe.com/en_US/flex/mobileapps/WS19f279b149e7481c-24dc70c812b9cbf7285-8000.html
rem http://forum.starling-framework.org/topic/wrong-wmode-with-adobeair-32-desktop
rem call createapk.bat


set e=%cd%
set swf=FlashTowerDefenseApp.ApplicationSprite.swf
set apk=air.apk
set xml=air-descriptor.xml

echo build

call "X:\jsc.internal.git\keystore\asus\air17.com.abstractatech-package.and.sign.bat" %e% %swf% %apk% %xml%







echo off
rem http://help.adobe.com/en_US/flex/mobileapps/WS19f279b149e7481c-24dc70c812b9cbf7285-8000.html
rem http://forum.starling-framework.org/topic/wrong-wmode-with-adobeair-32-desktop
rem call createapk.bat

echo install
"x:\util\android-sdk-windows\platform-tools\adb.exe"  install -r air.apk
