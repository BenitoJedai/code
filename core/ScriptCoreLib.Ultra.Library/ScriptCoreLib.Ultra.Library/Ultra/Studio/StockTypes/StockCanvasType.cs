using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;

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


            InitializeConstructorCode(ctor, r);

            this.Methods.Add(ctor);
        }

        private static void InitializeConstructorCode(SolutionProjectLanguageMethod ctor, SolutionProjectLanguageField r)
        {
            // jsc has a bug with nested params in stack...

            var Brushes_Red = 
                new KnownStockTypes.System.Windows.Media.Brushes.get_Red().ToCallExpression();

            var r_set_Fill_to_Brushes_Red =
                new KnownStockTypes.System.Windows.Shapes.Rectangle.set_Fill().ToCallExpression(r,
                    Brushes_Red
                );

            var r_AttachTo_this =
                new KnownStockTypes.ScriptCoreLib.Shared.Avalon.Extensions.SupportsContainerExtensions.AttachTo().ToCallExpression(
                    r,
                    new PseudoThisExpression()
                );

            var r_MoveTo_8_8 =
                 new KnownStockTypes.ScriptCoreLib.Shared.Avalon.Extensions.SupportsContainerExtensions.MoveTo().ToCallExpression(
                    r,
                    (PseudoInt32ConstantExpression)8,
                    (PseudoInt32ConstantExpression)8
                );

            var this_get_Height_sub_16 =
                new KnownStockTypes.System.Windows.FrameworkElement.get_Height().ToCallExpression(new PseudoThisExpression()) - 16;

            var this_get_Width_sub_16 =
                new KnownStockTypes.System.Windows.FrameworkElement.get_Width().ToCallExpression(new PseudoThisExpression()) - 16;


            var r_SizeTo_16_16 =
                  new KnownStockTypes.ScriptCoreLib.Shared.Avalon.Extensions.SupportsContainerExtensions.SizeTo().ToCallExpression(
                     r,
                     this_get_Width_sub_16,
                     this_get_Height_sub_16
                 );


            var this_add_SizeChanged_handler = r_SizeTo_16_16.ToAnonymousMethod();

            this_add_SizeChanged_handler.Parameters.Add("s");
            this_add_SizeChanged_handler.Parameters.Add("e");

            var this_add_SizeChanged =
                new KnownStockTypes.System.Windows.FrameworkElement.add_SizeChanged().ToCallExpression(
                    new PseudoThisExpression(),
                    this_add_SizeChanged_handler
                );

            ctor.Code = new SolutionProjectLanguageCode
            {
                r_set_Fill_to_Brushes_Red,
                r_AttachTo_this,
                r_MoveTo_8_8,
                this_add_SizeChanged
            };
        }
    }
}
