@echo off
pushd
echo + compile native
setlocal

rem 2011 :)
set _toolkit=C:\Program Files\Microsoft Visual Studio 10.0\VC
set _init=%_toolkit%\..\Common7\Tools\vsvars32.bat


echo - setting vars
call "%_init%" 
rem >nul

set _targetpath=bin
set _sourcefiles=*.c


set _libname=lib_jnistb10

set _java=C:\Program Files\Java\jdk1.6.0_24

set _args=/I "%_java%\include"
set _args=%_args% /I "%_java%\include\win32"
set _args=%_args% /TC /Zm200 /D "WIN32" 
rem set _args=%_args% /LD /nologo /EHsc  %_sourcefiles% 
set _args=%_args% /Febin\%_libname%.dll 

rem /link /ASSEMBLYMODULE:bin/foo.netmodule

type dispatch.c > __program.c
type dispatch_x86.c >> __program.c

set _args=%_args% /LD /nologo /EHsc __program.c
set _args=%_args% /Fobin\%_libname%.obj




rem echo cl.exe  %_args%
set _command="%_toolkit%\bin\cl.exe" %_args%

rem cd
rem dir *.cpp

rem echo %_command%
call %_command%
rem > compile.log

erase __program.c

pushd bin

rem call "C:\Program Files\Microsoft Visual Studio 10.0\VC\bin\link.exe" /DLL /LTCG /CLRIMAGETYPE:IJW     /OUT:bar.dll *.obj
  
popd
  
endlocal
popd
 
 echo done