using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Cordova;

[assembly: Script]
[assembly: ScriptTypeFilter(ScriptType.JavaScript)]

namespace CordovaHelloWorld
{
    [Script]
    public class Class1
    {
        public Accelerometer accel = new Accelerometer();

        public Class1()
        {
            Action<Acceleration> onAccelAction = onAccelCB;
            Action onAccelErrorAction = onAccelErrorCB;


            //this.accel = CordovaNative.Window.navigator.accelerometer;

            if (this.accel == null)
            {
                //this.ta.innerText = "Accelerometer was null";
            }
            else
                this.accel.getCurrentAcceleration(onAccelAction, onAccelErrorAction);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="acc"></param>
        void onAccelCB(Acceleration acc)
        {
            //this.ta.innerText = "x=" + acc.x + "y=" + acc.y + "z=" + acc.z;
        }


        /// <summary>
        /// 
        /// </summary>
        void onAccelErrorCB()
        {
            //this.ta.innerText = "Accel Error";

        }
    }
}
