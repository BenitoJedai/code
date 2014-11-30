/* prevalidated at 2014-11-30 11:40:22 AM */
#include "TestNDK.dll.h"
void* TestNDK_xNativeActivity_CS___9__CachedAnonymousMethodDelegate1;


// instance TestNDK.xNativeActivity..ctor
LPTestNDK_xNativeActivity TestNDK_xNativeActivity__ctor_6000002(LPTestNDK_xNativeActivity __that)
{
    return __that;
}

void android_main(struct android_app* state)
{
    void* android_app0;

    android_app0 = ((void*)state);
    app_dummy();
    __android_log_print((int)4, (char*)"xNativeActivity", (char*)"enter TestNDK");
    state->userData = NULL;

    if ((TestNDK_xNativeActivity_CS___9__CachedAnonymousMethodDelegate1 == NULL))
    {
        TestNDK_xNativeActivity_CS___9__CachedAnonymousMethodDelegate1 = TestNDK_xNativeActivity__android_main_b__0;
    }

    state->onAppCmd = TestNDK_xNativeActivity_CS___9__CachedAnonymousMethodDelegate1;
    __android_log_print((int)4, (char*)"xNativeActivity", (char*)"exit TestNDK");
}

void TestNDK_xNativeActivity__android_main_b__0(void* app, int cmd)
{
    __android_log_print((int)0, (char*)"xNativeActivity", (char*)"onAppCmd");

    if ((cmd == 1))
    {
        __android_log_print((int)0, (char*)"xNativeActivity", (char*)"APP_CMD_INIT_WINDOW");
    }

}

