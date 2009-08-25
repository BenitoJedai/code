@echo off
setlocal

::build is part of msbuild now
::call build

call find.java java.exe
set TargetPath=%ReturnValue%

if '%TargetPath%' == '' (
    echo java not found
    goto :eof
)


pushd ..\bin\Release\web

:: import primary applet settings
call setup.settings.cmd


echo + run [%ProjectName% @ %TargetPath%]

pushd bin


copy %PackageName% C:\util\robocode\robots
::echo %TargetPath% -cp "%PATH%;%PackageName%" %CompilandFullName% %*

popd
popd
endlocal
