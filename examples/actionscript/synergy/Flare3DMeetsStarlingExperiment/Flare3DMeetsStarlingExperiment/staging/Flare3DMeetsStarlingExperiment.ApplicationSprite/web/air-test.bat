echo off
rem http://help.adobe.com/en_US/flex/mobileapps/WS19f279b149e7481c-24dc70c812b9cbf7285-8000.html
rem http://forum.starling-framework.org/topic/wrong-wmode-with-adobeair-32-desktop
rem call build.bat


rem C:\util\flex_sdk_4.6\bin\adl  foo-app.xml  -profile mobileDevice
C:\util\air16_sdk_sa_win\bin\adl  air-descriptor.xml  -profile mobileDevice -screensize 680x762:680x800
