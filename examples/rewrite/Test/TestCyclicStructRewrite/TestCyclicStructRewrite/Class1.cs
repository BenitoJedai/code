extern alias cyclic;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public struct a<T>
{
}

struct c
{
    public a<u> a;
    //a<cyclic::u> a;
}



class u
{
    public c c;
}

class Program
{
    static void Main(string[] args)
    {
        // can we still use it?

        var u = new u();
        var c = u.c;
        var a = c.a;

        Console.WriteLine("done! " + a.GetType().AssemblyQualifiedName);
        Console.ReadKey();
    }
}

