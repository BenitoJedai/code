@echo off
pushd ..\bin\release\web\
echo killing all java in pure hope to terminate the servlet...
taskkill /IM java.exe /F
taskkill /FI "WINDOWTITLE eq volatile_dev_appserver*" /F
start "volatile_dev_appserver" /MIN C:\util\appengine-java-sdk-1.2.5\bin\dev_appserver.cmd www
start "web" explorer "http://localhost:8080"
popd