@echo off
setlocal
pushd ..\bin\debug\web



call :build %1 %2


popd
endlocal
goto :eof

:build
echo - %2
:: http://www.adobe.com/products/flex/sdk/
call C:\util\flex2\bin\mxmlc.exe -debug -keep-as3-metadata -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
goto :eof