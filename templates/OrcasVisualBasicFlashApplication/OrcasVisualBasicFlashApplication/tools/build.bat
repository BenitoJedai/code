@echo off

setlocal
pushd ..\bin\release


:: Dll name
call c:\util\jsc\bin\jsc.exe %1.dll  -as

pushd web

:: Namespace name, type name
call :mxmlc %1/ActionScript %1

popd
popd
endlocal

goto :eof

:mxmlc 
echo - %2
:: http://www.adobe.com/products/flex/sdk/
call C:\util\flex2\bin\mxmlc.exe -keep-as3-metadata -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
goto :eof