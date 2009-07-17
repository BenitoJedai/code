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
::@call :mxmlc %1/ActionScript %1

@call compile.native %2 %3
@call :upx %2
goto :eof

:jsc
pushd ..\bin\%ConfigurationName%

call c:\util\jsc\bin\jsc.exe %TargetFileName% -c


popd
goto :eof

:upx
@echo off
pushd ..\bin\%ConfigurationName%\web



call C:\util\upx303w\upx.exe -9 -o %1.upx.exe %1


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
:: call C:\util\flex2\bin\mxmlc.exe -keep-as3-metadata -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
::call C:\util\flex\bin\mxmlc.exe -debug -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
call C:\util\flex\bin\mxmlc.exe -optimize=true -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
::call C:\util\flex\bin\mxmlc.exe -debug -keep-as3-metadata -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
goto :eof