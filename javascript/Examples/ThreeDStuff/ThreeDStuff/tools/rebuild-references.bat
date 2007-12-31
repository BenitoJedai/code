@echo off
setlocal
:: go back to the svn root
pushd ..\..\..\..\..\

:: this bat file rebuilds all the referenced sub projects

set msbuild=%SystemRoot%\Microsoft.NET\Framework\v3.5\MSBuild.exe
set flags=/nologo /verbosity:q 

:: jsc is expected here
set target=C:\util\jsc\bin


:: rebuild compiler
call :build compiler\ScriptCoreLibA\ScriptCoreLibA.sln

call :build compiler\jsc\jsc.sln

call :build core\ScriptCoreLib\ScriptCoreLib.sln
call :build core\ScriptCoreLib.Query\ScriptCoreLib.Query.sln
call :build javascript\Controls\LayeredControl\ScriptCoreLib.Controls.LayeredControl.sln
call :build javascript\Controls\ScriptCoreLib.Controls.NatureBoy\ScriptCoreLib.Controls.NatureBoy.sln




goto :skip

:: java
::call :build templates\AppletTemplate\DemoApplet.sln


call :build javascript\Games\LightsOut\LightsOut.sln
call :build javascript\Examples\DragStan\drag.sln

:skip

popd
endlocal
goto :eof

:build
echo - %1
call %msbuild% %flags% %1
goto :eof

:buildfast
echo - %1
call %msbuild% %flags% /p:PostBuildEvent="" %1
goto :eof
