using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DragDataTableIntoCSVFile;
using DragDataTableIntoCSVFile.Design;
using DragDataTableIntoCSVFile.HTML.Pages;
using System.Windows.Forms;
using System.Data;
using ScriptCoreLib.Library;

namespace DragDataTableIntoCSVFile
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // see also
            // X:\jsc.svn\examples\javascript\DragIntoCRX\DragIntoCRX\Application.cs

            page.Data.WhenClicked(
                async button =>
                {
                    var DataTable = await this.DoEnterData();
                    var DataTable_xml = StringConversionsForDataTable.ConvertToString(
                                        DataTable
                                    );


                    var grid = new DataGridView { DataSource = DataTable };


                    grid.AttachControlTo(page.output);

                    var csv = WriteCSV(DataTable);


                    // http://www.w3.org/TR/file-writer-api/

                    page.x.title = "special.csv";
                    page.x.style.textDecoration = "underline";

                    page.x.css.hover.style.color = "red";


                    // http://stackoverflow.com/questions/19327749/javascript-blob-filename-without-link
                    var blob = new Blob(new[] { csv.ToString() }, new { type = "octet/stream" });

                    var href = blob.ToObjectURL();



                    page.x.href = href;

                    // we can now click on the link
                    page.x.download = page.x.title;

                    // hide the fact, we are actually using <a>
                    //page.x.Hide();

                    //var iframe = new IHTMLIFrame { name = "y" }.AttachToDocument();
                    //page.x.target = iframe.name;

                    // http://updates.html5rocks.com/2011/08/Saving-generated-files-on-the-client-side
                    // http://msdn.microsoft.com/en-us/library/ie/hh779016(v=vs.85).aspx
                    // http://stackoverflow.com/questions/4309958/can-i-write-files-with-html5-js




                    page.Csv.disabled = false;
                    page.Csv.onclick +=
                        delegate
                        {
                            page.x.click();

                        };

                    page.Csv.ondragstart +=
                          e =>
                          {
                              Console.WriteLine("ondragstart");

                              // public void addElement(IHTMLElement element);
                              //e.dataTransfer.addElement(
                              // http://help.dottoro.com/ljxfefku.php

                              // X:\jsc.svn\examples\javascript\DropFileIntoSQLite\DropFileIntoSQLite\Application.cs
                              e.dataTransfer.effectAllowed = "copy";

                              e.dataTransfer.setData(

                                   typeof(DataTable).Name
                                    //"jsc/datatable"
                                    ,
                                    DataTable_xml

                              );

                              //                              ondragover: { types = 1, items = 1, files = 0 }
                              // view-source:29615
                              //{ type = jsc/datatable } 

                              Console.WriteLine("setDownloadURL");
                              // http://www.thecssninja.com/html5/gmail-dragout
                              // Unfortunately it doesn’t work anymore in any browser, not also in chrome

                              // https://developer.mozilla.org/en-US/docs/Web/API/DataTransfer#setData.28.29
                              e.dataTransfer.setDownloadURL(
                                 page.x.title,
                                  Encoding.UTF8.GetBytes(csv.ToString())
                              );
                          };


                }
            );

        }

        private static StringBuilder WriteCSV(System.Data.DataTable DataTable)
        {
            var csv = new StringBuilder();

            // http://en.wikipedia.org/wiki/Comma-separated_values

            foreach (DataColumn item in DataTable.Columns)
            {
                csv.Append(item.ColumnName + ",");
            }

            csv.AppendLine();

            foreach (DataRow item in DataTable.Rows)
            {
                var r = new DataGridViewRow();

                foreach (DataColumn c in DataTable.Columns)
                {
                    csv.Append(item[c] + ",");
                }

                csv.AppendLine();
            }
            return csv;
        }

    }
}
