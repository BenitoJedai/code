@echo off
setlocal

call find.java javac.exe
set TargetPath=%ReturnValue%

if '%TargetPath%' == '' (
    echo java not found
    goto :eof
)

echo java is %TargetPath%
pushd ..\bin\%ConfigurationName%\web

:: import settings
call setup.settings.cmd

echo + compile java [%ProjectName%:%CompilandNamespace0%\%CompilandType%]

:: add additional libraries here
set TargetSourceFiles=java;%JC_HOME%\lib\javacardframework.jar;%JC_HOME%\lib\api.jar

::%TargetPath% -source 1.4 -target 1.4 -Xlint:all,-serial,-unchecked -cp %TargetSourceFiles% -d release  java\%CompilandNamespace0%\%CompilandType%.java
::%TargetPath% -source 1.4 -target 1.4 -classpath %TargetSourceFiles% -d release java\%CompilandNamespace0%\%CompilandType%.java
set __COMMAND=%TargetPath% -source 1.4 -target 1.4 -g  -classpath %TargetSourceFiles% -d release java\%CompilandNamespace0%\%CompilandType%.java

echo before javac
echo %__COMMAND%

%__COMMAND%
echo after javac

endlocal
popd
