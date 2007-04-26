/*
 * @(#)dispatch.c	1.9 98/03/22
 * 
 * Copyright (c) 1998 Sun Microsystems, Inc. All Rights Reserved.
 *
 * See also the LICENSE file in this distribution.
 */

/*
 * JNI native methods supporting the infrastructure for shared
 * dispatchers.  Includes native methods for classes CPtr, CFunc, and
 * CMalloc.
 */

#ifdef SOLARIS2
#include <dlfcn.h>
#define LOAD_LIBRARY(name) dlopen(name, RTLD_LAZY)
#define FIND_ENTRY(lib, name) dlsym(lib, name)
#endif

#ifdef WIN32
#include <windows.h>
#define LOAD_LIBRARY(name) LoadLibrary(name)
#define FIND_ENTRY(lib, name) GetProcAddress(lib, name)
#endif

#include <stdlib.h>
#include <string.h>

#include <jni.h>

#include "jni_CPtr.h"
#include "jni_CFunc.h"
#include "jni_CMalloc.h"

/* Global references to frequently used classes and objects */
static jclass classString;
static jclass classInteger;
static jclass classFloat;
static jclass classDouble;
static jclass classCPtr;
static jobject objectCPtrNULL;

/* Cached field and method IDs */
static jmethodID String_getBytes_ID;
static jmethodID String_init_ID;
static jfieldID Integer_value_ID;
static jfieldID Float_value_ID;
static jfieldID Double_value_ID;
static jfieldID CPtr_peer_ID;

static jboolean __CFunc_IsVerbose;

/* Forward declarations */
static void throwByName(JNIEnv *env, const char *name, const char *msg);
static char * getJavaString(JNIEnv *env, jstring jstr);
static jstring newJavaString(JNIEnv *env, const char *str);
static jobject makeCPtr(JNIEnv *env, void *p);


/********************************************************************/
/*		     Native methods of class CFunc		    */
/********************************************************************/

/* These are the set of types CFunc can handle now */
typedef enum {
    TY_CPTR = 0,
    TY_INTEGER,
    TY_FLOAT,
    TY_DOUBLE,
    TY_DOUBLE2,
    TY_STRING,
} ty_t;

/* represent a machine word */
typedef union {
    jint i;
    jfloat f;
    void *p;
} word_t;

/* A CPU-dependent assembly routine that passes the arguments to C
 * stack and invoke the function.
 */
extern void 
asm_dispatch(void *func, int nwords, word_t *c_args, ty_t ty, jvalue *resP);

