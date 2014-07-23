using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{

    // http://www.w3.org/TR/2014/WD-dom-20140710/#interface-mutationobserver
    // http://caniuse.com/mutationobserver

    [Script(HasNoPrototype = true, ExternalTarget = "MutationObserver")]
    public class MutationObserver
    {
        // Uncaught TypeError: Failed to construct 'MutationObserver': Callback argument must be a function 
        // jsc is not yet doing the correct thing here.

        //public MutationObserver(MutationCallback callback)

        public MutationObserver(IFunction callback)
        {

        }

        public void observe(INode target, object options)
        {
            // X:\jsc.svn\examples\javascript\Test\TestShadowIFrame\TestShadowIFrame\Application.cs


        }
    }

    [Script]
    public delegate void MutationCallback(MutationRecord[] mutations, MutationObserver observer);

    [Script(HasNoPrototype = true, ExternalTarget = "MutationObserver")]
    public class MutationRecord
    {
        public readonly string attributeName;
    }


}
