
@echo off
set ConfigurationName=Release

pushd ..\bin\%ConfigurationName%\web\
echo + compile native [%1] %ConfigurationName%
setlocal

set _libname=%1

rem 2003 toolkit
rem set _toolkit=X:\util\vc2003.toolkit
rem set _init=%_toolkit%\vcvars32.bat

rem 2003
::set _toolkit=X:\util\dotnet2003\Vc7

::set _init=%_toolkit%\..\Common7\Tools\vsvars32.bat
set _toolkit=C:\Program Files (x86)\Microsoft Visual Studio 11.0\VC
set _init=C:\Program Files\Microsoft SDKs\Windows\v7.1\Bin\SetEnv.cmd

if exist "%_init%" goto :found

rem 2005

set _toolkit=D:\Program Files\Microsoft Visual Studio 9.0\VC
set _init=%_toolkit%\vcvarsall.bat

if exist "%_init%" goto :found

echo vcvarsall.bat not found
goto :eof

:found

echo - setting vars
rem echo call C:\Windows\System32\cmd.exe /E:ON /V:ON /T:0E /K "C:\Program Files\Microsoft SDKs\Windows\v7.1\Bin\SetEnv.cmd"
rem echo call C:\Windows\System32\cmd.exe /E:ON /V:ON /T:0E /K "C:\Program Files\Microsoft SDKs\Windows\v7.1\Bin\SetEnv.cmd"
pushd C:\Program Files\Microsoft SDKs\Windows\v7.1\Bin\
c:

call "C:\Program Files\Microsoft SDKs\Windows\v7.1\Bin\SetEnv.cmd"
popd
x:

set _targetpath=bin\Debug\web
set _sourcefiles=*.c




::set _java=x:\util\java5

::set _args=/I "%_java%\include"
::set _args=%_args% /I "%_java%\include\win32"
set _args=%_args% /TC /Zm200 
set _args=%_args% /nologo /EHsc  %_sourcefiles% 
set _args=%_args% /Fe%_libname%




rem echo cl.exe  %_args%
set _command="%_toolkit%\bin\cl.exe" %_args%

rem cd
rem dir *.cpp

echo call %_command%
cmd /C %_command%

rem > compile.log

echo done...
rem endlocal
rem popd
