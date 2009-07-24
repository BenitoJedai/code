@echo off
setlocal

echo - java version
call run.jar.bat

pushd ..\bin\Release\web\

:: import primary applet settings
call setup.settings.cmd

pushd ..
echo - .net version
call %CompilandNamespace0%.exe
popd
popd
endlocal
