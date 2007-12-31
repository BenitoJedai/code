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
call :build core\ScriptCoreLib.Cards\ScriptCoreLib.Cards.sln
call :build core\ScriptCoreLib.Net\ScriptCoreLib.Net.sln
call :build core\ScriptCoreLib.Drawing\ScriptCoreLib.Drawing.sln
call :build core\ScriptCoreLib.Drawing.Vector\ScriptCoreLib.Drawing.Vector\ScriptCoreLib.Drawing.Vector.sln
call :build core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms.sln

call :build javascript\Controls\LayeredControl\ScriptCoreLib.Controls.LayeredControl.sln
call :build javascript\Controls\TextEditor\ScriptCoreLib.Controls.TextEditor.sln

:: c# template
call :buildfast templates\OrcasScriptApplication\OrcasScriptApplication.sln
:: visual basic template
call :buildfast templates\OrcasVisualBasicScriptApplication\OrcasVisualBasicScriptApplication.sln

call :buildfast javascript\Examples\ButterFly\ButterFly.sln
call :buildfast javascript\Games\ConsoleWorm\ConsoleWorm.sln
call :buildfast javascript\Examples\ThreeDStuff\ThreeDStuff.sln
call :buildfast javascript\Games\SpaceInvaders\SpaceInvaders.sln
call :buildfast javascript\Games\LightsOut2\LightsOut2.sln
call :buildfast javascript\Examples\TextEditorDemo\TextEditorDemo.sln
call :buildfast javascript\Examples\CardGames\CardGames.sln
call :buildfast javascript\Examples\MouseWheel\MouseWheel.sln
call :buildfast javascript\Examples\ImageZoomer\ImageZoomer.sln
call :buildfast javascript\Examples\TextRotator\TextRotator.sln
call :buildfast javascript\Examples\TextScreenSaver\TextScreenSaver.sln
call :buildfast javascript\Examples\LinqToObjects\LinqToObjects.sln
call :buildfast javascript\Examples\NumberGuessingGame\NumberGuessingGame.sln
call :buildfast javascript\Examples\HulaGirl\HulaGirl.sln
call :buildfast javascript\Examples\SimpleBankPage\SimpleBankPage.sln
call :buildfast javascript\Examples\GMapsClone\GMapsClone.sln
call :buildfast javascript\Examples\SubSquare\SubSquare.sln
call :buildfast javascript\Examples\SimpleRollover\SimpleRollover.sln
call :buildfast javascript\Examples\SimpleFilmstrip\SimpleFilmstrip.sln
call :buildfast javascript\Examples\HotPolygon\HotPolygon.sln
call :buildfast javascript\Games\DockMaster\DockMaster.sln
call :buildfast javascript\Examples\GoogleGears\GGearAlpha.sln
call :buildfast javascript\Examples\MonthSchedule\MonthSchedule.sln
call :buildfast javascript\Games\GameOfLife\GameOfLife.sln
call :buildfast javascript\Examples\VectorExample\VectorExample.sln
call :buildfast javascript\Examples\FormsExample\FormsExample.sln
call :buildfast javascript\Examples\FormsExample.VisualBasic\FormsExample.VisualBasic.sln
call :buildfast javascript\Games\Mahjong\Mahjong.sln
call :buildfast javascript\Examples\RetroCanvas\RetroCanvas.sln

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
