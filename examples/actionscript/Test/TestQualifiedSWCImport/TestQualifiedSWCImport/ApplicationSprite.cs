using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;

namespace TestQualifiedSWCImport
{
    public sealed class ApplicationSprite : Sprite
    {
        public global::FooNamespace.InnerFooNamespace.Foo Foo1;
        public global::OtherFooNamespace.InnerFooNamespace.Foo Foo2;
        public global::OtherFooNamespace.InnerFooNamespace.More.Foo Foo3;

        public ApplicationSprite()
        {
            var bar = new global::OtherFooNamespace.InnerFooNamespace.Bar();

        }

    }
}
