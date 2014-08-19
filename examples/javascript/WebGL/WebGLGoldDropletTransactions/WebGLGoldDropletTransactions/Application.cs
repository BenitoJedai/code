using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using ScriptCoreLib.Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WebGLGoldDropletTransactions;
using WebGLGoldDropletTransactions.Data;
using WebGLGoldDropletTransactions.Design;
using WebGLGoldDropletTransactions.HTML.Pages;
using WebGLOBJExperiment;

namespace WebGLGoldDropletTransactions
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

            // 1e40:01:01 RewriteToAssembly error: System.MissingMethodException: Method not found: 'Void ScriptCoreLib.JavaScript.DOM.IWindow.add_onframe(System.Action`1<Int32>)'.

#if chrome_works_again
            #region AtFormCreated
            FormStyler.AtFormCreated =
                 s =>
                 {
                     s.Context.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

                     var x = new ChromeTCPServerWithFrameNone.HTML.Pages.AppWindowDrag().AttachTo(s.Context.GetHTMLTarget());
                 };
            #endregion


            // chrome 31 wont load view-source
            // chrome 33 shows black window. nice.
            #region ChromeTCPServer
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                chrome.Notification.DefaultTitle = "Droplet";
                //chrome.Notification.DefaultIconUrl = new HTML.Images.FromAssets.Preview().src;

                ChromeTCPServer.TheServerWithStyledForm.Invoke(
                    AppSource.Text,
                    AtFormCreated: FormStyler.AtFormCreated
                );

                return;
            }
            #endregion
