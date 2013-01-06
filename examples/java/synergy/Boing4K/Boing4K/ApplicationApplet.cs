using java.applet;
using System.ComponentModel;

//namespace Boing4KTemplate
//{
// todo: JSC should demote classes that use java top level classes.. 

[Description("Subclass this class in your own applet.")]
public abstract class Boing4KApplet : a
{

}

internal sealed class ApplicationApplet : Boing4KApplet
{
    //        CreateType:  ApplicationApplet
    //error: System.InvalidOperationException: Unable to change after type has been created.
    //   at System.Reflection.Emit.TypeBuilder.ThrowIfCreated()

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
