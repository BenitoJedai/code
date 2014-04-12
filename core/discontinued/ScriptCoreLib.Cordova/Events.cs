using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.Shared.Drawing;
using System;

using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.Cordova
{
    /// <summary>
    /// The device object describes the device's hardware and software
    /// http://docs.phonegap.com/en/1.7.0/cordova_device_device.md.html#Device
    /// </summary>
    [Script(IsNative = true)]
    public class Events : ISink
    {
        #region Constructor

        public Events()
        {

        }

        #endregion

        #region Events

        #region event deviceready
        public event System.Action<IEvent> deviceready
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "deviceready");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "deviceready");
            }
        }
        #endregion


        #region event pause
        public event System.Action<IEvent> pause
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "pause");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "pause");
            }
        }
        #endregion


        #region event resume
        public event System.Action<IEvent> resume
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "resume");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "resume");
            }
        }
        #endregion


        #region event online
        public event System.Action<IEvent> online
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "online");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "online");
            }
        }
        #endregion


        #region event offline
        public event System.Action<IEvent> offline
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "offline");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "offline");
            }
        }
        #endregion



        #region event backbutton
        public event System.Action<IEvent> backbutton
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "backbutton");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "backbutton");
            }
        }
        #endregion


        #region event batterycritical
        public event System.Action<IEvent> batterycritical
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "batterycritical");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "batterycritical");
            }
        }
        #endregion


        #region event batterylow
        public event System.Action<IEvent> batterylow
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "batterylow");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "batterylow");
            }
        }
        #endregion



        #region event batterystatus
        public event System.Action<IEvent> batterystatus
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "batterystatus");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "batterystatus");
            }
        }
        #endregion


        #region event menubutton
        public event System.Action<IEvent> menubutton
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "menubutton");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "menubutton");
            }
        }
        #endregion

        #region event searchbutton
        public event System.Action<IEvent> searchbutton
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "searchbutton");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "searchbutton");
            }
        }
        #endregion

        #region event startcallbutton
        public event System.Action<IEvent> startcallbutton
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "startcallbutton");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "startcallbutton");
            }
        }
        #endregion

        #region event endcallbutton
        public event System.Action<IEvent> endcallbutton
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "endcallbutton");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "endcallbutton");
            }
        }
        #endregion

        #region event volumedownbutton
        public event System.Action<IEvent> volumedownbutton
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "volumedownbutton");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "volumedownbutton");
            }
        }
        #endregion

        #region event volumeupbutton
        public event System.Action<IEvent> volumeupbutton
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "volumeupbutton");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "volumeupbutton");
            }
        }
        #endregion

        #endregion
      
    }
}
