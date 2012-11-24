using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System.Xml.Linq;
using java.net;
using java.util.zip;
using System.Collections;
using System.IO;

//namespace TestBoing4KWindow
//{

static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    public static void Main(string[] args)
    {
        try
        {
            a.main(args);
        }
        catch
        {
            throw;
        }


        Console.WriteLine("hi! vm:" + typeof(object).FullName);


        System.Console.WriteLine("jvm");


        CLRProgram.XML = new XElement("hello", "world");
        CLRProgram.CLRMain(
        );

    }


}

public delegate XElement XElementFunc();

[SwitchToCLRContext]
static class CLRProgram
{
    public static XElement XML { get; set; }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    public static void CLRMain(
         StringAction ListMethods = null
        )
    {
        System.Console.WriteLine(XML);

        MessageBox.Show("it works?!?");
    }
}

//}
