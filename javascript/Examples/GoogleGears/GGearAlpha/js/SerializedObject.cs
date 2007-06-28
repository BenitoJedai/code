using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace GGearAlpha.js
{
    [Script]
    public interface ISerializedObject
    {
        // if a type has an interface attached then we can work around the System.Activator
        // bug where inheritance is currently not respected

        string VirtualId { get; set; }

    }

    [Script]
    public abstract class SerializedObject : ISerializedObject
    {
        public virtual  string VirtualId
        {
            set { }
            get { return null; }
        }


        public SerializedObject()
        {
            this.VirtualId = "$id+" + new System.Random().NextDouble();

        }
    }
}
