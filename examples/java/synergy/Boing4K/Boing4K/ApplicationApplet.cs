using java.applet;
using System.ComponentModel;

//namespace Boing4KTemplate
//{
// todo: JSC should demote classes that use java top level classes.. 

[Description("Subclass this class in your own applet.")]
public abstract class Boing4KApplet : a
{
    //    0001 0200000a <module>.SHA1634771bc450e967ca9380bb62bb4df7b3b4036ba@2042611486$00000043
    //- javac
    //"C:\Program Files (x86)\Java\jdk1.7.0_25\bin\javac.exe" -classpath "S:\ApplicationApplet\web\java";release -d release java\ApplicationApplet.java
    //javac: file not found: java\ApplicationApplet.java
    //Usage: javac <options> <source files>
    //use -help for a list of possible options


}

public sealed class ApplicationApplet : Boing4KApplet
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