/* invoke the real native function */
static void
dispatch(JNIEnv *env, jobject self, jobjectArray arr, ty_t ty, jvalue *resP)
{
#define MAX_NARGS 32
    int i, nargs, nwords;
    void *func;
    char argTypes[MAX_NARGS];
    word_t c_args[MAX_NARGS * 2];

    nargs = (*env)->GetArrayLength(env, arr);
    if (nargs > MAX_NARGS) {
        throwByName(env, "java/lang/IllegalArgumentException",
		    "too many arguments");
	return;
    }
    
    func = (void *)(*env)->GetLongField(env, self, CPtr_peer_ID);

    if (__CFunc_IsVerbose)
        printf("[jni dispatch] function (");
    
    for (nwords = 0, i = 0; i < nargs; i++) 
    {
        jobject arg = (*env)->GetObjectArrayElement(env, arr, i);
        
        if (arg == NULL) {
            throwByName(env, "java/lang/NullPointerException",
                "bad argument");
            goto cleanup;
        }
        
        if (__CFunc_IsVerbose)
        if (i > 0)
            printf(", ");
        
        if ((*env)->IsInstanceOf(env, arg, classInteger)) {
            c_args[nwords].i = (*env)->GetIntField(env, arg, Integer_value_ID);
            argTypes[nwords++] = TY_INTEGER;
            
            if (__CFunc_IsVerbose)
                printf("int");
            
        } else if ((*env)->IsInstanceOf(env, arg, classCPtr)) {
            c_args[nwords].p = 
                (void *)(*env)->GetLongField(env, arg, CPtr_peer_ID);
            argTypes[nwords++] = TY_CPTR;
            
            if (__CFunc_IsVerbose)
                printf("void*");
            
        } else if ((*env)->IsInstanceOf(env, arg, classString)) {
            if ((c_args[nwords].p = getJavaString(env, arg)) == 0) {
                goto cleanup;
            }
            argTypes[nwords++] = TY_STRING;
            
            if (__CFunc_IsVerbose)
                printf("char*");
            
        } else if ((*env)->IsInstanceOf(env, arg, classFloat)) {
            c_args[nwords].f = (*env)->GetFloatField(env, arg, Float_value_ID);
            argTypes[nwords++] = TY_FLOAT;
            
            if (__CFunc_IsVerbose)
                printf("float");
            
        } else if ((*env)->IsInstanceOf(env, arg, classDouble)) {
            *(jdouble *)(c_args + nwords) = 
                (*env)->GetDoubleField(env, arg, Double_value_ID);
            argTypes[nwords] = TY_DOUBLE;
            /* harmless with 64-bit machines*/
            argTypes[nwords + 1] = TY_DOUBLE2;
            /* make sure things work on 64-bit machines */
            nwords += sizeof(jdouble) / sizeof(word_t);
            
            if (__CFunc_IsVerbose)
                printf("double");
        } else {
            throwByName(env, "java/lang/IllegalArgumentException",
                "unrecognized argument type");
            goto cleanup;
        }
        (*env)->DeleteLocalRef(env, arg);
        
        
    }
    
    if (__CFunc_IsVerbose)
    {
        printf(")\n");
        fflush(stdout);
    }

    asm_dispatch(func, nwords, c_args, ty, resP);

cleanup:
    for (i = 0; i < nwords; i++) {
        if (argTypes[i] == TY_STRING) {
	    free(c_args[i].p);
	}
    }
    return;
}

JNIEXPORT void JNICALL Java_jni_CFunc_SetVerbose
  (JNIEnv *env, jclass self, jboolean value)
{
    __CFunc_IsVerbose = value;
}

/*
 * Class:     CFunc
 * Method:    callCPtr
 * Signature: ([Ljava/lang/Object;)LCPtr;
 */
JNIEXPORT jobject JNICALL 
Java_jni_CFunc_callCPtr(JNIEnv *env, jobject self, jobjectArray arr)
{
    jvalue result;
    dispatch(env, self, arr, TY_CPTR, &result);
    if ((*env)->ExceptionOccurred(env)) {
        return NULL;
    }
    return makeCPtr(env, (void *)result.j);
}

/*
 * Class:     CFunc
 * Method:    callDouble
 * Signature: ([Ljava/lang/Object;)D
 */
JNIEXPORT jdouble JNICALL 
Java_jni_CFunc_callDouble(JNIEnv *env, jobject self, jobjectArray arr)
{
    jvalue result;
    dispatch(env, self, arr, TY_DOUBLE, &result);
    return result.d;
}

/*
 * Class:     CFunc
 * Method:    callFloat
 * Signature: ([Ljava/lang/Object;)F
 */
JNIEXPORT jfloat JNICALL
Java_jni_CFunc_callFloat(JNIEnv *env, jobject self, jobjectArray arr)
{
    jvalue result;
    dispatch(env, self, arr, TY_FLOAT, &result);
    return result.f;
}

/*
 * Class:     CFunc
 * Method:    callInt
 * Signature: ([Ljava/lang/Object;)I
 */
JNIEXPORT jint JNICALL
Java_jni_CFunc_callInt(JNIEnv *env, jobject self, jobjectArray arr)
{
    jvalue result;
    dispatch(env, self, arr, TY_INTEGER, &result);
    return result.i;
}



/*
 * Class:     CFunc
 * Method:    callVoid
 * Signature: ([Ljava/lang/Object;)V
 */
JNIEXPORT void JNICALL
Java_jni_CFunc_callVoid(JNIEnv *env, jobject self, jobjectArray arr)
{
    jvalue result;
    dispatch(env, self, arr, TY_INTEGER, &result);
}

/*
 * Class:     CFunc
 * Method:    find
 * Signature: (Ljava/lang/String;Ljava/lang/String;)J
 */
