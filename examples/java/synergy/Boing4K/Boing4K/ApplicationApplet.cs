using java.applet;

//namespace Boing4KTemplate
//{
    // todo: JSC should demote classes that use java top level classes.. 

    internal sealed class ApplicationApplet : a
    {
        // see also: http://java4k.com/index.php?action=games&method=view&gid=328#source
        // other examples; wolfenstei4k, outrun

        public const int DefaultWidth = 256 * 4;
        public const int DefaultHeight = 256 * 4;

        public override void init()
        {
            base.resize(400, 300);
        }

    }
//}
