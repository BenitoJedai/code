@echo off
setlocal

::call build

call find.java appletviewer.exe
set TargetPath=%ReturnValue%

if '%TargetPath%' == '' (
    echo java not found
    goto :eof
)

pushd ..\bin\Debug\web

:: import primary applet settings
call setup.settings.cmd

if not exist %AppletWebPage% (
    echo There is no webpage %AppletWebPage%  
    goto :eof
)


call %TargetPath% "%AppletWebPage%" > test.log


popd
endlocal
