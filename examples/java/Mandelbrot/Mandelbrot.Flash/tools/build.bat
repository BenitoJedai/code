:mxmlc
@echo off

set TargetFileName=%2
set ConfigurationName=%3

@call :createproxy

if %ConfigurationName%==Debug (
  echo Debug mode will not perform post build!
  goto :eof
)


:: Dll name
@call :jsc %1

if '%ERRORLEVEL%' == '-1' (
    echo jsc failed.
    goto :eof
)
:: Namespace name, type name

echo running bash...
::call c:\cygwin\bin\bash.exe -login -c "where gcc"
set bash_path=/cygdrive/C/work/jsc.svn/examples/java/Mandelbrot/Mandelbrot.Flash/bin/Release/web
call c:\cygwin\bin\bash.exe -login -c "cd %bash_path% && gcc %TargetFileName%.c -O3 -Wall -swc -o %TargetFileName%.swc"
:: gcc FlashAlchemyEcho.dll.c -O3 -Wall -swc -o stringecho.swc

@call :mxmlc Mandelbrot/Flash/ActionScript MandelbrotFlash

goto :eof

:jsc
pushd ..\bin\%ConfigurationName%

call c:\util\jsc\bin\jsc.exe %TargetFileName% -c -as


popd
goto :eof

:mxmlc
@echo off
pushd ..\bin\%ConfigurationName%\web



call :build %1 %2


popd
goto :eof

:build
echo - %2
:: http://www.adobe.com/products/flex/sdk/
:: -compiler.verbose-stacktraces 
:: call C:\util\flex2\bin\mxmlc.exe -keep-as3-metadata -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
:: mxmlc.exe -library-path+=stringecho.swc -target-player=10.0.0 as3/EchoTest.as

::call C:\util\flex33\bin\mxmlc.exe -debug -library-path+=%TargetFileName%.swc -target-player=10.0.0 -keep-as3-metadata -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
call C:\util\flex33\bin\mxmlc.exe -optimize -library-path+=%TargetFileName%.swc -target-player=10.0.0 -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
goto :eof

:createproxy

call c:\util\jsc\bin\ScriptCoreLib.Alchemy.ExportGenerator.exe "..\bin\%ConfigurationName%\%TargetFileName%" "Mandelbrot.Flash.Alchemy.AlchemyProgram" "..\Alchemy\AlchemyProgram.Dispatch.cs" "Mandelbrot.Flash.ActionScript.MandelbrotProxy"


goto :eof