#endif


            #region clouds
            new WebGLClouds.HTML.Pages.Default().With(
                layout =>
                {
                    layout.body.AttachTo(page.clouds);

                    new WebGLClouds.Application(layout);
                }
            );
            #endregion



            page.header.style.backgroundColor = "";

            page.header.css.style.transition = "background-color 200ms linear";
            page.header.css.style.backgroundColor = "rgba(255, 255, 0, 0)";
            //page.header.css.style.backgroundColor = "rgba(255, 255, 0, 0.2)";

            page.header.css.hover.style.backgroundColor = "rgba(255, 255, 0, 1.0)";

            Native.window.onframe +=
                delegate
                {
                    if (Native.document.body.scrollTop == 0)
                        page.header.css.style.backgroundColor = "rgba(255, 255, 0, 0)";
                    else
                        page.header.css.style.backgroundColor = "rgba(0, 0, 0, 0.3)";

                };

            var oo = new List<THREE.Object3D>();

            var window = Native.window;

            var camera = new THREE.PerspectiveCamera(
                45,
                page.header.clientWidth / (double)page.header.clientHeight,
                1,
                2000
                );
            camera.position.z = 400;

            // scene

            var scene = new THREE.Scene();

            var ambient = new THREE.AmbientLight(0x101030);
            scene.add(ambient);

            var directionalLight = new THREE.DirectionalLight(0xffeedd);
            directionalLight.position.set(0, 0, 1);
            scene.add(directionalLight);

            var renderer = new THREE.WebGLRenderer();

            //renderer.domElement.AttachToDocument();
            renderer.domElement.AttachTo(page.header);
            renderer.setSize(page.header.clientWidth, page.header.clientHeight);
            //renderer.setSize(window.Width, window.Height);
            //renderer.domElement.style.SetLocation(0, 0);


            var mouseX = 0;
            var mouseY = 0;
            var st = new Stopwatch();
            st.Start();

            Native.window.onframe +=
                delegate
                {

                    oo.WithEach(
                        x =>
                            x.rotation.y = st.ElapsedMilliseconds * 0.001
                    );


                    camera.position.x += (mouseX - camera.position.x) * .05;
                    camera.position.y += (-mouseY - camera.position.y) * .05;

                    camera.lookAt(scene.position);

                    renderer.render(scene, camera);


                };

            Native.window.onresize +=
                delegate
                {
                    camera.aspect = page.header.clientWidth / (double)page.header.clientHeight;

                    camera.updateProjectionMatrix();

                    //renderer.setSize(window.Width, window.Height);
                    renderer.setSize(page.header.clientWidth, page.header.clientHeight);

                };




            var data = Book1.GetDataSet();

            #region bind
            Func<string, IHTMLElement, DataGridView> bind =
                (DataMember, c) =>
                {
                    var g = new DataGridView
                    {
                        BackgroundColor = Color.Transparent,


                        // does this work?
                        DefaultCellStyle = new DataGridViewCellStyle
                        {

                            SelectionBackColor = Color.Black,
                            SelectionForeColor = Color.Yellow,

                            //BackColor = Color.Transparent
                            //BackColor = Color.FromArgb(0x05, 0, 0, 0)
                            BackColor = Color.FromArgb(0x3f, 255, 255, 255)
                        },

                        ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                        {

                            BackColor = Color.FromArgb(0x8f, 255, 255, 255)
                        },


                        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,

                        // do we have a test for this?
                        AllowUserToAddRows = false,

                        //AllowUserToDeleteRows = false,

                        RowHeadersVisible = false,


                        // cannot hide column headers yet
                        // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridView.set_ColumnHeadersVisible(System.Boolean)]
                        //ColumnHeadersVisible = false,

                        DataSource = data,
                        DataMember = DataMember,
                    };

                    // this should be the one that maximizes itself onto the parent which is supposed to be absolute in size
                    //g.GetHTMLTarget().With(
                    //    div =>
                    //    {
                    //        //div.style.reset();

                    //        // no scrollbars, thanks
                    //        div.style.overflow = IStyle.OverflowEnum.hidden;
                    //        (div.style as dynamic).zIndex = "";

                    //        div.style.position = IStyle.PositionEnum.relative;
                    //        div.style.left = "";
                    //        div.style.top = "";
                    //        div.style.right = "";
                    //    }
                    //);

                   c.style.position = IStyle.PositionEnum.relative;
                    c.style.height = "20em";

                    c.Clear();
                    g.AttachControlTo(c);

                    return g;
                };
            #endregion


            bind("Assets", page.assets);
            bind("Transactions", page.transactions).ReadOnly = true;

            // script: error JSC1000: No implementation found for this native method, please implement [System.Data.DataTableCollection.get_Item(System.String)]
            var data_Assets = data.Tables["Assets"];
            var data_Transactions = data.Tables["Transactions"];


            #region data_Assets_NewRow
            Action<DataRow> data_Assets_NewRow =
                 SourceRow =>
                 {

                     var r = new Random();

                     new sack_of_gold2().Source.Task.ContinueWithResult(
                        o =>
                        {
                            o.position.y = -80;
                            scene.add(o);
                            oo.Add(o);

                            o.position.x = r.Next(-250, 250);
                            o.position.z = r.Next(-400, 200);
                            (o as dynamic).scale = new THREE.Vector3(0.5, 0.5, 0.5);

                            data_Assets.RowDeleting +=
                                (sender, e) =>
                                {
                                    if (SourceRow != e.Row)
                                        return;

                                    scene.remove(o);
                                    oo.Remove(o);

                                    data_Transactions.Rows.Add(
                                        "now", "item removed -" + SourceRow["Net worth"]
                                    );

                                };


                        }
                    );

                 };
            #endregion


            data_Assets.Rows.AsEnumerable().WithEach(data_Assets_NewRow);

            // "X:\jsc.svn\examples\javascript\DropFileIntoSQLite\DropFileIntoSQLite.sln"
            // X:\jsc.svn\examples\javascript\DragDataTableIntoCSVFile\DragDataTableIntoCSVFile\Application.cs
            #region ondragstart
            page.header.ondragstart +=
                e =>
                {
                    data_Assets.Rows.AsEnumerable().FirstOrDefault().With(
                        SourceRow =>
                        {
                            // x:\jsc.svn\examples\javascript\dropfileintosqlite\dropfileintosqlite\application.cs

                            data_Assets.Rows.Remove(SourceRow);
                            //data_Assets.Rows.RemoveAt(0);


                            var clipboard = new DataTable();

                            clipboard.Columns.AddRange(
                                Enumerable.ToArray(
                                    from x in data_Assets.Columns.AsEnumerable()
                                    select new DataColumn { ColumnName = x.ColumnName }
                                )
                            );

                            clipboard.Rows.Add(
                                Enumerable.ToArray(
                                    from x in data_Assets.Columns.AsEnumerable()
                                    select SourceRow[x]
                                )
                            );

                            e.dataTransfer.effectAllowed = "copy";

                            var clipboard_string = StringConversionsForDataTable.ConvertToString(clipboard);
                            e.dataTransfer.setData(typeof(DataTable).Name, clipboard_string);
                        }
                    );

                };
            #endregion


            // X:\jsc.svn\market\javascript\Abstractatech.JavaScript.FileStorage\Abstractatech.JavaScript.FileStorage\Application.cs
            var dz = new DropZone();


            var TimerHide = new ScriptCoreLib.JavaScript.Runtime.Timer(
                  delegate
                  {
                      dz.body.Orphanize();
                  }
              );

            #region ondragover
            Action<DragEvent> ondragover =
                evt =>
                {


                    evt.stopPropagation();
                    evt.preventDefault();


                    if (evt.dataTransfer.types.Contains(typeof(DataTable).Name.ToLower()))
                    {


                        evt.dataTransfer.dropEffect = "copy"; // Explicitly show this is a copy.

                        dz.body.AttachTo(Native.document.documentElement);
                        dz.bglayer.style.transition = "background-color 500ms linear";
                        dz.bglayer.style.backgroundColor = "rgba(0,0,0, 0.7)";

                        TimerHide.Stop();
                    }

                };

            Native.Document.body.ondragover += ondragover;
            dz.Container.ondragover += ondragover;
            #endregion



            dz.Container.ondragleave +=
                 evt =>
                 {
                     //Console.WriteLine("ondragleave");

                     //Console.WriteLine(" dz.Container.ondragleave");
                     TimerHide.StartTimeout(90);

                     evt.stopPropagation();
                     evt.preventDefault();

                 };

            Native.window.onblur +=
                delegate
                {
                    data_Transactions.Rows.Add(
                        //"now", "item added +" + SourceRow["Net worth"]
                        "now", "blur"
                    );

                };

            Native.window.onfocus +=
                delegate
                {
                    data_Transactions.Rows.Add(
                        //"now", "item added +" + SourceRow["Net worth"]
                        "now", "focus"
                    );

                };

            data_Assets_NewRow +=
                SourceRow =>
                {
                    data_Transactions.Rows.Add(
                        //"now", "item added +" + SourceRow["Net worth"]
                        "now", "item added"
                    );
                };

            data_Assets.TableNewRow +=
                (sender, e) =>
                {
                    data_Assets_NewRow(e.Row);
                };


            #region ondrop
            dz.Container.ondrop +=
                evt =>
                {
                    //Console.WriteLine("ondrop");

                    TimerHide.StartTimeout(90);

                    evt.stopPropagation();
                    evt.preventDefault();

                    if (evt.dataTransfer.items != null)
                    {
                        // X:\jsc.svn\examples\javascript\DragDataTableIntoCSVFile\DragDataTableIntoCSVFile\Application.cs

                        evt.dataTransfer.items.AsEnumerable().Where(
                            x =>

                                x.type.ToLower() ==

                                // let jsc type system sort it out?
                                // how much reflection does jsc give us nowadays?
                                typeof(DataTable).Name.ToLower()

                        ).WithEach(
                            async xx =>
                            {
                                // http://www.whatwg.org/specs/web-apps/current-work/multipage/dnd.html#dfnReturnLink-0
                                var DataTable_xml = await xx.getAsString();

                                var DataTable = StringConversionsForDataTable.ConvertFromString(DataTable_xml);

                                DataTable.Rows.AsEnumerable().WithEach(
                                    SourceRow =>
                                    {

                                        data_Assets.Rows.Add(
                                            Enumerable.ToArray(
                                                from x in data_Assets.Columns.AsEnumerable()
                                                select SourceRow[x]
                                            )
                                        );
                                    }
                                );
                            }
                        );
                    }
                };
            #endregion


        }

    }

    public static class X
    {
        public static IEnumerable<File> AsEnumerable(this FileList f)
        {
            return Enumerable.Range(0, (int)f.length).Select(k => f[(uint)k]);
        }

        public static IEnumerable<DataTransferItem> AsEnumerable(this DataTransferItemList f)
        {
            return Enumerable.Range(0, (int)f.length).Select(k => f[(uint)k]);
        }
    }
}
