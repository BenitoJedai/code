:mxmlc
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
@call :mxmlc %1/ActionScript %1

goto :eof

:jsc
pushd ..\bin\%ConfigurationName%

dir
start /WAIT cmd /K c:\util\jsc\bin\jsc.meta.exe %1.dll -as


popd
goto :eof

:mxmlc
@echo off
pushd ..\bin\%ConfigurationName%\web



call :build %1 %2


popd
goto :eof

:build
echo - %2
:: http://www.adobe.com/products/flex/sdk/
:: -compiler.verbose-stacktraces 

call x:\util\flex4\bin\mxmlc +configname=airmobile  -use-network=false -static-link-runtime-shared-libraries=true -debug -swf-version=13 -target-player=11.1.0 -keep-as3-metadata -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
goto :eof