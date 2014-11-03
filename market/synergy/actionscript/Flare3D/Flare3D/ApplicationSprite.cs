using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;

namespace Flare3D
{
    public sealed class ApplicationSprite : Sprite
    {
        // Write Assembly Failed!. ERROR:Could not load type 'flare.core.Pivot3D' from assembly 'Flare3D_2.5.18_Trial.swc, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'.

        //System.TypeLoadException: Declaration referenced in a method implementation cannot be a final method.  Type: 'flare.core.Texture3D'.  Assembly: 'Flare3D_2.5.18_Trial.swc, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'.
        //System.TypeLoadException: Declaration referenced in a method implementation cannot be a final method.  Type: 'flare.core.Pivot3D'.  Assembly: 'Flare3D_2.5.18_Trial.swc, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'.
        //System.Reflection.ReflectionTypeLoadException: Unable to load one or more of the requested types. Retrieve the LoaderExceptions property for more information.

        public ApplicationSprite()
        {
        }

    }
}
