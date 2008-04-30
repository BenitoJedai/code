using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.flash.media
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/media/Camera.html#getCamera()
    [Script(IsNative = true)]
    public class Camera : EventDispatcher
    {
        #region Methods
        /// <summary>
        /// [static] Returns a reference to a Camera object for capturing video.
        /// </summary>
        public static Camera getCamera(string name)
        {
            return default(Camera);
        }

        /// <summary>
        /// [static] Returns a reference to a Camera object for capturing video.
        /// </summary>
        public static Camera getCamera()
        {
            return default(Camera);
        }

        /// <summary>
        /// Specifies which video frames are transmitted in full (called keyframes) instead of being interpolated by the video compression algorithm.
        /// </summary>
        public void setKeyFrameInterval(int keyFrameInterval)
        {
        }

        /// <summary>
        /// Specifies whether to use a compressed video stream for a local view of the camera.
        /// </summary>
        public void setLoopback(bool compress)
        {
        }

        /// <summary>
        /// Specifies whether to use a compressed video stream for a local view of the camera.
        /// </summary>
        public void setLoopback()
        {
        }

        /// <summary>
        /// Sets the camera capture mode to the native mode that best meets the specified requirements.
        /// </summary>
        public void setMode(int width, int height, double fps, bool favorArea)
        {
        }

        /// <summary>
        /// Sets the camera capture mode to the native mode that best meets the specified requirements.
        /// </summary>
        public void setMode(int width, int height, double fps)
        {
        }

        /// <summary>
        /// Specifies how much motion is required to dispatch the activity event.
        /// </summary>
        public void setMotionLevel(int motionLevel, int timeout)
        {
        }

        /// <summary>
        /// Specifies how much motion is required to dispatch the activity event.
        /// </summary>
        public void setMotionLevel(int motionLevel)
        {
        }

        /// <summary>
        /// Sets the maximum amount of bandwidth per second or the required picture quality of the current outgoing video feed.
        /// </summary>
        public void setQuality(int bandwidth, int quality)
        {
        }

        #endregion

        #region Constructors
        #endregion



        #region Properties
        /// <summary>
        /// [read-only] The amount of motion the camera is detecting.
        /// </summary>
        public double activityLevel { get; private set; }

        /// <summary>
        /// [read-only] The maximum amount of bandwidth the current outgoing video feed can use, in bytes.
        /// </summary>
        public int bandwidth { get; private set; }

        /// <summary>
        /// [read-only] The rate at which the camera is capturing data, in frames per second.
        /// </summary>
        public double currentFPS { get; private set; }

        /// <summary>
        /// [read-only] The maximum rate at which the camera can capture data, in frames per second.
        /// </summary>
        public double fps { get; private set; }

        /// <summary>
        /// [read-only] The current capture height, in pixels.
        /// </summary>
        public int height { get; private set; }

        /// <summary>
        /// [read-only] A zero-based integer that specifies the index of the camera, as reflected in the array returned by the names property.
        /// </summary>
        public int index { get; private set; }

        /// <summary>
        /// [read-only] The number of video frames transmitted in full (called keyframes) instead of being interpolated by the video compression algorithm.
        /// </summary>
        public int keyFrameInterval { get; private set; }

        /// <summary>
        /// [read-only] Indicates whether a local view of what the camera is capturing is compressed and decompressed (true), as it would be for live transmission using Flash Media Server, or uncompressed (false).
        /// </summary>
        public bool loopback { get; private set; }

        /// <summary>
        /// [read-only] The amount of motion required to invoke the activity event.
        /// </summary>
        public int motionLevel { get; private set; }

        /// <summary>
        /// [read-only] The number of milliseconds between the time the camera stops detecting motion and the time the activity event is invoked.
        /// </summary>
        public int motionTimeout { get; private set; }

        /// <summary>
        /// [read-only] A Boolean value indicating whether the user has denied access to the camera (true) or allowed access (false) in the Flash Player Privacy dialog box.
        /// </summary>
        public bool muted { get; private set; }

        /// <summary>
        /// [read-only] The name of the current camera, as returned by the camera hardware.
        /// </summary>
        public string name { get; private set; }

        /// <summary>
        /// [static] [read-only] An array of strings indicating the names of all available cameras without displaying the Flash Player Privacy dialog box.
        /// </summary>
        public static string[] names { get; private set; }

        /// <summary>
        /// [read-only] The required level of picture quality, as determined by the amount of compression being applied to each video frame.
        /// </summary>
        public int quality { get; private set; }

        /// <summary>
        /// [read-only] The current capture width, in pixels.
        /// </summary>
        public int width { get; private set; }

        #endregion


        #region Events
        /// <summary>
        /// Dispatched when a camera begins or ends a session.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<ActivityEvent> activity;

        /// <summary>
        /// Dispatched when a camera reports its status.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<StatusEvent> status;

        #endregion

   
    }
}
