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


echo + run java [%ProjectName%]

pushd bin

%TargetPath% -cp "%PATH%;%PackageName%" %CompilandFullName% %*

popd
popd

echo + run .net
pushd ..\bin\Release\
call UnsignedByteSupport.exe
popd

endlocal
