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

call :build templates\OrcasScriptApplication\OrcasScriptApplication.sln

call :build javascript\Examples\ButterFly\ButterFly.sln
call :build javascript\Games\ConsoleWorm\ConsoleWorm.sln
call :build javascript\Examples\ThreeDStuff\ThreeDStuff.sln
call :build javascript\Games\SpaceInvaders\SpaceInvaders.sln
call :build javascript\Games\LightsOut2\LightsOut2.sln

goto :skip

::call :build core\ScriptCoreLib.Cards\ScriptCoreLib.Cards.sln
::call :build core\ScriptCoreLib.Drawing\ScriptCoreLib.Drawing.sln
::call :build core\ScriptCoreLib.Drawing\ScriptCoreLib.Drawing\ScriptCoreLib.Drawing.Vector.sln
::call :build core\ScriptCoreLib.Net\ScriptCoreLib.Net.sln

::call :build core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms.sln

:: rebuild controls
::call :build javascript\Controls\TextEditor\ScriptCoreLib.Controls.TextEditor.sln



:: rebuild templates

::call :build templates\AppletTemplate\DemoApplet.sln



:: rebuild examples


call :build javascript\Games\DockMaster\DockMaster.sln
call :build javascript\Games\LightsOut\LightsOut.sln

call :build javascript\Games\GameOfLife\GameOfLife.sln
call :build javascript\Games\Mahjong\Mahjong.sln


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

:skip



:: msbuild does not work with visual basic 9 at the moment...

::call :build templates\OrcasVisualBasicScriptApplication\OrcasVisualBasicScriptApplication.sln
::call :build javascript\Examples\FormsExample.VisualBasic\FormsExample.VisualBasic.sln

popd
endlocal
goto :eof

:build
echo - %1
call %msbuild% %flags% %1
goto :eof