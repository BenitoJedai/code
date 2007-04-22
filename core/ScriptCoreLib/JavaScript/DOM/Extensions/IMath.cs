
namespace ScriptCoreLib.JavaScript.DOM
{
    
    public partial class IMath
    {
        [Script(DefineAsStatic=true)]
        public int minmax(int e, int min, int max) 
        {
            return this.max(this.min(e, min), max);
        }
    }
}