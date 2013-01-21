echo off
rem http://help.adobe.com/en_US/flex/mobileapps/WS19f279b149e7481c-24dc70c812b9cbf7285-8000.html
rem http://forum.starling-framework.org/topic/wrong-wmode-with-adobeair-32-desktop
C:\util\flex_sdk_4.6\bin\adt -package -target apk -storetype pkcs12 -keystore newcert.p12 -keypass password foo.apk foo-app.xml foo.swf
