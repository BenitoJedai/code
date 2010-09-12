using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio.StockTypes
{
    public class StockCanvasType : SolutionProjectLanguageType
    {
        public StockCanvasType(string Namespace, string Name)
        {
            this.Namespace = Namespace;
            this.Name = Name;

            this.BaseType = new KnownStockTypes.System.Windows.Controls.Canvas();

            // no need to be sealed
            this.IsSealed = false;

            // empty ctor
            this.Methods.Add(this.GetDefaultConstructorDefinition());
        }
    }
}
