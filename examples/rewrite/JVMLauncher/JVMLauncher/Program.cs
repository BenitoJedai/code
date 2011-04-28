using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace JVMLauncher
{
    [StructLayout(LayoutKind.Sequential)]
    struct _jclass
    {
    }

    [StructLayout(LayoutKind.Sequential)]
    struct _jmethodID
    {
    }

    unsafe delegate _jclass* JNINativeInterface_FindClass(JNIENV* env, string name);
    unsafe delegate _jmethodID* JNINativeInterface_GetStaticMethodID(JNIENV* env, _jclass* clazz, string name, string sig);
    unsafe delegate void JNINativeInterface_CallStaticVoidMethodA(JNIENV* env, _jclass* clazz, _jmethodID* methodID, void* args);

    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 932)]
    unsafe struct JNINativeInterface
    {
        public IntPtr reserved0;
        public IntPtr reserved1;
        public IntPtr reserved2;
        public IntPtr reserved3;
        public IntPtr GetVersion;
        public IntPtr DefineClass;
        public IntPtr FindClass;
        public IntPtr FromReflectedMethod;
        public IntPtr FromReflectedField;
        public IntPtr ToReflectedMethod;
        public IntPtr GetSuperclass;
        public IntPtr IsAssignableFrom;
        public IntPtr ToReflectedField;
        public IntPtr Throw;
        public IntPtr ThrowNew;
        public IntPtr ExceptionOccurred;
        public IntPtr ExceptionDescribe;
        public IntPtr ExceptionClear;
        public IntPtr FatalError;
        public IntPtr PushLocalFrame;
        public IntPtr PopLocalFrame;
        public IntPtr NewGlobalRef;
        public IntPtr DeleteGlobalRef;
        public IntPtr DeleteLocalRef;
        public IntPtr IsSameObject;
        public IntPtr NewLocalRef;
        public IntPtr EnsureLocalCapacity;
        public IntPtr AllocObject;
        public IntPtr NewObject;
        public IntPtr NewObjectV;
        public IntPtr NewObjectA;
        public IntPtr GetObjectClass;
        public IntPtr IsInstanceOf;
        public IntPtr GetMethodID;
        public IntPtr CallObjectMethod;
        public IntPtr CallObjectMethodV;
        public IntPtr CallObjectMethodA;
        public IntPtr CallBooleanMethod;
        public IntPtr CallBooleanMethodV;
        public IntPtr CallBooleanMethodA;
        public IntPtr CallByteMethod;
        public IntPtr CallByteMethodV;
        public IntPtr CallByteMethodA;
        public IntPtr CallCharMethod;
        public IntPtr CallCharMethodV;
        public IntPtr CallCharMethodA;
        public IntPtr CallShortMethod;
        public IntPtr CallShortMethodV;
        public IntPtr CallShortMethodA;
        public IntPtr CallIntMethod;
        public IntPtr CallIntMethodV;
        public IntPtr CallIntMethodA;
        public IntPtr CallLongMethod;
        public IntPtr CallLongMethodV;
        public IntPtr CallLongMethodA;
        public IntPtr CallFloatMethod;
        public IntPtr CallFloatMethodV;
        public IntPtr CallFloatMethodA;
        public IntPtr CallDoubleMethod;
        public IntPtr CallDoubleMethodV;
        public IntPtr CallDoubleMethodA;
        public IntPtr CallVoidMethod;
        public IntPtr CallVoidMethodV;
        public IntPtr CallVoidMethodA;
        public IntPtr CallNonvirtualObjectMethod;
        public IntPtr CallNonvirtualObjectMethodV;
        public IntPtr CallNonvirtualObjectMethodA;
        public IntPtr CallNonvirtualBooleanMethod;
        public IntPtr CallNonvirtualBooleanMethodV;
        public IntPtr CallNonvirtualBooleanMethodA;
        public IntPtr CallNonvirtualByteMethod;
        public IntPtr CallNonvirtualByteMethodV;
        public IntPtr CallNonvirtualByteMethodA;
        public IntPtr CallNonvirtualCharMethod;
        public IntPtr CallNonvirtualCharMethodV;
        public IntPtr CallNonvirtualCharMethodA;
        public IntPtr CallNonvirtualShortMethod;
        public IntPtr CallNonvirtualShortMethodV;
        public IntPtr CallNonvirtualShortMethodA;
        public IntPtr CallNonvirtualIntMethod;
        public IntPtr CallNonvirtualIntMethodV;
        public IntPtr CallNonvirtualIntMethodA;
        public IntPtr CallNonvirtualLongMethod;
        public IntPtr CallNonvirtualLongMethodV;
        public IntPtr CallNonvirtualLongMethodA;
        public IntPtr CallNonvirtualFloatMethod;
        public IntPtr CallNonvirtualFloatMethodV;
        public IntPtr CallNonvirtualFloatMethodA;
        public IntPtr CallNonvirtualDoubleMethod;
        public IntPtr CallNonvirtualDoubleMethodV;
        public IntPtr CallNonvirtualDoubleMethodA;
        public IntPtr CallNonvirtualVoidMethod;
        public IntPtr CallNonvirtualVoidMethodV;
        public IntPtr CallNonvirtualVoidMethodA;
        public IntPtr GetFieldID;
        public IntPtr GetObjectField;
        public IntPtr GetBooleanField;
        public IntPtr GetByteField;
        public IntPtr GetCharField;
        public IntPtr GetShortField;
        public IntPtr GetIntField;
        public IntPtr GetLongField;
        public IntPtr GetFloatField;
        public IntPtr GetDoubleField;
        public IntPtr SetObjectField;
        public IntPtr SetBooleanField;
        public IntPtr SetByteField;
        public IntPtr SetCharField;
        public IntPtr SetShortField;
        public IntPtr SetIntField;
        public IntPtr SetLongField;
        public IntPtr SetFloatField;
        public IntPtr SetDoubleField;
        public IntPtr GetStaticMethodID;
        public IntPtr CallStaticObjectMethod;
        public IntPtr CallStaticObjectMethodV;
        public IntPtr CallStaticObjectMethodA;
        public IntPtr CallStaticBooleanMethod;
        public IntPtr CallStaticBooleanMethodV;
        public IntPtr CallStaticBooleanMethodA;
        public IntPtr CallStaticByteMethod;
        public IntPtr CallStaticByteMethodV;
        public IntPtr CallStaticByteMethodA;
        public IntPtr CallStaticCharMethod;
        public IntPtr CallStaticCharMethodV;
        public IntPtr CallStaticCharMethodA;
        public IntPtr CallStaticShortMethod;
        public IntPtr CallStaticShortMethodV;
        public IntPtr CallStaticShortMethodA;
        public IntPtr CallStaticIntMethod;
        public IntPtr CallStaticIntMethodV;
        public IntPtr CallStaticIntMethodA;
        public IntPtr CallStaticLongMethod;
        public IntPtr CallStaticLongMethodV;
        public IntPtr CallStaticLongMethodA;
        public IntPtr CallStaticFloatMethod;
        public IntPtr CallStaticFloatMethodV;
        public IntPtr CallStaticFloatMethodA;
        public IntPtr CallStaticDoubleMethod;
        public IntPtr CallStaticDoubleMethodV;
        public IntPtr CallStaticDoubleMethodA;
        public IntPtr CallStaticVoidMethod;
        public IntPtr CallStaticVoidMethodV;
        public IntPtr CallStaticVoidMethodA;
        public IntPtr GetStaticFieldID;
        public IntPtr GetStaticObjectField;
        public IntPtr GetStaticBooleanField;
        public IntPtr GetStaticByteField;
        public IntPtr GetStaticCharField;
        public IntPtr GetStaticShortField;
        public IntPtr GetStaticIntField;
        public IntPtr GetStaticLongField;
        public IntPtr GetStaticFloatField;
        public IntPtr GetStaticDoubleField;
        public IntPtr SetStaticObjectField;
        public IntPtr SetStaticBooleanField;
        public IntPtr SetStaticByteField;
        public IntPtr SetStaticCharField;
        public IntPtr SetStaticShortField;
        public IntPtr SetStaticIntField;
        public IntPtr SetStaticLongField;
        public IntPtr SetStaticFloatField;
        public IntPtr SetStaticDoubleField;
        public IntPtr NewString;
        public IntPtr GetStringLength;
        public IntPtr GetStringChars;
        public IntPtr ReleaseStringChars;
        public IntPtr NewStringUTF;
        public IntPtr GetStringUTFLength;
        public IntPtr GetStringUTFChars;
        public IntPtr ReleaseStringUTFChars;
        public IntPtr GetArrayLength;
        public IntPtr NewObjectArray;
        public IntPtr GetObjectArrayElement;
        public IntPtr SetObjectArrayElement;
        public IntPtr NewBooleanArray;
        public IntPtr NewByteArray;
        public IntPtr NewCharArray;
        public IntPtr NewShortArray;
        public IntPtr NewIntArray;
        public IntPtr NewLongArray;
        public IntPtr NewFloatArray;
        public IntPtr NewDoubleArray;
        public IntPtr GetBooleanArrayElements;
        public IntPtr GetByteArrayElements;
        public IntPtr GetCharArrayElements;
        public IntPtr GetShortArrayElements;
        public IntPtr GetIntArrayElements;
        public IntPtr GetLongArrayElements;
        public IntPtr GetFloatArrayElements;
        public IntPtr GetDoubleArrayElements;
        public IntPtr ReleaseBooleanArrayElements;
        public IntPtr ReleaseByteArrayElements;
        public IntPtr ReleaseCharArrayElements;
        public IntPtr ReleaseShortArrayElements;
        public IntPtr ReleaseIntArrayElements;
        public IntPtr ReleaseLongArrayElements;
        public IntPtr ReleaseFloatArrayElements;
        public IntPtr ReleaseDoubleArrayElements;
        public IntPtr GetBooleanArrayRegion;
        public IntPtr GetByteArrayRegion;
        public IntPtr GetCharArrayRegion;
        public IntPtr GetShortArrayRegion;
        public IntPtr GetIntArrayRegion;
        public IntPtr GetLongArrayRegion;
        public IntPtr GetFloatArrayRegion;
        public IntPtr GetDoubleArrayRegion;
        public IntPtr SetBooleanArrayRegion;
        public IntPtr SetByteArrayRegion;
        public IntPtr SetCharArrayRegion;
        public IntPtr SetShortArrayRegion;
        public IntPtr SetIntArrayRegion;
        public IntPtr SetLongArrayRegion;
        public IntPtr SetFloatArrayRegion;
        public IntPtr SetDoubleArrayRegion;
        public IntPtr RegisterNatives;
        public IntPtr UnregisterNatives;
        public IntPtr MonitorEnter;
        public IntPtr MonitorExit;
        public IntPtr GetJavaVM;
        public IntPtr GetStringRegion;
        public IntPtr GetStringUTFRegion;
        public IntPtr GetPrimitiveArrayCritical;
        public IntPtr ReleasePrimitiveArrayCritical;
        public IntPtr GetStringCritical;
        public IntPtr ReleaseStringCritical;
        public IntPtr NewWeakGlobalRef;
        public IntPtr DeleteWeakGlobalRef;
        public IntPtr ExceptionCheck;
        public IntPtr NewDirectByteBuffer;
        public IntPtr GetDirectBufferAddress;
        public IntPtr GetDirectBufferCapacity;
        public IntPtr GetObjectRefType;
    }

    [StructLayout(LayoutKind.Sequential)]
    unsafe struct JavaVM
    {
        public JNINativeInterface* functions;
    }

    [StructLayout(LayoutKind.Sequential)]
    unsafe struct JNIENV
    {
        public JNINativeInterface* functions;
    }

    [StructLayout(LayoutKind.Sequential)]
    unsafe struct JavaVMOption
    {
        public string optionString;
        public void* extraInfo;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    unsafe struct JavaVMInitArgs
    {
        public int version;
        public int nOptions;

        public void* options;

        public byte ignoreUnrecognized;
    }

    unsafe delegate int JNI_CreateJavaVM(JavaVM** pvm, JNIENV** penv, IntPtr args);


    unsafe static class Program
    {
        [DllImport("kernel32", SetLastError = true)]
        static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("KERNEL32.DLL", CharSet = CharSet.Ansi, EntryPoint = "GetProcAddress", ExactSpelling = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, String lpProcName);



        static void Main(string[] args)
        {
            var RUNTIME_DLL = "C:\\Program Files\\Java\\jdk1.6.0_24\\jre\\bin\\client\\jvm.dll";
            var CLASS_PATH = "-Djava.class.path=Z:\\research\\20110427_jvmdll\\19065\\JavaDaemon\\HelloKNR\\HelloKNR.jar";
            var CLASS_NAME = "com/doorul/HelloKNR";
            var JNI_VERSION_1_4 = 0x00010004;

            var handle = LoadLibrary(RUNTIME_DLL);

            var __JNI_CreateJavaVM = GetProcAddress(handle, "JNI_CreateJavaVM");

            // http://www.codeproject.com/KB/cs/DynamicInvokeCSharp.aspx
            var JNI_CreateJavaVM = (JNI_CreateJavaVM)Marshal.GetDelegateForFunctionPointer(__JNI_CreateJavaVM, typeof(JNI_CreateJavaVM));

            var options = new JavaVMOption { optionString = 
                CLASS_PATH };
                //(void*)Marshal.StringToHGlobalAnsi(CLASS_PATH) };

            var options_ptr_len = Marshal.SizeOf(options) + 64;
            IntPtr options_ptr = Marshal.AllocHGlobal(options_ptr_len);
            Marshal.Copy(Enumerable.Range(0, options_ptr_len).Select(k => (byte)0xCC).ToArray(), 0, options_ptr, options_ptr_len);
            Marshal.StructureToPtr(options, options_ptr, false);


            var vm_args = new JavaVMInitArgs
            {
                version = JNI_VERSION_1_4,
                options = (void*)options_ptr,
                nOptions = 1,
                ignoreUnrecognized = 0
            };

            JavaVM* vm;
            JNIENV* env;



            // http://msdn.microsoft.com/en-us/library/system.runtime.interopservices.marshal.structuretoptr.aspx#Y800
            var vm_args_ptr_len = Marshal.SizeOf(vm_args) + 64;
            IntPtr vm_args_ptr = Marshal.AllocHGlobal(vm_args_ptr_len);
            Marshal.Copy(Enumerable.Range(0, vm_args_ptr_len).Select(k => (byte)0xCC).ToArray(), 0, vm_args_ptr, vm_args_ptr_len);

            Marshal.StructureToPtr(vm_args, vm_args_ptr, false);

            var res = JNI_CreateJavaVM((JavaVM**)&vm, (JNIENV**)&env, vm_args_ptr);

            var FindClass = (JNINativeInterface_FindClass)Marshal.GetDelegateForFunctionPointer(
                env->functions->FindClass,
                typeof(JNINativeInterface_FindClass)
            );


            var cls = FindClass(env,
                CLASS_NAME
                );

                //(void*)Marshal.StringToHGlobalAnsi(CLASS_NAME));


            var GetStaticMethodID = (JNINativeInterface_GetStaticMethodID)Marshal.GetDelegateForFunctionPointer(
                env->functions->GetStaticMethodID,
                typeof(JNINativeInterface_GetStaticMethodID)
            );


            var mid = GetStaticMethodID(
                env,
                cls,
                "main",
                "([Ljava/lang/String;)V"
                //(void*)Marshal.StringToHGlobalAnsi("main"),
                //(void*)Marshal.StringToHGlobalAnsi("([Ljava/lang/String;)V")
            );

            var CallStaticVoidMethodA = (JNINativeInterface_CallStaticVoidMethodA)Marshal.GetDelegateForFunctionPointer(
               env->functions->CallStaticVoidMethodA,
               typeof(JNINativeInterface_CallStaticVoidMethodA)
           );

            // http://www.velocityreviews.com/forums/t370129-java-native-interface-translate-java-call-to-jni.html

            ///* build the argument list */
            //str = (*env)->FindClass(env, "java/lang/String");
            //jargs = (*env)->NewObjectArray(env, num_args, str, NULL);

            ///* prefer to do this in a loop if args already in an array of char* */
            //(*env)->SetObjectArrayElement(env, jargs, 0, (*env)->NewStringUTF(env, "-fo"))
            //(*env)->SetObjectArrayElement(env, jargs, 1, (*env)->NewStringUTF(env, "Data.xml"))
            //(*env)->SetObjectArrayElement(env, jargs, 2, (*env)->NewStringUTF(env, "-c"))
            ///* etcetera */

            IntPtr __args = Marshal.AllocHGlobal(8);
            Marshal.Copy(Enumerable.Range(0, 8).Select(k => (byte)0x00).ToArray(), 0, __args, 8);

            CallStaticVoidMethodA(env, cls, mid, (void*)__args);
        }
    }
}
