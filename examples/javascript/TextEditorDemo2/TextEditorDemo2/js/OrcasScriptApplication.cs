using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Controls;
using System;
using System.Collections.Generic;


namespace TextEditorDemo2.js
{
	[Script, ScriptApplicationEntryPoint]
	public class TextEditorDemo2
	{
		public TextEditorDemo2()
		{
			var n = new Pages.MyEditor();

			n.Container.AttachToDocument();

			//var a = new IHTMLAnchor(
			//    "http://sketchup.google.com/3dwarehouse/search?q=stargate",
			//    "Open 3dwarehouse in another window"
			//);
			//a.style.fontSize = "large-xx";
			//a.AttachToDocument();



			//ii.setAttribute("src", "http://sketchup.google.com/3dwarehouse/");


			IHTMLDiv Control = new IHTMLDiv();

			n.Edit1.parentNode.replaceChild(Control, n.Edit1);

			n.Logo.src = "assets/TextEditorDemo2/Preview.png";

			//Control.AttachToDocument();


			var text = new TextEditor(Control);

	
						text.InnerHTML = "Drag images to this frame!<hr />";
		
			// IE error

			text.Height = 200;
			text.Width = 400;
			//text.InnerHTML = n.Edit1.value;

			text.IsFadeEnabled = false;

			var i = new IHTMLImage(21, 20) { src = "assets/TextEditorDemo2/cal.png" };

			var CurrentList = new List<Extensions.GoogleThreeDWarehouseImage>();

			n.ToLarge.onclick += e =>
				{
					CurrentList.ForEach(k => k.AnimationZoom = 4);
				};

			n.ToMedium.onclick += e =>
				{
					CurrentList.ForEach(k => k.AnimationZoom = 1);
				};
			n.ToSmall.onclick += e =>
				{
					CurrentList.ForEach(k => k.AnimationZoom = 0.5);
				};

			Action<string[], IHTMLButton> ToPreview =
				(data, button) =>
				{
					var ii = new IHTMLImage(40, 30) { src = data[0] };
					ii.style.verticalAlign = "middle";

					var sp = new IHTMLSpan();
					sp.style.marginLeft = "1em";
					sp.AttachTo(button);

					ii.AttachTo(button).ToGoogleThreeDWarehouseImage().Animate();

				};
			n.Nasa.onclick +=
				delegate
				{
					text.InnerHTML = Pages.Nasa.Static.HTML;
				};


			n.Houses.onclick +=
				delegate
				{
					text.InnerHTML = Pages.Houses.Static.HTML;

				};

			n.CnC.onclick +=
				delegate
				{
					text.InnerHTML = Pages.CnC.Static.HTML;

				};

			n.Ships.onclick +=
			delegate
			{
				text.InnerHTML = Pages.Ships.Static.HTML;
			};

			ToPreview(Pages.Ships.Static.Images, n.Ships);
			ToPreview(Pages.CnC.Static.Images, n.CnC);
			ToPreview(Pages.Houses.Static.Images, n.Houses);
			ToPreview(Pages.Nasa.Static.Images, n.Nasa);

			//i.AttachToDocument();
			n.OK.onclick +=
				delegate
				{
					n.ContainerForImages.removeChildren();
					CurrentList.Clear();
					//text.Document.getElementsByTagName("img").ToGoogleThreeDWarehouseImages().Animate();

					var clones = text.Document.GetClonedImages();
					foreach (IHTMLImage iii in clones)
					{
					

						var w = iii.AttachTo(n.ContainerForImages).ToGoogleThreeDWarehouseImage();

						w.Animate();

						CurrentList.Add(w);
					}

				};
			//);

			//OK.Control.style.paddingLeft = "1em";
			//text.BottomToolbarContainer.appendChild(OK.Control);

			text.TopToolbarContainer.Hide();
			text.BottomToolbarContainer.Hide();

		}


		static TextEditorDemo2()
		{
			typeof(TextEditorDemo2).SpawnTo(i => new TextEditorDemo2());
		}

	}

}
