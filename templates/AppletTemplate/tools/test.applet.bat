@echo off
setlocal


call find.java appletviewer.exe
set TargetPath=%ReturnValue%

if '%TargetPath%' == '' (
    echo java not found
    goto :eof
)

pushd ..\bin\release\web

:: import primary applet settings
call setup.settings.cmd

if not exist %AppletWebPage% (
    echo There is no webpage %AppletWebPage%  
    goto :eof
)


call %TargetPath% "%AppletWebPage%"


popd
endlocal
