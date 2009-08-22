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

call :fork @%TargetPath% -cp "%PATH%;%PackageName%" %CompilandFullName% %*


popd
popd
endlocal
goto :eof
:sleep

:: http://www.makeuseof.com/tag/10-windows-command-line-tips-tricks-you-should-definitely-check-out/
ping -n %1 127.0.0.1 > NUL 2>&1

goto :eof
:fork
setlocal
set ForkFile=%CompilandFullName%.fork.bat
echo %* > %ForkFile%
echo forking %ForkFile%...
start %ForkFile%
call :sleep 2
del  %ForkFile%
if '%ForkFile%' == '' (
    echo fork file was not deleted
)
echo forking %ForkFile%... done
endlocal
goto :eof