@echo off
pushd
echo + compile native
setlocal



rem 2003 toolkit
rem set _toolkit=X:\util\vc2003.toolkit
rem set _init=%_toolkit%\vcvars32.bat

rem 2003
set _toolkit=X:\util\dotnet2003\Vc7
set _init=%_toolkit%\..\Common7\Tools\vsvars32.bat

rem 2005
rem set _toolkit=X:\util\vs2005re\VC
rem set _init=%_toolkit%\vcvarsall.bat

echo - setting vars
call %_init% >nul

set _targetpath=bin
set _sourcefiles=*.c


set _libname=lib_jnistb10.dll

set _java=x:\util\java5

set _args=/I "%_java%\include"
set _args=%_args% /I "%_java%\include\win32"
set _args=%_args% /TC /Zm200 /D "WIN32"
set _args=%_args% /LD /nologo /EHsc  %_sourcefiles% 
set _args=%_args% /Febin\%_libname%




rem echo cl.exe  %_args%
set _command=%_toolkit%\bin\cl.exe %_args%

rem cd
rem dir *.cpp

rem echo %_command%
call %_command%
rem > compile.log

endlocal
popd
