@echo off
setlocal



call find.java java.exe
set TargetPath=%ReturnValue%

if '%TargetPath%' == '' (
    echo java not found
    goto :eof
)


pushd ..\bin\release\web

:: import primary applet settings
call setup.settings.cmd


echo + run [%ProjectName%]

pushd bin

%TargetPath% -cp "%PATH%;%PackageName%" %CompilandFullName% %*

popd
popd
endlocal
