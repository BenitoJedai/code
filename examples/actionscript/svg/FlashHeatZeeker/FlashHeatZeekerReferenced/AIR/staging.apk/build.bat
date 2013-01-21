echo off
rem http://help.adobe.com/en_US/flex/mobileapps/WS19f279b149e7481c-24dc70c812b9cbf7285-8000.html
rem mxmlc +configname=airmobile MyMobileApp.mxml

set sp=X:\jsc.svn\examples\actionscript\svg\FlashHeatZeeker\FlashHeatZeekerReferenced\bin\Release\staging\FlashHeatZeekerReferenced.ApplicationSprite\web

set lib="%sp%\assets\starling\starling.AssetsLibrary.swc"

C:\util\flex_sdk_4.6\bin\mxmlc +configname=airmobile  --target-player=11.1.0 -swf-version=13  -static-link-runtime-shared-libraries=true   -output=foo.swf   -sp=%sp% -library-path+=%lib%  "%sp%/FlashHeatZeekerReferenced\ApplicationSprite.as"

rem http://stackoverflow.com/questions/14438524/add-adobe-captive-runtime-to-android-eclipse-project
