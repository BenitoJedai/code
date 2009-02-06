@echo off
setlocal

set msbuild=%SystemRoot%\Microsoft.NET\Framework\v3.5\MSBuild.exe
set flags=/nologo /verbosity:q

set target=C:\util\jsc\bin

:: C:\util\jsc\bin is expected to exist as compiler and framework assemblies will be placed there

:: rebuild compiler
call :build compiler\ScriptCoreLibA\ScriptCoreLibA.sln
call :build compiler\jsc\jsc.sln

:: java
call :build core\ScriptCoreLibJava\ScriptCoreLibJava.sln
call :build core\ScriptCoreLibJava.jni\ScriptCoreLibJava.jni.sln

:: rebuild framework

call :build core\jsc.server\jsc.server.sln
call :build core\ScriptCoreLib\ScriptCoreLib.sln
call :build core\ScriptCoreLib.Net\ScriptCoreLib.Net.sln
call :build core\ScriptCoreLib.Query\ScriptCoreLib.Query.sln
call :build core\ScriptCoreLib.XLinq\ScriptCoreLib.XLinq.sln
call :build core\ScriptCoreLib.Cards\ScriptCoreLib.Cards.sln
call :build core\ScriptCoreLib.Net\ScriptCoreLib.Net.sln
call :build core\ScriptCoreLib.Nonoba\ScriptCoreLib.Nonoba.sln
call :build core\ScriptCoreLib.Mochi\ScriptCoreLib.Mochi.sln
call :build core\ScriptCoreLib.GoogleMaps\ScriptCoreLib.GoogleMaps.sln
call :build core\ScriptCoreLib.Drawing\ScriptCoreLib.Drawing.sln
call :build core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms.sln
call :build core\ScriptCoreLib.Archive\ScriptCoreLib.Archive.sln
call :build core\ScriptCoreLib.Avalon\ScriptCoreLib.Avalon.sln
call :build core\ScriptCoreLib.Avalon.Cursors\ScriptCoreLib.Avalon.Cursors.sln
call :build core\ScriptCoreLib.Avalon.TextButton\ScriptCoreLib.Avalon.TextButton.sln
call :build core\ScriptCoreLib.Avalon.TextSuggestions\ScriptCoreLib.Avalon.TextSuggestions.sln
call :build core\ScriptCoreLib.Avalon.TiledImageButton\ScriptCoreLib.Avalon.TiledImageButton.sln
call :build core\ScriptCoreLib.Avalon.Carousel\ScriptCoreLib.Avalon.Carousel.sln
call :build core\ScriptCoreLib.RayCaster\ScriptCoreLib.RayCaster.sln
call :build core\ScriptCoreLib.Maze\ScriptCoreLib.Maze.sln


endlocal
goto :eof

:build
echo - %1
call %msbuild% %flags% %1
goto :eof