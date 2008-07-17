@echo off
setlocal

call find.java jar.exe
set TargetPath=%ReturnValue%

if '%TargetPath%' == '' (
    echo java not found
    goto :eof
)


pushd ..\bin\Debug\web

call setup.settings.cmd

echo + creating jar [%PackageName%]

%TargetPath% cvM -C release . > bin\%PackageName%

popd
endlocal
