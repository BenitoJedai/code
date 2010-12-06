﻿using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.DOM;
using System.Collections.Generic;
using ScriptCoreLib.JavaScript.Runtime;
using System;
using System.Linq;
using HyperDesign.HTML.Pages;


namespace HyperDesign.js
{
	[Script, ScriptApplicationEntryPoint]
	public class HyperDesign
	{
		public HyperDesign()
		{
			Native.Document.body.style.overflow = IStyle.OverflowEnum.auto;

			var g1 = new ThreeDGroup1();

			//WriteAnimations();

			g1.Images.ToGoogleThreeDWarehouseImages().Animate();


			g1.Frame1.onload +=
				e =>
				{
					g1.Go1.style.color = Color.Blue;

				};

			g1.Go1.onclick +=
				delegate
				{
					g1.Go1.style.color = Color.Red;
					g1.Frame1.src = "http://google.com";
				};

			g1.Container.AttachToDocument();

			var n = new Application();

			n.Container.AttachToDocument();

			n.Read.onclick +=
				delegate
				{
					n.FirstName.value = "John";
					n.LastName.value = "Doe";
				};

			var Items = new IHTMLDiv();

			Items.style.height = "300px";
			Items.style.overflow = IStyle.OverflowEnum.auto;
			Items.style.backgroundColor = Color.System.ButtonFace;
			Items.style.borderWidth = "2px";
			Items.style.borderStyle = "solid";
			Items.style.borderColor = Color.System.ButtonShadow;

			Items.AttachToDocument();

			var List = new List<Employee>();

			n.Add.onclick +=
				delegate
				{
					var i = new Summary();
					var j = new Employee
					{
						Number = List.Count + 1,
						FirstName = n.FirstName.value,
						LastName = n.LastName.value
					};
					List.Add(j);

					Action Update =
						delegate
						{
							i.Number.value = "#" + j.Number;
							i.FirstName.value = j.FirstName;
							i.LastName.value = j.LastName;
						};

					Update();

					i.Delete.onclick +=
						delegate
						{
							i.Container.Dispose();
						};

					i.Container.AttachTo(Items);

					i.Edit.onclick +=
						delegate
						{
							i.Edit.disabled = true;

							var details = new Details();

							details.FirstName.value = i.FirstName.value;
							details.LastName.value = i.LastName.value;
							details.Bio.value = j.Bio;

							details.Container.AttachTo(i.Details);

							details.Discard.onclick +=
								delegate
								{
									details.Container.Dispose();
									i.Edit.disabled = false;
								};

							details.Save.onclick +=
								delegate
								{
									details.Container.Dispose();
									i.Edit.disabled = false;

									j.Bio = details.Bio.value;
									j.FirstName = details.FirstName.value;
									j.LastName = details.LastName.value;

									Update();
								};
						};
				};
		}

		private static void WriteAnimations()
		{
			Action<string> Write = r => new IHTMLDiv { innerText = r }.AttachToDocument();


			foreach (var rr in ThreeDGroup1.Create().Images)
			{
                var r = rr.src;

				Write(r);

				var u = new Uri(r);

				Write(u.Host); // sketchup.google.com
				Write(u.AbsolutePath);
				Write(u.Query); // /3dwarehouse/download

				foreach (var p in u.Query.Split('&'))
				{
					var kv = p.Split('=');
					if (kv.Length == 2)
					{
						Write(kv[0]);
						Write(kv[1]);

						if (kv[0] == "mid")
						{
							var container = new IHTMLDiv { innerText = "loading..." }.AttachToDocument();
							var frames = Enumerable.ToArray(
								from imagenum in Enumerable.Range(0, 36)
								let e = new { mid = kv[1], imagenum }
								select new IHTMLImage("http://sketchup.google.com/3dwarehouse/download?mid=" + e.mid + "&rtyp=swivel&setnum=0&imagenum=" + e.imagenum)
								{
									width = 40,
									height = 30
								}
							);

							var cc = frames.Last();

							cc.InvokeOnComplete(
								i =>
								{

									var cf = frames.AsCyclicEnumerator();

									new Timer(
										t =>
										{
											cf.MoveNext();
											container.removeChildren();
											cf.Current.AttachTo(container);

										},
										1, 1000 / 24).StartInterval();
								}
							);



						}
					}
				}
			}
		}

		public class Employee
		{
			public int Number;

			public string FirstName;
			public string LastName;

			public string Bio;
		}

		static HyperDesign()
		{
			typeof(HyperDesign).SpawnTo(i => new HyperDesign());
		}

	}

}
