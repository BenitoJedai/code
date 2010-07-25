@echo off
setlocal

set msbuild=%SystemRoot%\Microsoft.NET\Framework\v3.5\MSBuild.exe
set msbuild40=%SystemRoot%\Microsoft.NET\Framework\v4.0.30128\MSBuild.exe

set flags=/nologo /verbosity:q

set target=C:\util\jsc\bin


set SplashAssembly=c:\util\jsc\bin\jsc.splash.exe

:: we need to pre build that assembly in "Assets" configuration
::ERASE /s /Q c:\util\jsc\cache 
::ERASE /s /Q %LOCALAPPDATA%\jsc\cache 

::call c:\util\jsc\bin\jsc.meta.exe ConfigurationPrecompile

set SDKConfiguration=jsc.SDKConfiguration.xml

:: the configuration is machine local and should be reconfigured at install
ren %target%\%SDKConfiguration% %SDKConfiguration%.transient

call c:\util\jsc\bin\jsc.meta.exe RewriteToInstaller /Splash:%SplashAssembly% /AttachDebugger:false /OutputStrongNameKeyPair:"W:\jsc_key.snk" /AttachDebugger:true

ren %target%\%SDKConfiguration%.transient %SDKConfiguration%

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