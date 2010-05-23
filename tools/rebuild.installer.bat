@echo off
setlocal

set msbuild=%SystemRoot%\Microsoft.NET\Framework\v3.5\MSBuild.exe
set msbuild40=%SystemRoot%\Microsoft.NET\Framework\v4.0.30128\MSBuild.exe

set flags=/nologo /verbosity:q

set target=C:\util\jsc\bin


set SplashAssembly=c:\util\jsc\bin\jsc.splash.exe

:: we need to pre build that assembly in "Assets" configuration
call c:\util\jsc\bin\jsc.meta.exe ConfigurationPrecompile
call c:\util\jsc\bin\jsc.meta.exe RewriteToInstaller /Splash:%SplashAssembly% /AttachDebugger:false

endlocal
goto :eof

:build
echo - %1
call %msbuild% %flags% %1
goto :eof

:build40
echo - %1
call %msbuild40% %flags% %1
goto :eof