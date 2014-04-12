using SharedBrowserSessionExperiment.DataLayer.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;

namespace SharedBrowserSessionExperiment
{
    public partial class TheBrowserTab : Form
    {
        // X:\jsc.svn\examples\javascript\forms\FormsDualDataSource\FormsDualDataSource\ApplicationControl.cs

        public TheBrowserTab()
        {
            NavigationOrdersNavigateBindingSource.CreateDataSource.With(
                CreateDataSource =>
                {
                    NavigationOrdersNavigateBindingSource.CreateDataSource =
                        delegate
                        {
                            Console.WriteLine("TheBrowserTab NavigationOrdersNavigateBindingSource.CreateDataSource");

                            return CreateDataSource();
                        };
                }
            );

            InitializeComponent();
        }

        private void navigationOrdersNavigateBindingSourceBindingNavigator_RefreshItems(object sender, EventArgs e)
        {
            //Console.WriteLine("navigationOrdersNavigateBindingSourceBindingNavigator_RefreshItems");

            //this.Text = new
            //{
            //    navigationOrdersNavigateBindingSourceBindingNavigator.PositionItem.Text,
            //    navigationOrdersNavigateBindingSourceBindingNavigator.BindingSource.Position
            //}.ToString();
        }

        private void navigationOrdersNavigateBindingSourceBindingSource_PositionChanged(object sender, EventArgs e)
        {
            Console.WriteLine("navigationOrdersNavigateBindingSourceBindingSource_PositionChanged");
            this.Text = new
            {
                navigationOrdersNavigateBindingSourceBindingSource.Position
            }.ToString();
        }

        private void TheBrowserTab_Load(object sender, EventArgs e)
        {
            // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Resources\ResourceManager.cs
            //script: error JSC1000: No implementation found for this native method, please implement [System.Resources.ResourceManager.GetObject(System.String)]
        }

        private void TheBrowserTab_DragDrop(object sender, DragEventArgs e)
        {
            // X:\jsc.svn\examples\javascript\forms\Test\TestDropURL\TestDropURL\ApplicationControl.cs


            var r = new NavigationOrdersNavigateRow { urlString = (string)e.Data.GetData("Text") };

            if (r.urlString.StartsWith("https://www.youtube.com/watch?v="))
                r.urlString = "https://www.youtube.com/embed/" + r.urlString.SkipUntilOrEmpty("https://www.youtube.com/watch?v=");

            //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.WebBrowser.set_ScriptErrorsSuppressed(System.Boolean)]

            var IsWellFormedUriString = Uri.IsWellFormedUriString(r.urlString, UriKind.Absolute);
            Console.WriteLine(new { IsWellFormedUriString, r.urlString });


            if (IsWellFormedUriString)
            {
                // 58:24479ms { IsWellFormedUriString = true, urlString = https://www.youtube.com/embed/lsbYqjMkK9Q }

                (this.navigationOrdersNavigateBindingSourceBindingSource.AddNew() as DataRowView).With(
                    rr =>
                    {
                        rr["urlString"] = r.urlString;
                    }
                );

            }
        }

        private void TheBrowserTab_DragOver(object sender, DragEventArgs e)
        {
            // IE allows only link
            e.Effect = DragDropEffects.Link;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //BindingSource
            (this.navigationOrdersNavigateBindingSourceBindingSource.AddNew() as DataRowView).With(
                rr =>
                {
                    rr["urlString"] = "https://www.youtube.com/embed/lsbYqjMkK9Q";
                }
            );
        }

