:mxmlc
@echo off

set TargetFileName=%2
set ConfigurationName=%3

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
::@call :mxmlc %1/ActionScript %1
echo running bash...
::call c:\cygwin\bin\bash.exe -login -c "where gcc"
set bash_path=/cygdrive/c/work/jsc.svn/examples/actionscript/FlashAlchemyEcho/FlashAlchemyEcho/bin/Release/web
call c:\cygwin\bin\bash.exe -login -c "cd %bash_path% && gcc %TargetFileName%.c -O3 -Wall -swc -o %TargetFileName%.swc"
:: gcc FlashAlchemyEcho.dll.c -O3 -Wall -swc -o stringecho.swc

goto :eof

:jsc
pushd ..\bin\%ConfigurationName%

call c:\util\jsc\bin\jsc.exe %1.dll  -x


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
call C:\util\flex\bin\mxmlc.exe -keep-as3-metadata -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
goto :eof