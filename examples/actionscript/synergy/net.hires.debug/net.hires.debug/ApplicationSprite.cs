using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;

namespace net.hires.debug
{
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150107


            // http://www.flare3d.com/support/index.php?topic=1101.0
            // "X:\jsc.svn\examples\actionscript\synergy\net.hires.debug\net.hires.debug\net.hires.debug.csproj"
            // was this dependency built?

            // Error	5	The type or namespace name 'Stats' could not be found (are you missing a using directive or an assembly reference?)	X:\jsc.svn\examples\actionscript\synergy\net.hires.debug\net.hires.debug\ApplicationSprite.cs	15	31	net.hires.debug
            this.addChild(new Stats());
        }

    }
}
