using java.applet;

/*
namespace OutRun4KTemplate.Components
{*/

// todo: JSC should demote classes that use java top level classes.. 
public sealed class OutRun4KTemplate_Components_MyApplet1 : a
{
    public const int DefaultWidth = 256 * 4;
    public const int DefaultHeight = 256 * 4;

    public string FooMethodX()
    {
        return this.FooMethod2011();
    }

    public override void init()
    {
        base.resize(DefaultWidth, DefaultHeight);
    }

}
/*
}
 * */
