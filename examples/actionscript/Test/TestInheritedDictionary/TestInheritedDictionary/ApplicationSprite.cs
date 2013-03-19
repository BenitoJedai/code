using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;

namespace TestInheritedDictionary
{
    //R:\web\TestInheritedDictionary\Level_AttributeDictonary.as(17): col: 24 Error: Interface method get_Item_100669773 in namespace ScriptCoreLib.Shared.BCLImplementation.System.Collections:__IDictionary not implemented by class TestInheritedDictionary:Level_AttributeDictonary.

    //    public final class Level_AttributeDictonary extends __Dictionary_2 implements __IDictionary_2, __ICollection_1, __IEnumerable_1, __IEnumerable, __IDictionary, __ICollection, __ISerializable, __IDeserializationCallback
    //                       ^

    //R:\web\TestInheritedDictionary\Level_AttributeDictonary.as(17): col: 24 Error: Interface method set_Item_100669774 in namespace ScriptCoreLib.Shared.BCLImplementation.System.Collections:__IDictionary not implemented by class TestInheritedDictionary:Level_AttributeDictonary.

    //    public final class Level_AttributeDictonary extends __Dictionary_2 implements __IDictionary_2, __ICollection_1, __IEnumerable_1, __IEnumerable, __IDictionary, __ICollection, __ISerializable, __IDeserializationCallback
    //                       ^

    //R:\web\TestInheritedDictionary\Level_AttributeDictonary.as(17): col: 24 Error: Interface method get_Count_100669607 in namespace ScriptCoreLib.Shared.BCLImplementation.System.Collections:__ICollection not implemented by class TestInheritedDictionary:Level_AttributeDictonary.

    //    public final class Level_AttributeDictonary extends __Dictionary_2 implements __IDictionary_2, __ICollection_1, __IEnumerable_1, __IEnumerable, __IDictionary, __ICollection, __ISerializable, __IDeserializationCallback
    //                       ^

    //R:\web\TestInheritedDictionary\Level_AttributeDictonary.as(17): col: 24 Error: Interface method get_Count_100669607 in namespace ScriptCoreLib.Shared.BCLImplementation.System.Collections:__ICollection not implemented by class TestInheritedDictionary:Level_AttributeDictonary.

    //    public final class Level_AttributeDictonary extends __Dictionary_2 implements __IDictionary_2, __ICollection_1, __IEnumerable_1, __IEnumerable, __IDictionary, __ICollection, __ISerializable, __IDeserializationCallback
    //                       ^






    partial class Level
    {
        public class Attribute
        {
        }

        public sealed class AttributeDictonary : Dictionary<string, Action<string>>
        {
            public void Add(Attribute e)
            {
                //KeyValuePair<string, Action<string>> i = e;

                //this.Add(i);
            }
        }

    }

    public sealed class ApplicationSprite : Sprite
    {

        public ApplicationSprite()
        {
            new Level.AttributeDictonary().Add(null);
        }

    }
}