JNIEXPORT jlong JNICALL Java_jni_CFunc_find
  (JNIEnv *env, jobject self, jstring lib, jstring fun)
{
    void *handle;
    void *func;
    char *libname = 0;
    char *funname = 0;

    if ((libname = getJavaString(env, lib)) == 0) {
        goto ret;
    }
    if ((funname = getJavaString(env, fun)) == 0) {
        goto ret;
    }
    if ((handle = (void *)LOAD_LIBRARY(libname)) == NULL) {
        throwByName(env, "java/lang/UnsatisfiedLinkError", libname);
	goto ret;
    }
    if ((func = (void *)FIND_ENTRY(handle, funname)) == NULL) {
        throwByName(env, "java/lang/UnsatisfiedLinkError", funname);
	goto ret;
    }

 ret:
    free(libname);
    free(funname);
    return (jlong)func;
}


/********************************************************************/
/*		     Native methods of class CPtr		    */
/********************************************************************/

/*
 * Class:     CPtr
 * Method:    initIDs
 * Signature: (LCPtr;)I
 */
JNIEXPORT jint JNICALL 
Java_jni_CPtr_initIDs(JNIEnv *env, jclass cls, jobject nullCPtr)
{
    objectCPtrNULL = (*env)->NewGlobalRef(env, nullCPtr);
    if (objectCPtrNULL == NULL) return 0;

    classString = (*env)->FindClass(env, "java/lang/String");
    if (classString == NULL) return 0;
    classString = (*env)->NewGlobalRef(env, classString);
    if (classString == NULL) return 0;

    classInteger = (*env)->FindClass(env, "java/lang/Integer");
    if (classInteger == NULL) return 0;
    classInteger = (*env)->NewGlobalRef(env, classInteger);
    if (classInteger == NULL) return 0;

    classFloat = (*env)->FindClass(env, "java/lang/Float");
    if (classFloat == NULL) return 0;
    classFloat = (*env)->NewGlobalRef(env, classFloat);
    if (classFloat == NULL) return 0;

    classDouble = (*env)->FindClass(env, "java/lang/Double");
    if (classDouble == NULL) return 0;
    classDouble = (*env)->NewGlobalRef(env, classDouble);
    if (classDouble == NULL) return 0;

    classCPtr = (*env)->NewGlobalRef(env, cls);
    if (classCPtr == NULL) return 0;

    String_getBytes_ID = 
        (*env)->GetMethodID(env, classString, "getBytes", "()[B");
    if (String_getBytes_ID == NULL) return 0;
    String_init_ID = (*env)->GetMethodID(env, classString, "<init>", "([B)V");
    if (String_init_ID == NULL) return 0;

    Integer_value_ID = (*env)->GetFieldID(env, classInteger, "value", "I");
    if (Integer_value_ID == NULL) return 0;

    Float_value_ID = (*env)->GetFieldID(env, classFloat, "value", "F");
    if (Float_value_ID == NULL) return 0;

    Double_value_ID = (*env)->GetFieldID(env, classDouble, "value", "D");
    if (Double_value_ID == NULL) return 0;

    CPtr_peer_ID = (*env)->GetFieldID(env, classCPtr, "peer", "J");
    return sizeof(void *);
}

/*
 * Class:     CPtr
 * Method:    copyIn
 * Signature: (I[BII)V
 */
JNIEXPORT void JNICALL Java_jni_CPtr_copyIn__I_3BII
  (JNIEnv *env, jobject self, jint boff, jbyteArray arr, jint off, jint n)
{
    jbyte *peer = (jbyte *)(*env)->GetLongField(env, self, CPtr_peer_ID);
    (*env)->GetByteArrayRegion(env, arr, off, n, peer + boff);
}

/*
 * Class:     CPtr
 * Method:    copyIn
 * Signature: (I[CII)V
 */
JNIEXPORT void JNICALL Java_jni_CPtr_copyIn__I_3CII
  (JNIEnv *env, jobject self, jint boff, jcharArray arr, jint off, jint n)
{
    jchar *peer = (jchar *)(*env)->GetLongField(env, self, CPtr_peer_ID);
    (*env)->GetCharArrayRegion(env, arr, off, n, peer + boff);
}

/*
 * Class:     CPtr
 * Method:    copyIn
 * Signature: (I[DII)V
 */
