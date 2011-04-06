@echo off

@pushd
@echo + compile java
call "C:\Program Files\Java\jdk1.6.0_24\bin\javac.exe" -source 1.4 -target 1.4 -Xlint:all -Xlint:-unchecked -cp . -d bin-java  jni/Main.java
call "C:\Program Files\Java\jdk1.6.0_24\bin\javah.exe" -classpath bin-java -d . jni.CMalloc jni.CFunc jni.CPtr

set _jarfile=package.jar

call "C:\Program Files\Java\jdk1.6.0_24\bin\jar.exe" cvM -C bin-java . > bin\%_jarfile%


@popd