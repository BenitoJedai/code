using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.google.appengine.api.utils
{
    [Script(IsNative = true)]
    public class SystemProperty
    {
        // tested by ?

        public static SystemProperty applicationId;
        public static SystemProperty applicationVersion;
        //public static SystemProperty.Environment environment;
        public static SystemProperty instanceReplicaId;
        public static SystemProperty version;

        public SystemProperty()
        {

        }

        public virtual string get()
        {
            return default(string);
        }
        public virtual string key()
        {
            return default(string);
        }

        public virtual void set(string value)
        {
        }

        //[Script(IsNative = true)]
        //public class Environment : SystemProperty
        //{
        //    protected internal Environment();

        //    public virtual void set(SystemProperty.Environment.Value value);
        //    public virtual SystemProperty.Environment.Value value();

        //    //[Script(IsNative = true)]
        //    //public sealed class Value : Enum
        //    //{
        //    //    public static SystemProperty.Environment.Value Development;
        //    //    public static SystemProperty.Environment.Value Production;

        //    //    protected internal Value();

        //    //    public string value();
        //    //    public static SystemProperty.Environment.Value valueOf(string value);
        //    //    public static SystemProperty.Environment.Value[] values();
        //    //}
        //}
    }
}
