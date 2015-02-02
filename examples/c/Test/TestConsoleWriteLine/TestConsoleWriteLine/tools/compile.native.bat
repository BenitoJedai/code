

@echo off

set ConfigurationName=%2

pushd ..\bin\%ConfigurationName%\web\
echo + compile native [%1] %ConfigurationName%
setlocal

set _libname=%1

echo - setting vars 
echo call "C:\Program Files\Microsoft SDKs\Windows\v7.1\Bin\SetEnv.Cmd"

:: http://stackoverflow.com/questions/7865432/command-line-compile-using-cl-exe
call "C:\Program Files\Microsoft SDKs\Windows\v7.1\Bin\SetEnv.Cmd" /x86

set _targetpath=bin\Debug\web
set _sourcefiles=*.c


::set _args=/I "%_java%\include"
::set _args=%_args% /I "%_java%\include\win32"

:: Specifies a C source file.
set _args=%_args% /TC   %_sourcefiles%  

::set _args=%_args% /TC /Zm200 
::set _args=%_args% /nologo /EHsc  %_sourcefiles% 

::  /EHsc command-line option instructs the compiler to enable C++ exception handling. 
::set _args=%_args% /EHsc  %_sourcefiles% 

:: https://msdn.microsoft.com/en-us/library/19z1t1wy.aspx
::set _args=%_args% /LDd /MDd /Fe%_libname%
:: set _args=%_args% /Zi /LDd /MD /Fe%_libname% /link /MACHINE:X86
::set _args=%_args% /Zi /LD /MD /Fe%_libname% 
set _args=%_args% /Zi  /LD  /Fe%_libname% 

:: fatal error LNK1112: module machine type 'x64' conflicts with target machine type 'X86'

rem echo cl.exe  %_args%
set _command="cl.exe" %_args%

rem cd
rem dir *.cpp

echo %cd%
echo call %_command%
call %_command%
echo done
rem > compile.log

endlocal
popd
