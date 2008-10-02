:mxmlc
@echo off



:: Dll name
@call :jsc %1

if '%ERRORLEVEL%' == '-1' (
    echo jsc failed.
    goto :eof
)
:: Namespace name, type name
@call :mxmlc %1/ActionScript Assets

goto :eof

:jsc
pushd ..\bin\debug

call c:\util\jsc\bin\jsc.exe %1.dll  -as


popd
goto :eof

:mxmlc
@echo off
pushd ..\bin\debug\web



call :build %1 %2


popd
goto :eof

:build
echo - %2
:: http://www.adobe.com/products/flex/sdk/
:: -compiler.verbose-stacktraces 
:: call C:\util\flex2\bin\mxmlc.exe -keep-as3-metadata -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
:: call C:\util\flex\bin\mxmlc.exe -keep-as3-metadata -incremental=true -output=%2.swf -strict -sp=. %1/%2.as

::call C:\util\flex\bin\compc.exe -source-path=web -include-classes=SomeGenericAssets.ActionScript.Assets -directory=true -debug=false -output=web

:: http://www.mikechambers.com/blog/2006/05/19/quick-example-using-compc-to-compile-swcs/
call C:\util\flex\bin\compc.exe -source-path=. -debug=false -o=%2.swc  -include-classes  SomeGenericAssets.ActionScript.%2

goto :eof