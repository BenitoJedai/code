@echo off
setlocal

set msbuild=%SystemRoot%\Microsoft.NET\Framework\v3.5\MSBuild.exe
set msbuild40=%SystemRoot%\Microsoft.NET\Framework\v4.0.30128\MSBuild.exe

set flags=/nologo /verbosity:q

set target=C:\util\jsc\bin


set SplashType=PromotionWebApplication.AvalonLogo.Desktop.AvalonLogoForDesktop
set SplashMethod=ShowDialogSplash
set SplashAssembly=W:\jsc.svn\examples\java\PromotionWebApplication\PromotionWebApplication.AvalonLogo\bin\Assets\PromotionWebApplication.AvalonLogo.dll

:: we need to pre build that assembly in "Assets" configuration
call c:\util\jsc\bin\jsc.meta.exe RewriteToInstaller /Splash.SplashType:%SplashType% /Splash.SplashMethod:%SplashMethod% /Splash.SplashAssembly:%SplashAssembly%

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