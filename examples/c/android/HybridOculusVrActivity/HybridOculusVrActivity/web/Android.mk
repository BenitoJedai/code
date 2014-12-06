# Copyright (C) 2010 The Android Open Source Project
#
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
#
#      http://www.apache.org/licenses/LICENSE-2.0
#
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
#
LOCAL_PATH := $(call my-dir)

include $(CLEAR_VARS)

include import_vrlib.mk		# import VRLib for this module.  Do NOT call $(CLEAR_VARS) until after building your module.
										# use += instead of := when defining the following variables: LOCAL_LDLIBS, LOCAL_CFLAGS, LOCAL_C_INCLUDES, LOCAL_STATIC_LIBRARIES 

#Android NDK: ERROR:jni/Android.mk:oculus: LOCAL_SRC_FILES points to a missing file
#Android NDK: Check that ./obj/local/armeabi-v7a/liboculus.a exists  or that its path is correct


LOCAL_MODULE    := HybridOculusVrActivity
#LOCAL_SRC_FILES := OvrApp.cpp
LOCAL_SRC_FILES := HybridOculusVrActivity.dll.c
LOCAL_LDLIBS    := -llog -landroid -lEGL -lGLESv1_CM -lGLESv2
    

LOCAL_STATIC_LIBRARIES := android_native_app_glue

include $(BUILD_SHARED_LIBRARY)

$(call import-module,android/native_app_glue)
