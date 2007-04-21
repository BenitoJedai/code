using ScriptCoreLib;
using ScriptCoreLib.Shared.Drawing;


namespace ScriptCoreLib.JavaScript.Silverlight.Input
{

    [Script(HasNoPrototype=true)]
    public class MouseEventArgs
    {
        public readonly int X;
        public readonly int Y;

        public Point Position
        {
            [Script(DefineAsStatic=true)]
            get
            {
                return new Point(X, Y);
            }
        }

        public readonly bool Ctrl;
        public readonly bool Shift;
    }
}