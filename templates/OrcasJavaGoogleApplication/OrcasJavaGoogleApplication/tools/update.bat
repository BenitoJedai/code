@echo off
pushd ..\bin\release\web\
start C:\util\appengine-java-sdk-1.2.6\bin\appcfg.cmd update www
popd