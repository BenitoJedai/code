using java.applet;

/*
namespace wolfenstein4k.Components
{
*/
    // todo: JSC should demote classes that use java top level classes.. 
    internal sealed class MyApplet1 : a
    {
        public const int DefaultWidth = 256 * 4;
        public const int DefaultHeight = 256 * 4;

        public override void init()
        {
            base.resize(DefaultWidth, DefaultHeight);
        }

    }
/*
}
*/
