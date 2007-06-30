@echo off
setlocal

set msbuild=%SystemRoot%\Microsoft.NET\Framework\v3.5.20404\MSBuild.exe
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
call :build core\ScriptCoreLib.Query\ScriptCoreLib.Query.sln
call :build core\ScriptCoreLib.Cards\ScriptCoreLib.Cards.sln
call :build core\ScriptCoreLib.Net\ScriptCoreLib.Net.sln
call :build core\ScriptCoreLib.Drawing\ScriptCoreLib.Drawing.sln
call :build core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms.sln

:: rebuild controls
call :build javascript\Controls\TextEditor\ScriptCoreLib.Controls.TextEditor.sln
call :build javascript\Controls\LayeredControl\ScriptCoreLib.Controls.LayeredControl.sln


:: rebuild templates
call :build templates\OrcasScriptApplication\OrcasScriptApplication.sln
call :build templates\AppletTemplate\DemoApplet.sln



:: rebuild examples
call :build javascript\Games\SpaceInvaders\SpaceInvaders.sln
call :build javascript\Games\DockMaster\DockMaster.sln
call :build javascript\Games\LightsOut\LightsOut.sln
call :build javascript\Games\LightsOut2\LightsOut2.sln
call :build javascript\Games\GameOfLife\GameOfLife.sln
call :build javascript\Games\Mahjong\Mahjong.sln

call :build javascript\Examples\ButterFly\ButterFly.sln
call :build javascript\Examples\CardGames\CardGames.sln
call :build javascript\Examples\DragStan\drag.sln
call :build javascript\Examples\FormsExample\FormsExample.sln
call :build javascript\Examples\GMapsClone\GMapsClone.sln
call :build javascript\Examples\GoogleGears\GGearAlpha.sln
call :build javascript\Examples\HotPolygon\HotPolygon.sln
call :build javascript\Examples\HulaGirl\HulaGirl.sln
call :build javascript\Examples\ImageZoomer\ImageZoomer.sln
call :build javascript\Examples\MouseWheel\MouseWheel.sln

call :build javascript\Examples\NumberGuessingGame\NumberGuessingGame.sln
call :build javascript\Examples\SimpleBankPage\SimpleBankPage.sln
call :build javascript\Examples\SimpleFilmstrip\SimpleFilmstrip.sln
call :build javascript\Examples\SimpleRollover\SimpleRollover.sln
call :build javascript\Examples\SubSquare\SubSquare.sln
call :build javascript\Examples\TextRotator\TextRotator.sln
call :build javascript\Examples\TextEditorDemo\TextEditorDemo.sln




:: msbuild does not work with visual basic 9 at the moment...

::call :build templates\OrcasVisualBasicScriptApplication\OrcasVisualBasicScriptApplication.sln
::call :build javascript\Examples\FormsExample.VisualBasic\FormsExample.VisualBasic.sln


endlocal
goto :eof

:build
echo - %1
call %msbuild% %flags% %1
goto :eof