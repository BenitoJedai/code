@echo off
pushd ..\bin\debug\web


call C:\util\flex2\bin\mxmlc.exe -incremental=true -output=MyWebCamera.swf -strict -sp=. MyWebCamera/ActionScript/MyWebCamera.as

popd