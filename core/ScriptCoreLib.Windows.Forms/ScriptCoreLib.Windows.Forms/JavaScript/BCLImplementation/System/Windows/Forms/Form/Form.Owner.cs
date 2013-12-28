using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Drawing;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    public partial class __Form
    {
        public List<Form> InternalOwnedForms = new List<Form>();

        public Form[] OwnedForms
        {
            get
            {
                return InternalOwnedForms.ToArray();
            }
        }

        #region Owner
        public __Form InternalOwner;
        public Form Owner
        {
            get { return InternalOwner; }
            set
            {
                if (InternalOwner != null)
                    InternalOwner.InternalOwnedForms.Remove(this);

                InternalOwner = value;

                if (InternalOwner != null)
                {
                    InternalOwner.InternalOwnedForms.Add(this);
                    InternalOwner.InternalUpdateZIndex();
                }
            }
        }
        #endregion
    }


}
