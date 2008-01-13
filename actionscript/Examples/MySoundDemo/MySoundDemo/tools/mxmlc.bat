@echo off
pushd ..\bin\debug\web


call C:\util\flex2\bin\mxmlc.exe -incremental=true -output=MySoundDemo.swf -strict -sp=. MySoundDemo/ActionScript/MySoundDemo.as

popd