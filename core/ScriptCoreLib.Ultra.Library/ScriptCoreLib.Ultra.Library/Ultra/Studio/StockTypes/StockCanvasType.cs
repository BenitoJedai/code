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

            this.UsingNamespaces.Add("System");
            this.UsingNamespaces.Add("System.Text");
            this.UsingNamespaces.Add("System.Linq");
            this.UsingNamespaces.Add("System.Xml");
            this.UsingNamespaces.Add("System.Xml.Linq");
            this.UsingNamespaces.Add("System.Windows.Media");
            this.UsingNamespaces.Add("System.Windows.Shapes");
            this.UsingNamespaces.Add("ScriptCoreLib.Extensions");
            this.UsingNamespaces.Add("ScriptCoreLib.Shared.Avalon.Extensions");

            // empty ctor
            var ctor = this.GetDefaultConstructorDefinition();

            var r = new SolutionProjectLanguageField
            {
                FieldType = new KnownStockTypes.System.Windows.Shapes.Rectangle(),
                FieldConstructor = new KnownStockTypes.System.Windows.Shapes.Rectangle().GetDefaultConstructor(),
                Name = "r",
                IsReadOnly = true
            };

            // we are adding a field. does it show up in the source code later?
            // SolutionProjectLanguage.WriteType makes it happen!
            this.Fields.Add(r);

            ctor.Code = new SolutionProjectLanguageCode
            {

            };

            this.Methods.Add(ctor);
        }
    }
}
