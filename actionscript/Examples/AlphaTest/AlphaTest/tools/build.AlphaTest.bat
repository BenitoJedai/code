@echo off
pushd ..\bin\debug\web


call C:\util\flex2\bin\mxmlc.exe -incremental=true -output=bin\AlphaTest.swf -strict -sp=. AlphaTest/ActionScript/AlphaTest.as

popd