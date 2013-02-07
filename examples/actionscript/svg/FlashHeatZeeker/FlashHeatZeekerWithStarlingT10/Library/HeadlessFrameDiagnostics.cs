using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashHeatZeekerWithStarlingT10.Library
{
    // this interface is used to communicate from 
    // flash to html container
    // jsc could try to do this on its own in the future
    public interface IFrameDiagnostics
    {
        Action<Action<string, string>> traceperformance { get; set; }
         IFrameDiagnosticsFlag traceperformance_enabled { get; set; }
        

        Action F1 { get; set; }
        Action F2 { get; set; }

        IFrameDiagnosticsFlag user_pause { get; set; }

        IFrameDiagnosticsFlag hidelayers { get; set; }
        
        IFrameDiagnosticsFlag hidetrees { get; set; }
        IFrameDiagnosticsFlag hidegroundunits { get; set; }
        IFrameDiagnosticsFlag hideairunits { get; set; }
    }



    // to be used in flash
    public class HeadlessFrameDiagnostics : IFrameDiagnostics
    {
        // jsc could turn this into implicit interface for tier split/context switch

        public Action<Action<string, string>> traceperformance { get; set; }
        public IFrameDiagnosticsFlag traceperformance_enabled { get; set; }

        public Action F1 { get; set; }
        public Action F2 { get; set; }


        public IFrameDiagnosticsFlag user_pause { get; set; }

        public IFrameDiagnosticsFlag hidelayers { get; set; }

        public IFrameDiagnosticsFlag hidetrees { get; set; }
        public IFrameDiagnosticsFlag hidegroundunits { get; set; }
        public IFrameDiagnosticsFlag hideairunits { get; set; }



    }


    public interface IFrameDiagnosticsFlag
    {
        string value { get; set; }
    }

    public class HeadlessFrameDiagnosticsFlag : IFrameDiagnosticsFlag
    {
        public HeadlessFrameDiagnosticsFlag()
        {

        }

        public Func<string> GetValue;
        public Action<string> SetValue;

        public string value
        {
            get
            {
                return GetValue();
            }
            set
            {
                SetValue(value);
            }
        }
    }
}
