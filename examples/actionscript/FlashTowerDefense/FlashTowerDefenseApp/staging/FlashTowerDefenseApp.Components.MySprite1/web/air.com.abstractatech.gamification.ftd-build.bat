echo off
rem http://help.adobe.com/en_US/flex/mobileapps/WS19f279b149e7481c-24dc70c812b9cbf7285-8000.html
rem http://forum.starling-framework.org/topic/wrong-wmode-with-adobeair-32-desktop
rem call createapk.bat


set e=X:\jsc.svn\examples\actionscript\FlashTowerDefense\FlashTowerDefenseApp\bin\Debug\staging\FlashTowerDefenseApp.Components.MySprite1\web
set swf=FlashTowerDefenseApp.Components.MySprite1.swf
set apk=air.com.abstractatech.gamification.ftd.apk
set xml=air.com.abstractatech.gamification.ftd.xml

"X:\jsc.internal.svn\keystore\asus\air.com.abstractatech-package.and.sign.bat" %e% %swf% %apk% %xml%