JNIEXPORT void JNICALL Java_jni_CPtr_copyIn__I_3DII
  (JNIEnv *env, jobject self, jint boff, jdoubleArray arr, jint off, jint n)
{
    jdouble *peer = (jdouble *)(*env)->GetLongField(env, self, CPtr_peer_ID);
    (*env)->GetDoubleArrayRegion(env, arr, off, n, peer + boff);
}

/*
 * Class:     CPtr
 * Method:    copyIn
 * Signature: (I[FII)V
 */
JNIEXPORT void JNICALL Java_jni_CPtr_copyIn__I_3FII
  (JNIEnv *env, jobject self, jint boff, jfloatArray arr, jint off, jint n)
{
    jfloat *peer = (jfloat *)(*env)->GetLongField(env, self, CPtr_peer_ID);
    (*env)->GetFloatArrayRegion(env, arr, off, n, peer + boff);
}

/*
 * Class:     CPtr
 * Method:    copyIn
 * Signature: (I[III)V
 */
JNIEXPORT void JNICALL Java_jni_CPtr_copyIn__I_3III
  (JNIEnv *env, jobject self, jint boff, jintArray arr, jint off, jint n)
{
    jint *peer = (jint *)(*env)->GetLongField(env, self, CPtr_peer_ID);
    (*env)->GetIntArrayRegion(env, arr, off, n, peer + boff);
}

/*
 * Class:     CPtr
 * Method:    copyIn
 * Signature: (I[JII)V
 */
JNIEXPORT void JNICALL Java_jni_CPtr_copyIn__I_3JII
  (JNIEnv *env, jobject self, jint boff, jlongArray arr, jint off, jint n)
{
    jlong *peer = (jlong *)(*env)->GetLongField(env, self, CPtr_peer_ID);
    (*env)->GetLongArrayRegion(env, arr, off, n, peer + boff);
}

/*
 * Class:     CPtr
 * Method:    copyIn
 * Signature: (I[SII)V
 */
JNIEXPORT void JNICALL Java_jni_CPtr_copyIn__I_3SII
  (JNIEnv *env, jobject self, jint boff, jshortArray arr, jint off, jint n)
{
    jshort *peer = (jshort *)(*env)->GetLongField(env, self, CPtr_peer_ID);
    (*env)->GetShortArrayRegion(env, arr, off, n, peer + boff);
}

/*
 * Class:     CPtr
 * Method:    copyOut
 * Signature: (I[BII)V
 */
JNIEXPORT void JNICALL Java_jni_CPtr_copyOut__I_3BII
  (JNIEnv *env, jobject self, jint boff, jbyteArray arr, jint off, jint n)
{
    jbyte *peer = (jbyte *)(*env)->GetLongField(env, self, CPtr_peer_ID);
    (*env)->SetByteArrayRegion(env, arr, off, n, peer + boff);
}

/*
 * Class:     CPtr
 * Method:    copyOut
 * Signature: (I[CII)V
 */
JNIEXPORT void JNICALL Java_jni_CPtr_copyOut__I_3CII
  (JNIEnv *env, jobject self, jint boff, jcharArray arr, jint off, jint n)
{
    jchar *peer = (jchar *)(*env)->GetLongField(env, self, CPtr_peer_ID);
    (*env)->SetCharArrayRegion(env, arr, off, n, peer + boff);
}

/*
 * Class:     CPtr
 * Method:    copyOut
 * Signature: (I[DII)V
 */
JNIEXPORT void JNICALL Java_jni_CPtr_copyOut__I_3DII
  (JNIEnv *env, jobject self, jint boff, jdoubleArray arr, jint off, jint n)
{
    jdouble *peer = (jdouble *)(*env)->GetLongField(env, self, CPtr_peer_ID);
    (*env)->SetDoubleArrayRegion(env, arr, off, n, peer + boff);
}

/*
 * Class:     CPtr
 * Method:    copyOut
 * Signature: (I[FII)V
 */
JNIEXPORT void JNICALL Java_jni_CPtr_copyOut__I_3FII
  (JNIEnv *env, jobject self, jint boff, jfloatArray arr, jint off, jint n)
{
    jfloat *peer = (jfloat *)(*env)->GetLongField(env, self, CPtr_peer_ID);
    (*env)->SetFloatArrayRegion(env, arr, off, n, peer + boff);
}

