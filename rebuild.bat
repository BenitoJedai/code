@echo off
setlocal

set msbuild=%SystemRoot%\Microsoft.NET\Framework\v3.5.20404\MSBuild.exe
set flags=/nologo /verbosity:q

set target=C:\util\jsc\bin

:: C:\util\jsc\bin is expected to exist

:: rebuild compiler
call :build compiler\jsc\jsc.sln

:: rebuild framework
call :build core\ScriptCoreLib\ScriptCoreLib.sln
call :build core\ScriptCoreLib.Query\ScriptCoreLib.Query.sln
call :build core\ScriptCoreLib.Cards\ScriptCoreLib.Cards.sln
call :build core\ScriptCoreLib.Net\ScriptCoreLib.Net.sln
call :build core\ScriptCoreLib.Drawing\ScriptCoreLib.Drawing.sln
call :build core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms.sln


:: rebuild templates
call :build templates\OrcasScriptApplication\OrcasScriptApplication.sln



:: rebuild examples
call :build javascript\Games\SpaceInvaders\SpaceInvaders.sln

call :build javascript\Examples\ButterFly\ButterFly.sln
call :build javascript\Examples\CardGames\CardGames.sln
call :build javascript\Examples\TextRotator\TextRotator.sln
call :build javascript\Examples\GoogleGears\GGearAlpha.sln
call :build javascript\Examples\GMapsClone\GMapsClone.sln
call :build javascript\Examples\FormsExample\FormsExample.sln
call :build javascript\Examples\ImageZoomer\ImageZoomer.sln
call :build javascript\Examples\HotPolygon\HotPolygon.sln
call :build javascript\Examples\SimpleRollover\SimpleRollover.sln
call :build javascript\Examples\MouseWheel\MouseWheel.sln
call :build javascript\Examples\HulaGirl\HulaGirl.sln

:: msbuild does not work with visual basic 9 at the moment...

:: rebuild visual basic templates
::call :build templates\OrcasVisualBasicScriptApplication\OrcasVisualBasicScriptApplication.sln

:: rebuild visual basic examples
::call :build javascript\Examples\FormsExample.VisualBasic\FormsExample.VisualBasic.sln


endlocal
goto :eof

:build
echo - %1
call %msbuild% %flags% %1
goto :eof