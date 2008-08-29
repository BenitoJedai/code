:mxmlc
@echo off

:: Dll name
@call :jsc %1



:: http://msdn.microsoft.com/en-us/library/ms379563(VS.80).aspx
call "C:\WINDOWS\Microsoft.NET\Framework\v3.5\csc.exe" /debug /out:"..\bin\debug\web\%1.NonobaServer.dll" /t:library  /recurse:"..\bin\debug\web\*.cs"  /lib:.. /r:"..\FlashTreasureHunt.MultiPlayer\Library\Nonoba.GameLibrary.dll"


:: Namespace name, type name
@call :mxmlc FlashTreasureHunt/ActionScript Game
::@call :mxmlc FlashTreasureHunt/ActionScript FlashTreasureHunt
::@call :mxmlc FlashTreasureHunt/ActionScript/Monetized MochiPreloader

goto :eof

:jsc
pushd ..\bin\debug

call c:\util\jsc\bin\jsc.exe %1.dll  -as -cs2


popd
goto :eof

:mxmlc
@echo off
pushd ..\bin\debug\web



call :build %1 %2


popd
goto :eof

:build
echo - %2
:: http://www.adobe.com/products/flex/sdk/
:: -compiler.verbose-stacktraces 
::call C:\util\flex\bin\mxmlc.exe -keep-as3-metadata -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
::call C:\util\flex\bin\mxmlc.exe -debug -use-network=true -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
call C:\util\flex\bin\mxmlc.exe -debug -optimize -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
::call C:\util\flex\bin\mxmlc.exe -optimize -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
::call C:\util\flex\bin\mxmlc.exe -optimize -use-network=false -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
goto :eof