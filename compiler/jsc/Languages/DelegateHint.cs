using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace jsc.Languages
{
    static class DelegateHint
    {
        static IEnumerable<KeyValuePair<ScriptCoreLib.ScriptDelegateDataHintAttribute, FieldInfo>> ToArray(Type e)
        {

            foreach (FieldInfo v in e.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
            {
                object[] vhint = v.GetCustomAttributes(typeof(ScriptCoreLib.ScriptDelegateDataHintAttribute), false);

                if (vhint.Length == 1)
                {
                    yield return new KeyValuePair<ScriptCoreLib.ScriptDelegateDataHintAttribute, FieldInfo>((ScriptCoreLib.ScriptDelegateDataHintAttribute)vhint[0], v);
                }
            }
        }


        public static void Resolve(Type MulticastDelegate, Type Delegate, 
            out System.Reflection.FieldInfo FieldList, 
            out System.Reflection.FieldInfo FieldTarget, 
            out System.Reflection.FieldInfo FieldMethod)
        {
            FieldList = null;
            FieldTarget = null;
            FieldMethod = null;

            foreach (KeyValuePair<ScriptCoreLib.ScriptDelegateDataHintAttribute, FieldInfo> v in ToArray(MulticastDelegate))
            {
                if (v.Key.Value == ScriptCoreLib.ScriptDelegateDataHintAttribute.FieldType.List)
                    FieldList = v.Value;
            }

            foreach (KeyValuePair<ScriptCoreLib.ScriptDelegateDataHintAttribute, FieldInfo> v in ToArray(Delegate))
            {
                if (v.Key.Value == ScriptCoreLib.ScriptDelegateDataHintAttribute.FieldType.Target)
                    FieldTarget = v.Value;
                if (v.Key.Value == ScriptCoreLib.ScriptDelegateDataHintAttribute.FieldType.Method)
                    FieldMethod = v.Value;
            }


            if (FieldList == null)
                throw new NotSupportedException("A MulticastDelegate implementation must have a native array field.");

            if (FieldTarget == null)
                throw new NotSupportedException("A Delegate implementation must have a target field.");

            if (FieldMethod == null)
                throw new NotSupportedException("A Delegate implementation must have a method field.");            
        }
    }
}
