@echo off
setlocal

set JAVA_HOME=C:\util\j2sdk1.4.2_19
set JC_HOME=C:\util\java_card_kit-2_2_1

pushd ..\bin\Release\web

:: import settings
echo before settings
call setup.settings.cmd
echo after settings


:: error: OrcasJavaCardApplet.Cafebabe: unsupported class file format of version 48.0.
:: converter  <options>  package_name  package_aid  major_version.minor_version 

	::-exportpath  <list of directories> 
	::              list the root directories where the Converter  
	::              will look for export files 
		      
::call %JC_HOME%\bin\converter.bat -classdir release -out EXP JCA CAP -applet %APPLET_AID% %APPLET_CLASSNAME%  %PACKAGE_NAME% %PACKAGE_AID% 1.0
set __COMMAND=%JC_HOME%\bin\converter.bat -exportpath %JC_HOME%\api_export_files -classdir release -out CAP -applet %AppletAID% %CompilandFullName%  %CompilandNamespace1% %PackageAID% 1.0

echo JAVA_HOME is %JAVA_HOME%
echo before jc converter
echo %__COMMAND%

%__COMMAND%
echo after jc converter


popd
endlocal