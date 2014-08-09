using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace FlashHeatZeeker.Core.Library
{
    // how does it differ from Task<>?
    [Description("Conflicting name with System.Lazy!")]
    public class XLazy<T>
    {
        public Func<T> InternalGetContent;
        public T InternalContent;

        public T Content
        {
            get
            {
                if (this.InternalGetContent != null)
                {
                    this.InternalContent = this.InternalGetContent();
                    this.InternalGetContent = null;

                }


                return this.InternalContent;
            }
        }

        public XLazy(Func<T> y)
        {
            InternalGetContent = y;
        }
    }

}
