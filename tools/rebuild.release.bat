@echo off
setlocal

set msbuild40=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe

set flags=/nologo
rem echo %flags%
set target=C:\util\jsc\bin


set SplashAssembly=c:\util\jsc\bin\jsc.splash.exe

:: we need to pre build that assembly in "Assets" configuration
ERASE /s /Q c:\util\jsc\cache 
ERASE /s /Q %LOCALAPPDATA%\jsc\cache 

::call c:\util\jsc\bin\jsc.meta.exe ConfigurationPrecompile

setlocal
echo - installer ..

call rebuild.installer.bat
endlocal


pushd ..

echo - increment installer version
call %target%\jsc.meta.exe ConfigurationIncrementApplicationVersion /ProjectFileName:compiler\jsc.configuration\jsc.configuration.csproj"

del /Q /S c:\util\jsc\publish
echo - make the installer
call :build40 /verbosity:q /target:publish "compiler\jsc.configuration\jsc.configuration.sln"

echo - make the website
ERASE /s /Q examples\java\PromotionWebApplication\PromotionWebApplication1\bin\Release
call :build40 /verbosity:q /p:Configuration=Release "examples\java\PromotionWebApplication\PromotionWebApplication.sln"

popd

endlocal
goto :eof



:build40

call %msbuild40% %flags% %*
goto :eof