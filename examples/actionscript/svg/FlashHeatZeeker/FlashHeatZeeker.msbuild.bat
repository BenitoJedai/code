@echo off
setlocal
title rebuild

::set msbuild=%SystemRoot%\Microsoft.NET\Framework\v3.5\MSBuild.exe
set msbuild=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe
set msbuild40=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe

set flags=/nologo /t:rebuild /p:Configuration=Release
call :build FlashHeatZeeker.sln

rem set flags=/nologo /t:rebuild
rem call :build com.abstractatech.web.sln


endlocal
goto :eof



:build
echo - %1
call %msbuild% %flags% %1

set __ERRORLEVEL=%ERRORLEVEL%
if %ERRORLEVEL% == 0 goto :eof
echo command %msbuild% %flags% %1
echo failed with %__ERRORLEVEL%
pause
exit /B %__ERRORLEVEL%
goto :eof

