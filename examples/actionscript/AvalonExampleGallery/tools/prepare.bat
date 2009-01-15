@echo off
setlocal
pushd ..\..\..\..


set msbuild=%SystemRoot%\Microsoft.NET\Framework\v3.5\MSBuild.exe
set flags=/nologo /verbosity:q

:: rebuild framework

call :buildsln core jsc.server

call :buildsln core ScriptCoreLib
call :buildsln core ScriptCoreLib.Query
call :buildsln core ScriptCoreLib.Net
call :buildsln core ScriptCoreLib.Avalon
call :buildsln core ScriptCoreLib.Avalon.Carousel
call :buildsln core ScriptCoreLib.Avalon.TextButton
call :buildsln core ScriptCoreLib.Avalon.TiledImageButton


call :buildsln examples\actionscript CarouselExample2
call :buildsln examples\actionscript DraggableClipRectangle
call :buildsln examples\actionscript DynamicCursor
call :buildsln examples\actionscript FlashAvalonQueryExample
call :buildsln examples\actionscript FlashMouseMaze
call :buildsln examples\actionscript NavigationButtons
call :buildsln examples\actionscript NumericTransmitter
call :buildsln examples\actionscript System_IO_StringReader
call :buildsln examples\actionscript TextSuggestions
call :buildsln examples\actionscript TextSuggestions2

call :buildsln examples\javascript BrowserAvalonExample
call :buildsln examples\javascript System_Windows_Input_MouseEventArgs


popd
endlocal
goto :eof

:buildsln
call :build %1\%2\%2.sln
goto :eof

:build
echo - %1
call %msbuild% %flags% %1
goto :eof