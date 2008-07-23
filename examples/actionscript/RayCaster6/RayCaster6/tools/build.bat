:mxmlc
@echo off



:: Dll name
@call :jsc %1

if '%ERRORLEVEL%' == '-1' (
    echo jsc failed.
    goto :eof
)
:: Namespace name, type name
@call :mxmlc %1/ActionScript %1

goto :eof

:jsc
pushd ..\bin\debug

call c:\util\jsc\bin\jsc.exe %1.dll  -as


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
:: http://livedocs.adobe.com/flex/201/html/wwhelp/wwhimpl/common/html/wwhelp.htm?context=LiveDocs_Book_Parts&file=compilers_123_24.html
:: http://www.adobe.com/products/flex/sdk/
:: -compiler.verbose-stacktraces 
:: call C:\util\flex2\bin\mxmlc.exe -keep-as3-metadata -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
::call C:\util\flex\bin\mxmlc.exe -optimize -use-network=false -keep-as3-metadata -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
call C:\util\flex\bin\mxmlc.exe -default-script-limits=500,2 -debug -use-network=false -keep-as3-metadata -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
::call C:\util\flex\bin\mxmlc.exe -optimize -use-network=false -incremental=true -output=%2.swf -strict -sp=. %1/%2.as
goto :eof