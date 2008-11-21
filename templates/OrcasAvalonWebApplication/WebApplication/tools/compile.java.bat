@echo off
setlocal

call find.java javac.exe
set TargetPath=%ReturnValue%

if '%TargetPath%' == '' (
    echo java not found
    goto :eof
)


pushd ..\bin\release\web

:: add additional libraries here
set TargetSourceFiles=java

echo [%TargetPath% %1]
::%TargetPath% -source 1.4 -target 1.4 -Xlint:all,-serial,-unchecked -cp %TargetSourceFiles% -d release  java\%CompilandNamespace0%\%CompilandType%.java
%TargetPath% -source 1.4 -target 1.4 -classpath %TargetSourceFiles% -d release  java\%1\%2.java

if '%ERRORLEVEL%' == '-1' (
    echo javac failed.
    goto :eof
)

popd

call find.java jar.exe
set TargetPath=%ReturnValue%

if '%TargetPath%' == '' (
    echo jar not found
    goto :eof
)

pushd ..\bin\release\web

echo [%TargetPath% %2]

%TargetPath% cvM -C release . > bin\%2Package.jar

endlocal

