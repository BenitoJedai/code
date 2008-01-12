@echo off
pushd ..\bin\debug\web


call C:\util\flex2\bin\mxmlc.exe -incremental=true -output=bin\HelloWorld3.swf -strict -sp=. AlphaTest/ActionScript/HelloWorld3.as

popd