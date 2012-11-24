using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]

namespace TestMulticonstraintBoxOpcode
{
    public class Sprite
    {
        public void Foo()
        { }
    }

    public interface IApplicationSprite
    {
        void Bar();
    }

    public static class ApplicationSpriteContent
    {
        public static void WithSprite(this Sprite e)
        {
        }

        public static void WithIApplicationSprite(this IApplicationSprite e)
        {
        }

        public static void InitializeContent<TApplicationSprite>(this TApplicationSprite that)
            where TApplicationSprite : Sprite, IApplicationSprite
        {

            that.Foo();
            that.WithSprite();
            that.Bar();
            that.WithIApplicationSprite();

        }
    }
}