/*
 * Class:     CPtr
 * Method:    copyOut
 * Signature: (I[III)V
 */
JNIEXPORT void JNICALL Java_jni_CPtr_copyOut__I_3III
  (JNIEnv *env, jobject self, jint boff, jintArray arr, jint off, jint n)
{
    jint *peer = (jint *)(*env)->GetLongField(env, self, CPtr_peer_ID);
    (*env)->SetIntArrayRegion(env, arr, off, n, peer + boff);
}

/*
 * Class:     CPtr
 * Method:    copyOut
 * Signature: (I[JII)V
 */
JNIEXPORT void JNICALL Java_jni_CPtr_copyOut__I_3JII
  (JNIEnv *env, jobject self, jint boff, jlongArray arr, jint off, jint n)
{
    jlong *peer = (jlong *)(*env)->GetLongField(env, self, CPtr_peer_ID);
    (*env)->SetLongArrayRegion(env, arr, off, n, peer + boff);
}

/*
 * Class:     CPtr
 * Method:    copyOut
 * Signature: (I[SII)V
 */
JNIEXPORT void JNICALL Java_jni_CPtr_copyOut__I_3SII
  (JNIEnv *env, jobject self, jint boff, jshortArray arr, jint off, jint n)
{
    jshort *peer = (jshort *)(*env)->GetLongField(env, self, CPtr_peer_ID);
    (*env)->SetShortArrayRegion(env, arr, off, n, peer + boff);
}

/*
 * Class:     CPtr
 * Method:    getByte
 * Signature: (I)B
 */
JNIEXPORT jbyte JNICALL Java_jni_CPtr_getByte
  (JNIEnv *env, jobject self, jint index)
{
    jbyte res;
    jbyte *peer = (jbyte *)(*env)->GetLongField(env, self, CPtr_peer_ID);
    memcpy(&res, peer + index, sizeof(res));
    return res;
}

/*
 * Class:     CPtr
 * Method:    getCPtr
 * Signature: (I)LCPtr;
 */
JNIEXPORT jobject JNICALL Java_jni_CPtr_getCPtr
  (JNIEnv *env, jobject self, jint index)
{
    void *ptr;
    jbyte *peer = (jbyte *)(*env)->GetLongField(env, self, CPtr_peer_ID);
    memcpy(&ptr, peer + index, sizeof(ptr));
    return makeCPtr(env, ptr);
}

/*
 * Class:     CPtr
 * Method:    getDouble
 * Signature: (I)D
 */
JNIEXPORT jdouble JNICALL Java_jni_CPtr_getDouble
  (JNIEnv *env, jobject self, jint index)
{
    jdouble res;
    jbyte *peer = (jbyte *)(*env)->GetLongField(env, self, CPtr_peer_ID);
    memcpy(&res, peer + index, sizeof(res));
    return res;
}

/*
 * Class:     CPtr
 * Method:    getFloat
 * Signature: (I)F
 */
JNIEXPORT jfloat JNICALL Java_jni_CPtr_getFloat
  (JNIEnv *env, jobject self, jint index)
{
    jfloat res;
    jbyte *peer = (jbyte *)(*env)->GetLongField(env, self, CPtr_peer_ID);
    memcpy(&res, peer + index, sizeof(res));
    return res;
}

/*
 * Class:     CPtr
 * Method:    getInt
 * Signature: (I)I
 */
JNIEXPORT jint JNICALL Java_jni_CPtr_getInt
  (JNIEnv *env, jobject self, jint index)
{
    jint res;
    jbyte *peer = (jbyte *)(*env)->GetLongField(env, self, CPtr_peer_ID);
    memcpy(&res, peer + index, sizeof(res));
    return res;
}

/*
 * Class:     CPtr
 * Method:    getLong
 * Signature: (I)J
 */
JNIEXPORT jlong JNICALL Java_jni_CPtr_getLong
  (JNIEnv *env, jobject self, jint index)
{
    jlong res;
    jbyte *peer = (jbyte *)(*env)->GetLongField(env, self, CPtr_peer_ID);
    memcpy(&res, peer + index, sizeof(res));
    return res;
}

