using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFieldOfGenericOfNestedType
{
    // #1
    interface IIntervalIntrospector<T>
    {
        //void foo(FormattingContext c);
        T fooT(T t);

        //T GetStart(T t);
    }

    // #3
    public class FormattingContext
        :
        //IIntervalIntrospector<FormattingContext.AnchorData>,
        //IIntervalIntrospector<FormattingContext.LazyIndentationData>,
        IIntervalIntrospector<FormattingContext.RelativeIndentationData>
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201306/20120626-roslyn

        //Roslyn.Services.Formatting.FormattingContext+InitialContextFinder due to rec_SourceType
        //Roslyn.Services.Formatting.FormattingContext+<>c__DisplayClassc due to rec_SourceType
        //Roslyn.Services.Formatting.FormattingContext+RootIndentationData due to rec_SourceType
        //Roslyn.Services.Formatting.FormattingContext+<>c__DisplayClass7 due to rec_SourceType
        //Roslyn.Services.Formatting.FormattingContext+LazyIndentationData due to rec_SourceType
        //Roslyn.Services.Formatting.FormattingContext+SimpleIndentationData due to rec_SourceType
        //Roslyn.Services.Formatting.FormattingContext+<>c__DisplayClass10 due to rec_SourceType
        //7 types above waiting for Roslyn.Services.Formatting.FormattingContext
        //  Roslyn.Services.Formatting.FormattingContext+RelativeIndentationData due to GetInterfaces GetGenericArguments
        //    Roslyn.Services.Formatting.FormattingContext+LazyIndentationData due to LookAtBaseType
        //      Roslyn.Services.Formatting.FormattingContext due to rec_SourceType

        // #2
        public class AnchorData
        {

        }

        public Dictionary<object, AnchorData> anchorBaseTokenMap;

        // #4
        public class IndentationData
        {

        }

        // #5
        public class LazyIndentationData : IndentationData
        {

        }

        // #6 what if this cannot stay nested?
        public class RelativeIndentationData : LazyIndentationData
        {

        }


        //public FormattingContext.AnchorData GetStart(FormattingContext.AnchorData t)
        //{
        //    throw new NotImplementedException();
        //}

        //public FormattingContext.LazyIndentationData GetStart(FormattingContext.LazyIndentationData t)
        //{
        //    throw new NotImplementedException();
        //}

        //public FormattingContext.RelativeIndentationData GetStart(FormattingContext.RelativeIndentationData t)
        //{
        //    throw new NotImplementedException();
        //}

        //public void foo()
        //{
        //    throw new NotImplementedException();
        //}


        public FormattingContext.RelativeIndentationData fooT(FormattingContext.RelativeIndentationData t)
        {
            FormattingContext.RelativeIndentationData loc = t;

            this.field = t;

            return t;
        }


        FormattingContext.RelativeIndentationData field;
        //public void foo(FormattingContext c)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
