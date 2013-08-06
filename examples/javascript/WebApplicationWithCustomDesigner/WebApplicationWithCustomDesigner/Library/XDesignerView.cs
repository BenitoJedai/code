using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsWebServiceWithDesigner.Library
{
    public partial class XDesignerView : UserControl
    {

        public XComponentDesigner Designer { get; set; }


        public XDesignerView()
        {
            InitializeComponent();
        }

        private void XDesignerView_Load(object sender, EventArgs e)
        {
            this.webBrowser1.DocumentText = @"
<html>
	<head>
		<title>App</title>
	</head>
	<body>
		<noscript>Error: This Application requires JavaScript.</noscript>
		<div id='PageContainer'>
			<h1 id='Header'>JSC - The .NET crosscompiler for web platforms.</h1>
			<p id='Content' style='padding: 2em;
				color: blue;'>Hello world</p>
		</div>
	</body>
</html>

";

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