/*
 * Class:     CPtr
 * Method:    getShort
 * Signature: (I)S
 */
JNIEXPORT jshort JNICALL Java_jni_CPtr_getShort
  (JNIEnv *env, jobject self, jint index)
{
    jshort res;
    jbyte *peer = (jbyte *)(*env)->GetLongField(env, self, CPtr_peer_ID);
    memcpy(&res, peer + index, sizeof(res));
    return res;
}

/*
 * Class:     CPtr
 * Method:    getString
 * Signature: (I)Ljava/lang/String;
 */
JNIEXPORT jstring JNICALL Java_jni_CPtr_getString
  (JNIEnv *env, jobject self, jint index)
{
    jbyte *peer = (jbyte *)(*env)->GetLongField(env, self, CPtr_peer_ID);
    return newJavaString(env, (const char *)peer + index);
}

/*
 * Class:     CPtr
 * Method:    setByte
 * Signature: (IB)V
 */
JNIEXPORT void JNICALL Java_jni_CPtr_setByte
  (JNIEnv *env, jobject self, jint index, jbyte value)
{
    jbyte *peer = (jbyte *)(*env)->GetLongField(env, self, CPtr_peer_ID);
    memcpy(peer + index, &value, sizeof(value));
}

/*
 * Class:     CPtr
 * Method:    setCPtr
 * Signature: (ILCPtr;)V
 */
JNIEXPORT void JNICALL Java_jni_CPtr_setCPtr
  (JNIEnv *env, jobject self, jint index, jobject cptr)
{
    void *ptr = (void *)(*env)->GetLongField(env, cptr, CPtr_peer_ID);
    jbyte *peer = (jbyte *)(*env)->GetLongField(env, self, CPtr_peer_ID);
    memcpy(peer + index, &ptr, sizeof(ptr));
}

/*
 * Class:     CPtr
 * Method:    setDouble
 * Signature: (ID)V
 */
JNIEXPORT void JNICALL Java_jni_CPtr_setDouble
  (JNIEnv *env, jobject self, jint index, jdouble value)
{
    jbyte *peer = (jbyte *)(*env)->GetLongField(env, self, CPtr_peer_ID);
    memcpy(peer + index, &value, sizeof(value));
}

/*
 * Class:     CPtr
 * Method:    setFloat
 * Signature: (IF)V
 */
JNIEXPORT void JNICALL Java_jni_CPtr_setFloat
  (JNIEnv *env, jobject self, jint index, jfloat value)
{
    jbyte *peer = (jbyte *)(*env)->GetLongField(env, self, CPtr_peer_ID);
    memcpy(peer + index, &value, sizeof(value));
}

/*
 * Class:     CPtr
 * Method:    setInt
 * Signature: (II)V
 */
JNIEXPORT void JNICALL Java_jni_CPtr_setInt
  (JNIEnv *env, jobject self, jint index, jint value)
{
    jbyte *peer = (jbyte *)(*env)->GetLongField(env, self, CPtr_peer_ID);
    memcpy(peer + index, &value, sizeof(value));
}

/*
 * Class:     CPtr
 * Method:    setLong
 * Signature: (IJ)V
 */
JNIEXPORT void JNICALL Java_jni_CPtr_setLong
  (JNIEnv *env, jobject self, jint index, jlong value)
{
    jbyte *peer = (jbyte *)(*env)->GetLongField(env, self, CPtr_peer_ID);
    memcpy(peer + index, &value, sizeof(value));
}

/*
 * Class:     CPtr
 * Method:    setShort
 * Signature: (IS)V
 */
JNIEXPORT void JNICALL Java_jni_CPtr_setShort
  (JNIEnv *env, jobject self, jint index, jshort value)
{
    jbyte *peer = (jbyte *)(*env)->GetLongField(env, self, CPtr_peer_ID);
    memcpy(peer + index, &value, sizeof(value));
}

/*
 * Class:     CPtr
 * Method:    setString
 * Signature: (ILjava/lang/String;)V
 */
