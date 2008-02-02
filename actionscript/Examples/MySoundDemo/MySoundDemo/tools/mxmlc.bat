@echo off
setlocal
pushd ..\bin\debug\web


call :build MySoundDemo/ActionScript MySoundDemo
call :build MySoundDemo/ActionScript MySoundDemo2


popd
endlocal
goto :eof

:build
echo - %2
call C:\util\flex2\bin\mxmlc.exe -keep-as3-metadata -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
goto :eof