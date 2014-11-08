using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Reflection;
using ScriptCoreLib.ActionScript.Extensions;
using System.Reflection;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Type.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Type.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Type.cs
    // http://sourceforge.net/p/jsc/code/HEAD/tree/core/ScriptCoreLib/JavaScript/BCLImplementation/System/Type.cs
    // http://referencesource.microsoft.com/#mscorlib/system/type.cs
    // https://github.com/mono/mono/tree/master/mcs/class/corlib/System/Type.cs

    [ScriptImportsType("flash.utils.describeType")]
    [ScriptImportsType("flash.utils.getDefinitionByName")]
    [Script(Implements = typeof(global::System.Type))]
    internal class __Type : __MemberInfo
    {
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Type.cs

        public static implicit operator __Type(Type e)
        {
            return (__Type)(object)e;
        }

        RuntimeTypeHandle _TypeHandle;

        public static Type GetType(string x)
        {
            // tested by?
            // X:\jsc.svn\examples\actionscript\Test\TestThreadStart\TestThreadStart\ApplicationSprite.cs


            var e = new __Type
            {
                InternalTypeDescription = describeType(getDefinitionByName(x))
            };

            return (Type)(object)e;
        }

        internal static Type GetTypeFromValue(object x)
        {
            var e = new __Type
            {
                InternalTypeDescription = describeType(x)
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

        XML InternalTypeDescription;
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
                if (InternalTypeDescription == null)
                    InternalTypeDescription = describeType(this._TypeHandle.Value.ToClassToken());

                return InternalTypeDescription;
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
            // X:\jsc.svn\examples\actionscript\Test\TestThreadStart\TestThreadStart\ApplicationSprite.cs

            return "Type " + new { TypeDescription };
        }

        public string InternalFullName
        {
            get
            {
                return TypeDescription.attribute("name").ToString();
            }
        }

        //<method name="start" declaredBy="com.lytro::Player" returnType="void"/>

        #region GetFields
        public __FieldInfo GetField(string n)
        {
            // X:\jsc.svn\examples\actionscript\Test\TestFieldExpression\TestFieldExpression\ApplicationSprite.cs

            var f = default(__FieldInfo);

            foreach (var k in GetFields())
            {
                if (k.Name == n)
                {
                    f = k;
                    break;
                }
            }

            return f;
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
                //x = this.TypeDescription;
                v = f.child("variable");
                for (int i = 0; i < v.length(); i++)
                {
                    a.Add(new __FieldInfo { _FieldDescription = v[i] });
                }
            }

            return a.ToArray();
        }
        #endregion

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


        public string Namespace
        {
            get
            {
                var v = InternalFullName;
                var z = "::";
                var i = v.IndexOf(z);

                if (i < 0)
                    return "";

                return v.Substring(0, i);
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

        public static bool operator !=(__Type left, __Type right)
        {
            return !InternalEquals(left, right);
        }

        public static bool operator ==(__Type left, __Type right)
        {
            return InternalEquals(left, right);
        }

        public bool Equals(__Type e)
        {
            return InternalEquals(this, e);
        }

        private static bool InternalEquals(__Type x, __Type e)
        {
            return x.InternalFullName == e.InternalFullName;
        }

        public Type BaseType
        {
            get
            {

                var n = default(Type);

                //<type name="FlashXMLExample.ActionScript.Serialized::MyDataClass" base="FlashXMLExample.ActionScript::MyDataClassCommon" isDynamic="false" isFinal="true" isStatic="false">
                //  <extendsClass type="FlashXMLExample.ActionScript::MyDataClassCommon"/>

                var extendsClass = this.InternalTypeDescription.elements("extendsClass");

                if (extendsClass.length() > 0)
                {
                    var x = extendsClass[0];
                    var type = x["@type"];

                    n = GetType(type.ToString());
                }

                return n;
            }
        }

        public override Type DeclaringType
        {
            get { throw new NotImplementedException(); }
        }
    }
}
