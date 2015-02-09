

@echo off

set ConfigurationName=%2

pushd ..\bin\%ConfigurationName%\web\
echo + compile native [%1] %ConfigurationName%
setlocal

set _libname=%1

echo - setting vars 
echo call "C:\Program Files\Microsoft SDKs\Windows\v7.1\Bin\SetEnv.Cmd"

:: http://stackoverflow.com/questions/7865432/command-line-compile-using-cl-exe
:: call "C:\Program Files\Microsoft SDKs\Windows\v7.1\Bin\SetEnv.Cmd" /x86
call "X:\Program Files (x86)\Microsoft Visual Studio 14.0\VC\vcvarsall.bat" x86

set _targetpath=bin\Debug\web
set _sourcefiles=*.c


::set _args=/I "%_java%\include"
::set _args=%_args% /I "%_java%\include\win32"

:: Specifies a C source file.
set _args=%_args%  /TC   %_sourcefiles%  

::set _args=%_args% /TC /Zm200 
::set _args=%_args% /nologo /EHsc  %_sourcefiles% 

::  /EHsc command-line option instructs the compiler to enable C++ exception handling. 
::set _args=%_args% /EHsc  %_sourcefiles% 

:: https://msdn.microsoft.com/en-us/library/19z1t1wy.aspx
::set _args=%_args% /LDd /MDd /Fe%_libname%
:: set _args=%_args% /Zi /LDd /MD /Fe%_libname% /link /MACHINE:X86
::set _args=%_args% /Zi /LD /MD /Fe%_libname% 
set _args=%_args% /Zi  /Fe%_libname% 


:: http://www.codeproject.com/Articles/4166/Using-MC-exe-message-resources-and-the-NT-event-lo
:: http://www.geoffchappell.com/studies/msvc/cl/cl/infiles.htm
:: fatal error LNK1112: module machine type 'x64' conflicts with target machine type 'X86'

rem echo cl.exe  %_args%
set _command="cl.exe" %_args%

:: https://msdn.microsoft.com/en-us/library/windows/desktop/aa381055(v=vs.85).aspx
rem call rc.exe -DWIN32 TestSwitchToCLR.rc

rem cd
rem dir *.cpp

echo %cd%
echo call %_command%
call %_command%

rem call link.exe /verbose /INCREMENTAL:NO /out:foo.exe TestSwitchToCLR.exe.obj TestSwitchToCLR.res
rem call link.exe /DLL /DEBUG  /out:NBSSC.dll NBSSC.dll.obj  /DEF:NBSSC.dll.def
rem call lib.exe /out:CLRLibrary.lib CLRLibrary.dll.obj

echo done
rem > compile.log

echo can we exit?
echo %ERRORLEVEL%

endlocal
popd



pause