JNIEXPORT void JNICALL Java_jni_CPtr_setString
  (JNIEnv *env, jobject self, jint index, jstring value)
{
    jbyte *peer = (jbyte *)(*env)->GetLongField(env, self, CPtr_peer_ID);
    char *str = getJavaString(env, value);
    if (str == NULL) return;
    strcpy((char *)peer + index, str);
    free(str);
}


/********************************************************************/
/*		     Native methods of class CMalloc		    */
/********************************************************************/

/*
 * Class:     CMalloc
 * Method:    malloc
 * Signature: (I)J
 */
JNIEXPORT jlong JNICALL Java_jni_CMalloc_malloc
  (JNIEnv *env, jclass cls, jint size)
{
    return (jlong)malloc(size);
}

/*
 * Class:     CMalloc
 * Method:    free
 * Signature: (J)V
 */
JNIEXPORT void JNICALL Java_jni_CMalloc_free
  (JNIEnv *env, jclass cls, jlong ptr)
{
    free((void *)ptr);
}


/********************************************************************/
/*			   Utility functions			    */
/********************************************************************/

/* Throw an exception by name */
static void 
throwByName(JNIEnv *env, const char *name, const char *msg)
{
    jclass cls = (*env)->FindClass(env, name);

    if (cls != 0) /* Otherwise an exception has already been thrown */
        (*env)->ThrowNew(env, cls, msg);

    /* It's a good practice to clean up the local references. */
    (*env)->DeleteLocalRef(env, cls);
}

/* Translates a Java string to a C string using the String.getBytes 
 * method, which uses default local encoding.
 */
static char *
getJavaString(JNIEnv *env, jstring jstr)
{
    jbyteArray hab = 0;
    jthrowable exc;
    char *result = 0;

    hab = (*env)->CallObjectMethod(env, jstr, String_getBytes_ID);
    exc = (*env)->ExceptionOccurred(env);
    if (!exc) {
        jint len = (*env)->GetArrayLength(env, hab);
        result = (char *)malloc(len + 1);
	if (result == 0) {
	    throwByName(env, "java/lang/OutOfMemoryError", 0);
	    (*env)->DeleteLocalRef(env, hab);
	    return 0;
	}
	(*env)->GetByteArrayRegion(env, hab, 0, len, (jbyte *)result);
	result[len] = 0; /* NULL-terminate */
    } else {
        (*env)->DeleteLocalRef(env, exc);
    }
    (*env)->DeleteLocalRef(env, hab);
    return result;
}

/* Constructs a Java string from a C array using the String(byte [])
 * constructor, which uses default local encoding.
 */
static jstring
newJavaString(JNIEnv *env, const char *str)
{
    jstring result;
    jbyteArray hab = 0;
    int len;

    len = strlen(str);
    hab = (*env)->NewByteArray(env, len);
    if (hab != 0) {
        (*env)->SetByteArrayRegion(env, hab, 0, len, (jbyte *)str);
	result = (*env)->NewObject(env, classString,
				   String_init_ID, hab);
	(*env)->DeleteLocalRef(env, hab);
	return result;
    }
    return 0;
}

/* Canonicalize NULL pointers */
static jobject makeCPtr(JNIEnv *env, void *p)
{
    jobject obj;
    if (p == NULL) {
        return objectCPtrNULL;
    }
    obj = (*env)->AllocObject(env, classCPtr);
    if (obj) {
        (*env)->SetLongField(env, obj, CPtr_peer_ID, (jlong)p);
    }
    return obj;
}

/*
 * Class:     jni_CMalloc
 * Method:    sizeof_jint
 * Signature: ()I
 */
JNIEXPORT jint JNICALL Java_jni_CMalloc_sizeof_1jint
  (JNIEnv * e, jclass c)
{
    return sizeof(jint);
}

/*
 * Class:     jni_CMalloc
 * Method:    sizeof_jbyte
 * Signature: ()I
 */
JNIEXPORT jint JNICALL Java_jni_CMalloc_sizeof_1jbyte
  (JNIEnv * e, jclass c)
{
    return sizeof(jbyte);
}

/*
 * Class:     jni_CMalloc
 * Method:    sizeof_jlong
 * Signature: ()I
 */
JNIEXPORT jint JNICALL Java_jni_CMalloc_sizeof_1jlong
  (JNIEnv * e, jclass c) 
{
    return sizeof(jlong);
}
