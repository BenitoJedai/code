@echo off
set TargetFileName=%2
set ConfigurationName=%3

if %ConfigurationName%==Debug (
  echo Debug mode will not perform post build!
  goto :eof
)

:: Dll name
@call :jsc %1

if '%ERRORLEVEL%' == '-1' (
    echo jsc failed.
    goto :eof
)
:: Namespace name, type name
::@call :mxmlc WebApplication/Client/ActionScript AvalonFlash
::@call compile.java.bat WebApplication/Client/Java ApplicationApplet

goto :eof

:jsc
pushd ..\bin\%ConfigurationName%

::call c:\util\jsc\bin\jsc.exe %TargetFileName%  -as -js
::call c:\util\jsc\bin\jsc.exe %TargetFileName%  -as -js -php -java
call c:\util\jsc\bin\jsc.exe %TargetFileName%  -php


popd
goto :eof

:mxmlc
@echo off
pushd ..\bin\%ConfigurationName%\web

echo - %2
:: http://www.adobe.com/products/flex/sdk/
:: -compiler.verbose-stacktraces 
:: call C:\util\flex2\bin\mxmlc.exe -keep-as3-metadata -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
call C:\util\flex\bin\mxmlc.exe -debug -keep-as3-metadata -incremental=true -output=%2.swf -strict -sp=. %1/%2.as

popd
goto :eof
