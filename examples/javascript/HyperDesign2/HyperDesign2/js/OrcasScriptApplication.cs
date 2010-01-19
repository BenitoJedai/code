using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.DOM;
using System;
using System.Collections.Generic;


namespace HyperDesign2.js
{
	[Script, ScriptApplicationEntryPoint]
	public class HyperDesign2
	{
		[Script]
		public class Employee
		{
			public int Number;

			public string FirstName;
			public string LastName;

			public string Bio = "No Bio yet!";

			// new data field in the details view

			public string Location = "Default Location";

		}

		public HyperDesign2()
		{
			var n = new Pages.Application();

			n.Container.AttachToDocument();

			n.Read.onclick +=
				delegate
				{
					n.FirstName.value = "John";
					n.LastName.value = "Doe";
				};

			// more code...
			// lets make some changes to the template code...
			// nice huh? :D

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
					var i = new Pages.Summary();
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

							var details = new Pages.Details();

							details.FirstName.value = i.FirstName.value;
							details.LastName.value = i.LastName.value;
							details.Bio.value = j.Bio;
							details.Location.value = j.Location;

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
									j.Location = details.Location.value;

									Update();
								};
						};
				};
		}


		static HyperDesign2()
		{
			typeof(HyperDesign2).SpawnTo(i => new HyperDesign2());
		}

	}

}
