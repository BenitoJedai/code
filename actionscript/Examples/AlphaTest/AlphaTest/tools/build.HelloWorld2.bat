@echo off
pushd ..\bin\debug\web


call C:\util\flex2\bin\mxmlc.exe -incremental=true -output=HelloWorld2.swf -strict -sp=. AlphaTest/ActionScript/HelloWorld2.as

popd