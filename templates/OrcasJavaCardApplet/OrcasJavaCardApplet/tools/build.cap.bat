@echo off
setlocal


set JAVA_HOME=C:\j2sdk1.4.2_17
::set JAVA_HOME=C:\Progra~1\Java\jre1.6.0_07
set JC_HOME=C:\util\java_card_kit-2_2_1

pushd ..\bin\Release\web

:: import settings
call setup.settings.cmd



:: error: OrcasJavaCardApplet.Cafebabe: unsupported class file format of version 48.0.
:: converter  <options>  package_name  package_aid  major_version.minor_version 

	::-exportpath  <list of directories> 
	::              list the root directories where the Converter  
	::              will look for export files 
		      
::call %JC_HOME%\bin\converter.bat -classdir release -out EXP JCA CAP -applet %APPLET_AID% %APPLET_CLASSNAME%  %PACKAGE_NAME% %PACKAGE_AID% 1.0
call %JC_HOME%\bin\converter.bat -exportpath %JC_HOME%\api_export_files -classdir release -out CAP -applet %AppletAID% %CompilandFullName%  %CompilandNamespace1% %PackageAID% 1.0


popd
endlocal