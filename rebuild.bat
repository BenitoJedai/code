@echo off
setlocal

set msbuild=%SystemRoot%\Microsoft.NET\Framework\v3.5\MSBuild.exe
set msbuild40=%SystemRoot%\Microsoft.NET\Framework\v4.0.30128\MSBuild.exe

set flags=/nologo /verbosity:q

set target=C:\util\jsc\bin

mkdir C:\util\jsc\bin
mkdir C:\util\jsc\lib

:: C:\util\jsc\bin is expected to exist as compiler and framework assemblies will be placed there

:: rebuild compiler
call :build compiler\ScriptCoreLibA\ScriptCoreLibA.sln
call :build compiler\jsc\jsc.sln


:: java
call :build core\ScriptCoreLibJava\ScriptCoreLibJava.sln
call :build core\ScriptCoreLibJava.Web\ScriptCoreLibJava.Web.sln
call :build core\ScriptCoreLibJava.Web.Services\ScriptCoreLibJava.Web.Services.sln
call :build core\ScriptCoreLibJava.jni\ScriptCoreLibJava.jni.sln

:: rebuild framework

call :build core\jsc.server\jsc.server.sln
call :build core\ScriptCoreLib\ScriptCoreLib.sln
call :build core\ScriptCoreLib.Reflection.Options\ScriptCoreLib.Reflection.Options.sln
call :build core\ScriptCoreLib.Query\ScriptCoreLib.Query.sln
call :build core\ScriptCoreLib.XLinq\ScriptCoreLib.XLinq.sln
call :build core\ScriptCoreLib.Cards\ScriptCoreLib.Cards.sln
call :build core\ScriptCoreLib.Net\ScriptCoreLib.Net.sln
call :build core\ScriptCoreLib.Nonoba\ScriptCoreLib.Nonoba.sln
call :build core\ScriptCoreLib.Mochi\ScriptCoreLib.Mochi.sln
call :build core\ScriptCoreLib.GoogleMaps\ScriptCoreLib.GoogleMaps.sln
call :build core\ScriptCoreLib.Drawing\ScriptCoreLib.Drawing.sln
call :build core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms.sln
call :build core\ScriptCoreLib.Archive.ZIP\ScriptCoreLib.Archive.ZIP.sln
call :build core\ScriptCoreLib.Archive\ScriptCoreLib.Archive.sln
call :build core\ScriptCoreLib.Avalon\ScriptCoreLib.Avalon.sln
call :build core\ScriptCoreLib.RayCaster\ScriptCoreLib.RayCaster.sln
call :build core\ScriptCoreLib.Maze\ScriptCoreLib.Maze.sln


:: build some tools
call :build javascript\Tools\ConvertASToCS\CreateNetworkProxy\CreateNetworkProxy.csproj


:: rebuild controls
call :build javascript\Controls\TextEditor\ScriptCoreLib.Controls.TextEditor.sln
call :build javascript\Controls\LayeredControl\ScriptCoreLib.Controls.LayeredControl.sln
call :build javascript\Controls\ScriptCoreLib.Controls.NatureBoy\ScriptCoreLib.Controls.NatureBoy.sln


call :build compiler\jsc.meta\jsc.meta.sln

:: rebuild Ultra templates
call :build templates\Orcas\OrcasUltraApplication\OrcasUltraApplication.sln
call :build templates\Orcas\OrcasUltraWebApplication\OrcasUltraWebApplication.sln

call :build40 templates\TwentyTen\UltraApplication\UltraApplication.sln
call :build40 templates\TwentyTen\UltraWebApplication\UltraWebApplication.sln

call :build40 templates\TwentyTen\FSharp\UltraApplication\UltraApplication.sln


:: rebuild templates
call :build templates\OrcasScriptApplication\OrcasScriptApplication.sln
call :build templates\OrcasFlashApplication\OrcasFlashApplication\OrcasFlashApplication.sln

call :build templates\AppletTemplate\DemoApplet.sln
call :build templates\OrcasWebApplication\OrcasWebApplication.sln
call :build templates\OrcasVisualBasicFlashApplication\OrcasVisualBasicFlashApplication.sln
call :build templates\OrcasVisualBasicScriptApplication\OrcasVisualBasicScriptApplication.sln

::call :build templates\OrcasWebSite\OrcasWebSite.sln



:: rebuild examples
:: call :build javascript\Examples\ButterFly\ButterFly.sln

:: call :build javascript\Games\SpaceInvaders\SpaceInvaders.sln
:: call :build javascript\Games\DockMaster\DockMaster.sln
:: call :build javascript\Games\LightsOut\LightsOut.sln
:: call :build javascript\Games\LightsOut2\LightsOut2.sln
:: call :build javascript\Games\GameOfLife\GameOfLife.sln
:: call :build javascript\Games\Mahjong\Mahjong.sln

:: call :build javascript\Examples\ButterFly\ButterFly.sln
:: call :build javascript\Examples\CardGames\CardGames.sln
:: call :build javascript\Examples\DragStan\drag.sln
:: call :build javascript\Examples\FormsExample\FormsExample.sln
:: call :build javascript\Examples\GMapsClone\GMapsClone.sln
:: call :build javascript\Examples\GoogleGears\GGearAlpha.sln
:: call :build javascript\Examples\HotPolygon\HotPolygon.sln
:: call :build javascript\Examples\HulaGirl\HulaGirl.sln
:: call :build javascript\Examples\ImageZoomer\ImageZoomer.sln
:: call :build javascript\Examples\MouseWheel\MouseWheel.sln

:: call :build javascript\Examples\NumberGuessingGame\NumberGuessingGame.sln
:: call :build javascript\Examples\SimpleBankPage\SimpleBankPage.sln
:: call :build javascript\Examples\SimpleFilmstrip\SimpleFilmstrip.sln
:: call :build javascript\Examples\SimpleRollover\SimpleRollover.sln
:: call :build javascript\Examples\SubSquare\SubSquare.sln
:: call :build javascript\Examples\TextRotator\TextRotator.sln
:: call :build javascript\Examples\TextEditorDemo\TextEditorDemo.sln




::call :build javascript\Examples\FormsExample.VisualBasic\FormsExample.VisualBasic.sln

call c:\util\jsc\bin\jsc.meta.exe RewriteToInstaller

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