@pushd
@echo + compile java
@x:\util\java5\bin\javac -source 1.4 -target 1.4 -Xlint:all -Xlint:-unchecked -cp . -d bin-java  jni/Main.java
@x:\util\java5\bin\javah -classpath bin-java -d . jni.CMalloc jni.CFunc jni.CPtr

set _jarfile=package.jar

x:\util\java5\bin\jar cvM -C bin-java . > bin\%_jarfile%


@popd