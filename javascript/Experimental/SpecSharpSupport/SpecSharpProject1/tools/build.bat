@echo off

pushd ..\bin\debug\

call "c:\util\jsc\bin\jsc.exe" "SpecSharpProject1.dll" -js
popd
