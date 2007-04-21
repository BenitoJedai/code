using ScriptCoreLib;


namespace ScriptCoreLib.JavaScript.Silverlight.Media
{
    // http://msdn2.microsoft.com/en-us/library/system.windows.media.translatetransform.x.aspx

    [Script(HasNoPrototype=true)]
    public class RotateTransform : Translate
    {
        public double Angle;
    }
}