        private async void button4_Click(object sender, EventArgs e)
        {


            this.applicationWebService1.RowsWithoutKeys = Enumerable.Range(0, this.navigationOrdersNavigateBindingSourceBindingSource.Count).Select(
                position =>
                {
                    var v = this.navigationOrdersNavigateBindingSourceBindingSource[position];

                    // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Shared\Data\Diagnostics\WithConnectionLambda.cs

                    //at System.Number.StringToNumber(String str, NumberStyles options, NumberBuffer& number, NumberFormatInfo info, Boolean parseDecimal)
                    //at System.Number.ParseInt64(String value, NumberStyles options, NumberFormatInfo numfmt)
                    //at System.String.System.IConvertible.ToInt64(IFormatProvider provider)
                    //at System.Convert.ToInt64(Object value)

                    //at ScriptCoreLib.Shared.Data.Diagnostics.WithConnectionLambdaZ.GetInt64OrDefault(DataRow e, String ColumnName)

                    //at SharedBrowserSessionExperiment.DataLayer.Data.NavigationOrdersNavigateRow.op_Implicit(DataRow )
                    //at SharedBrowserSessionExperiment.TheBrowserTab.<button4_Click>b__c(Int32 position) in x:\jsc.svn\examples\javascript\p2p\SharedBrowserSessionExperiment\SharedBrowserSessionExperiment\TheBrowserTab.cs:line 114
                    //at System.Linq.Enumerable.WhereSelectEnumerableIterator`2.MoveNext()
                    //at System.Linq.Buffer`1..ctor(IEnumerable`1 source)
                    //at System.Linq.Enumerable.ToArray[TSource](IEnumerable`1 source)
                    //at SharedBrowserSessionExperiment.TheBrowserTab.<button4_Click>d__d.MoveNext() in x:\jsc.svn\examples\javascript\p2p\SharedBrowserSessionExperiment\SharedBrowserSessionExperiment\TheBrowserTab.cs:line 109

                    NavigationOrdersNavigateRow r = (v as DataRowView).Row;

                    // r = {0, https://www.youtube.com/embed/XjwZAa2EjKA, , 4/12/2014 6:41:33 PM}

                    if (r.Key == default(NavigationOrdersNavigateKey))
                        return r;

                    return null;
                }
            ).ToArray();

            await this.applicationWebService1.BindingSourceSynchonization();

            var rows0 = this.navigationOrdersNavigateBindingSourceBindingSource.AsEnumerableOfDataRow().Select(
                // NavigationOrdersNavigateRow is not bound to DataRow
                // it could, tho.. int the future versions.
                x => new { x, y = (NavigationOrdersNavigateRow)x }
                );

            this.applicationWebService1.RowsWithoutKeys.WithEach(
                r =>
                {
                    Console.WriteLine("about to update form server " + new { r.Key });

                    // http://msdn.microsoft.com/en-us/magazine/cc163974.aspx

                    (from xx in rows0
                     where xx.y.urlString == r.urlString
                     where xx.y.Key == default(NavigationOrdersNavigateKey)
                     select xx
                        ).WithEach(xx =>
                            xx.x["Key"] = r.Key
                            );

                }
            );

            //await new Form { Text = "continue?" }.ShowAsync();

            var rows = this.navigationOrdersNavigateBindingSourceBindingSource.AsEnumerableOfDataRow().Select(
                // NavigationOrdersNavigateRow is not bound to DataRow
                // it could, tho.. int the future versions.
                    x =>
                    {
                        var xx = new { x, y = (NavigationOrdersNavigateRow)x };

                        //// string or not?
                        //var x_Key = x["Key"];
                        //var x_Key_isString = x_Key is string;

                        //// x_Key = "1"
                        //Console.WriteLine("after update " + new { x_Key, x_Key_isString, xx.y.Key });

                        return xx;
                    }
              );
            // about to add form server { Key = 3 }
            //after update { x_Key = 1, x_KeyType = System.String, Key = 1 }
            //{ Key = 1 } eq { Key = 3 }
            //after update { x_Key = 2, x_KeyType = System.String, Key = 2 }
            //{ Key = 2 } eq { Key = 3 }

            //61:11195ms about to add form server { Key = 3 }
            //61:11196ms after update { x_Key = 1, x_KeyType = <Namespace>., Key = 0 }
            //61:11196ms { Key = 0 } eq { Key = 3 }
            //61:11196ms after update { x_Key = 2, x_KeyType = <Namespace>., Key = 0 }
            //61:11196ms { Key = 0 } eq { Key = 3 }

            this.applicationWebService1.IncrementalSyncTake.WithEach(
                r =>
                {
                    //Console.WriteLine("about to add form server " + new { r.Key });

                    // either it exists or we need to add a new row!
                    var xx = rows.FirstOrDefault(x =>
                        {
                            //Console.WriteLine(new { x.y.Key } + " eq " + new { r.Key });

                            return x.y.Key == r.Key;
                        }
                    );

                    if (xx == null)
                    {
                        // not found
                        (this.navigationOrdersNavigateBindingSourceBindingSource.AddNew() as DataRowView).With(
                            z =>
                            {
                                var x = z.Row;
                                var y = (NavigationOrdersNavigateRow)x;

                                xx = new { x, y };

                                // set the key
                                xx.x["Key"] = r.Key;
                            }
                        );
                    }

                    // server overrides local data
                    xx.x["urlString"] = r.urlString;

                    // skip this key next time?
                    if (r.Key > this.applicationWebService1.IncrementalSyncSkip)
                        this.applicationWebService1.IncrementalSyncSkip = r.Key;

                    // we dont have updates tho
                }
            );
        }
    }

    static class X
    {
        public static IEnumerable<DataRow> AsEnumerableOfDataRow(this BindingSource x)
        {
            return Enumerable.Range(0, x.Count).Select(
                position =>
                {
                    var v = x[position];

                    return (v as DataRowView).Row;
                }
            ).ToArray();
        }
    }
}
