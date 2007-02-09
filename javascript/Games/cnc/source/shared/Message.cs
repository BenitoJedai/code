using ScriptCoreLib;

namespace cnc.source.shared
{
    /// <summary>
    /// this is the class that gets sent to the server back and forth
    /// </summary>
    [Script, System.Serializable]
    public class Message
    {
        public string Text;
        public string Tag;

        public int Identity;

        public int[] KnownIdentities;

        
    }
}