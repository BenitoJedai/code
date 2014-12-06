/************************************************************************************

Filename    :   OvrApp.cpp
Content     :   
Created     :   
Authors     :   

Copyright   :   Copyright 2014 Oculus VR, LLC. All Rights reserved.


*************************************************************************************/
#include <jni.h>

#include "OvrApp.h"

char* cxxGetString()
{
	// http://en.wikibooks.org/wiki/GCC_Debugging/g%2B%2B/Warnings/deprecated_conversion_from_string_constant

	//return "from cxxGetString";
	return  (char *) "from cxxGetString";
}

jlong cxxSetAppInterface(JNIEnv *jni, jclass clazz, jobject activity )
{
       LOG( "cxxSetAppInterface");
       return (new OvrApp())->SetActivity( jni, clazz, activity );
}





OvrApp::OvrApp()
{
}

OvrApp::~OvrApp()
{
}

void OvrApp::OneTimeInit( const char * launchIntent )
{
	// This is called by the VR thread, not the java UI thread.
	MaterialParms materialParms;
	materialParms.UseSrgbTextureFormats = false;
	Scene.LoadWorldModel( "/sdcard/oculus/tuscany.ovrscene", materialParms );
}

void OvrApp::OneTimeShutdown()
{
	// Free GL resources
}

void OvrApp::Command( const char * msg )
{
}

Matrix4f OvrApp::DrawEyeView( const int eye, const float fovDegrees )
{
	const Matrix4f view = Scene.DrawEyeView( eye, fovDegrees );

	return view;
}

Matrix4f OvrApp::Frame(const VrFrame vrFrame)
{
	// Player movement
    Scene.Frame( app->GetVrViewParms(), vrFrame, app->GetSwapParms().ExternalVelocity );

	app->DrawEyeViewsPostDistorted( Scene.CenterViewMatrix() );

	return Scene.CenterViewMatrix();
}

