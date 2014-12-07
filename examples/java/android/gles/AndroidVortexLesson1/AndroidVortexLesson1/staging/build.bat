rem build

echo hi

::set JAVA_HOME=C:\Program Files (x86)\Java\jdk1.6.0_32
set JAVA_HOME=C:\Program Files (x86)\Java\jdk1.7.0_45
call "C:\util\apache-ant-1.9.2\bin\ant.bat" debug -f build.xml
::call C:\util\apache-ant-1.8.3\bin\ant.bat debug -f build.xml