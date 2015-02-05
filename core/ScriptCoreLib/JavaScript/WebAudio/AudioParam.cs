using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebAudio
{
    // http://webaudio.github.io/web-audio-api/
    // http://webaudio.github.io/web-audio-api/#idl-def-AudioParam
    // http://dan.nea.me/audiolandscape/

    [Script(HasNoPrototype = true, ExternalTarget = "AudioParam")]
    public class AudioParam
    {
        public double value;

        //public double* pvalue;
        //public ref double refvalue;

        [Obsolete("experimental")]
        public IHTMLInput valueInput
        {
            [Script(DefineAsStatic = true)]
            set
            {
                // X:\jsc.svn\examples\javascript\audio\StandardOscillator\StandardOscillator\Application.cs

                Native.window.onframe +=
                    delegate
                    {
                        this.value = value.valueAsNumber;

                    };
            }
        }

        //public static AudioParam operator >(AudioParam target, IHTMLInput source)
        //{
        //    // nop
        //    return target;
        //}

        //public static AudioParam operator <(AudioParam target, IHTMLInput source)
        //public static AudioParam operator +=(AudioParam target, IHTMLInput source)
        //{
        //    // operator extensions?
        //    // Show Details	Severity	Code	Description	Project	File	Line
        //    // Error CS0564  The first operand of an overloaded shift operator must have the same type as the containing type, and the type of the second operand must be int ScriptCoreLib   AudioParam.cs   19
        //    //Error CS0201  Only assignment, call, increment, decrement, and new object expressions can be used as a statement StandardOscillator  Application.cs  58

        //    // rather useless?


        //    Native.window.onframe +=
        //        delegate
        //        {
        //            target.value = source.valueAsNumber;

        //        };

        //    return target;
        //}
    }
}
