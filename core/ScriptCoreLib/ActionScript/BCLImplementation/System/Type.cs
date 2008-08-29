using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Reflection;
using ScriptCoreLib.ActionScript.Extensions;
using System.Reflection;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [ScriptImportsType("flash.utils.describeType")]
    [ScriptImportsType("flash.utils.getDefinitionByName")]
    [Script(Implements = typeof(global::System.Type))]
    internal class __Type : __MemberInfo
    {
		public static implicit operator __Type(Type e)
		{
			return (__Type)(object)e;
		}

        RuntimeTypeHandle _TypeHandle;

        public static Type GetType(string x)
        {
            var e = new __Type
            {
                _TypeDescription = describeType(getDefinitionByName(x))
            };

            return (Type)(object)e;
        }

        internal static Type GetTypeFromValue(object x)
        {
            var e = new __Type
            {
                _TypeDescription = describeType(x)
            };

            return (Type)(object)e;
        }

        public static Type GetTypeFromHandle(RuntimeTypeHandle TypeHandle)
        {
            var e = new __Type
            {
                _TypeHandle = TypeHandle
            };

            return (Type)(object)e;
        }

        XML _TypeDescription;
        //<type name="FlashXMLExample.ActionScript.Serialized::MyDataClass" base="Class" isDynamic="true" isFinal="true" isStatic="true">
        //  <extendsClass type="Class"/>
        //  <extendsClass type="Object"/>
        //  <accessor name="prototype" access="readonly" type="*" declaredBy="Class"/>
        //  <factory type="FlashXMLExample.ActionScript.Serialized::MyDataClass">
        //    <extendsClass type="Object"/>
        //    <variable name="Value" type="int"/>
        //    <variable name="Text" type="String"/>
        //    <variable name="Data" type="FlashXMLExample.ActionScript.Serialized2::MyDataClass"/>
        //  </factory>
        //</type>

        //<type name="FlashXMLExample.ActionScript.Serialized::MyDataClass" base="FlashXMLExample.ActionScript::MyDataClassCommon" isDynamic="false" isFinal="true" isStatic="false">
        //  <extendsClass type="FlashXMLExample.ActionScript::MyDataClassCommon"/>
        //  <extendsClass type="Object"/>
        //  <variable name="Value" type="int"/>
        //  <variable name="Data" type="FlashXMLExample.ActionScript.Serialized2::MyDataClass"/>
        //  <variable name="Text" type="String"/>
        //</type>

        public XML TypeDescription
        {
            get
            {
                if (_TypeDescription == null)
                    _TypeDescription = describeType(this._TypeHandle.Value.ToClassToken());

                return _TypeDescription;
            }
        }

        // http://livedocs.adobe.com/flex/2/langref/flash/utils/package.html#describeType()
        [Script(OptimizedCode = "return flash.utils.describeType(e);")]
        internal static XML describeType(object e)
        {
            return default(XML);
        }

        // http://livedocs.adobe.com/flex/2/langref/flash/utils/package.html#getDefinitionByName()
        [Script(OptimizedCode = "return flash.utils.getDefinitionByName(e);")]
        public static object getDefinitionByName(string e)
        {
            return default(object);
        }

        public override string ToString()
        {
            return TypeDescription.ToString();
        }

        public string InternalFullName
        {
            get
            {
                return TypeDescription.attribute("name").ToString();
            }
        }

        public __FieldInfo[] GetFields()
        {
            // fixme: at this time also inherited and private fields are show

            var x = this.TypeDescription;

            var a = new List<__FieldInfo>();

            var v = x.child("variable");
            for (int i = 0; i < v.length(); i++)
            {
                a.Add(new __FieldInfo { _FieldDescription = v[i] });
            }

            var f = x.child("factory");

            // e.GetType() has no <factory />
            if (f.length() == 1)
            {
                x = this.TypeDescription;
                v = x.child("variable");
                for (int i = 0; i < v.length(); i++)
                {
                    a.Add(new __FieldInfo { _FieldDescription = v[i] });
                }
            }

            return a.ToArray();
        }

        public override string Name
        {
            get
            {
                var v = InternalFullName;
                var z = "::";
                var i = v.IndexOf(z);

                if (i < 0)
                    return v; // Top Level Name


                return v.Substring(i + z.Length);
            }
        }

        public string FullName
        {
            get
            {
                // fixme: should return .net styled names
                return InternalFullName;
            }
        }

        public bool Equals(__Type e)
        {
            return this.InternalFullName == e.InternalFullName;

            //return e.TypeDescription == this.TypeDescription;
        }

    }
}
