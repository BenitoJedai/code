@echo off
pushd ..\bin\debug

call c:\util\jsc\bin\jsc.exe %1.dll  -as


popd