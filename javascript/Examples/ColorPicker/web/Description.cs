using ScriptCoreLib;


[assembly: ScriptResources("colors")]
[assembly: ScriptResources("gfx_demo")]

namespace ColorPicker.source.shared
{

    [Script]
    public static class Description
    {
        [Script]
        public static class gfx_demo
        {
            public const string tongue = "gfx_demo/tongue.gif";
        }

        [Script]
        public static class colors
        {
            public const string palette = "colors/palette.png";
        }
    }
}