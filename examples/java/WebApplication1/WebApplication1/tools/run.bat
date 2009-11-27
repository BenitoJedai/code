@echo off 
pushd ..

call "C:\Program Files\Common Files\Microsoft Shared\DevServer\9.0\WebDev.WebServer.exe" /port:8081 /path:"%CD%\bin\staging" /vpath:"/"

popd