using TTFCurrencyExperment;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TTFCurrencyExperment.Design;
using ScriptCoreLib.Extensions;

namespace TTFCurrencyExperment
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            // script: error JSC1000: No implementation found for this native method, please implement [System.Data.DataTableCollection.get_Item(System.Int32)]
            //var data = Treasury.GetDataSet().Tables[0];
            var data = Treasury.GetDataSet();

            this.dataGridView1.DataSource = data;
            this.dataGridView1.DataMember = "Sheet1";

            this.dataGridView1.Rows.AsEnumerable().WithEach(
                r =>
                {
                    var c = r.Cells[0];

                    if ((string)c.Value == "EUR")
                    {
                        //new fontawesome_webfont(
                        //c.Style.Font = new Font("FontAwesome", 12);
                        //c.Style.Font = label1.Font;

                        //dataGridViewCellStyle1.Font = new System.Drawing.Font("FontAwesome", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                        // http://social.msdn.microsoft.com/Forums/en-US/8f713f45-fc25-4a31-b2ea-f38984d93d12/changing-the-font-in-datagridview-for-specific-cells

                        //var style = new DataGridViewCellStyle();

                        //style.Font = new System.Drawing.Font("FontAwesome", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                        //c.Style = style;

                        //r.DefaultCellStyle = style;
                        //c.Style.ApplyStyle(style);
                        //r.DefaultCellStyle.ApplyStyle(style);

                        //c.Value = "";

                        //c.Value = "\xf153";
                        //c.Value = new string((char)0xf153, 1);
                        c.Value = new string((char)FontAwesomeCurrency.EUR, 1);

                        // 


                    }

                    if ((string)c.Value == "GBP")
                        c.Value = FontAwesomeCurrency.GBP.AsString();

                    if ((string)c.Value == "USD")
                        c.Value = FontAwesomeCurrency.USD.AsString();

                    if ((string)c.Value == "INR")
                        c.Value = FontAwesomeCurrency.INR.AsString();

                    if ((string)c.Value == "JPY")
                        c.Value = FontAwesomeCurrency.JPY.AsString();

                    if ((string)c.Value == "RUB")
                        c.Value = FontAwesomeCurrency.RUB.AsString();

                    if ((string)c.Value == "KRW")
                        c.Value = FontAwesomeCurrency.KRW.AsString();

                    if ((string)c.Value == "BTC")
                        c.Value = FontAwesomeCurrency.BTC.AsString();
                }
            );

        }

    }

    // http://fontawesome.io/cheatsheet/
    public enum FontAwesomeCurrency
    {
        EUR = 0xf153,
        GBP,
        USD,
        INR,
        JPY,
        RUB,
        KRW,
        BTC
    }

    public static class X
    {
        public static string AsString(this FontAwesomeCurrency x)
        {
            return new string((char)x, 1);
        }
    }
}
