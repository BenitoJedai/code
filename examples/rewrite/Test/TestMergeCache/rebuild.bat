@echo off
setlocal


set msbuild=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe

set flags=/nologo

call :build TestMergeCache.sln

endlocal
goto :eof



:build
echo - %1
call %msbuild% %flags% %1

set __ERRORLEVEL=%ERRORLEVEL%
if %ERRORLEVEL% == 0 goto :eof
pause
exit /B %__ERRORLEVEL%
goto :eof

