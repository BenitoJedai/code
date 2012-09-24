using ScriptCoreLib;

namespace java.io
{
    [Script(IsNative = true)]
    public class DataOutputStream : FilterOutputStream
    {
       
        public DataOutputStream(OutputStream o): base(o)
        {

        }






        #region methods

        
        public void writeBytes(string s)
        {
        }


       

        #endregion


        public override void write(int b)
        {
        }
    }
}
