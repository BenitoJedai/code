using ScriptCoreLib;


namespace ScriptCoreLib.JavaScript.Silverlight
{
    // based on http://msdn2.microsoft.com/en-us/library/bb188406.aspx

    [Script]
    public class SilverlightControl
    {
        // this shall replace the ag host

        // comatibility
        [Script(NoDecoration = true)]
        public static void agHost(
            // hostElementId, id, width, height, backgroundColor, sourceElement, source, isWindowlessMode, framerate, errorHandler, reqMajorVer, reqMinorVer, reqBuildVer
            )
        {
        }
    }
}
