@echo off
pushd ..\bin\debug\web


call C:\util\flex2\bin\mxmlc.exe -incremental=true -output=bin\OrcasFlashApplication.swf -strict -sp=. OrcasFlashApplication/ActionScript/OrcasFlashApplication.as

popd