@echo off

@call :jsc %1

if '%ERRORLEVEL%' == '-1' (
    echo jsc failed.
    goto :eof
)

@call compile.java
@call create.jar

goto :eof

:jsc
pushd ..\bin\debug

call c:\util\jsc\bin\jsc.exe %1.dll -java
::call c:\util\jsc\bin\jsc.exe %1.dll -as


popd
goto :eof