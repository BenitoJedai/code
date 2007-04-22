using ScriptCoreLib;
using ScriptCoreLib.Shared.Drawing;


namespace ScriptCoreLib.JavaScript.Silverlight.Controls
{

    [Script(InternalConstructor=true)]
    public class Image : UIElement
    {
        public string Source;

        
        #region InternalConstructor
        public Image(SilverlightControl ag)
        {

        }

        internal static Image InternalConstructor(SilverlightControl ag)
        {
            return (Image) ag.CreateElement("Image");
        }
        #endregion

    }
}