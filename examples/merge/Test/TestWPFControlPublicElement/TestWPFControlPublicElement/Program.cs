using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestWPFControlPublicElement
{
	class Program
	{
		//		Severity Code    Description Project File Line
		//Error		'IComponentConnector' in explicit interface declaration is not an interface TestWPFControlPublicElement X:\jsc.svn\examples\merge\test\TestWPFControlPublicElement\TestWPFControlPublicElement\obj\Debug\UserControl1.g.cs	78
		//Error CS0538	'IComponentConnector' in explicit interface declaration is not an interface TestWPFControlPublicElement X:\jsc.svn\examples\merge\test\TestWPFControlPublicElement\TestWPFControlPublicElement\obj\Debug\UserControl1.g.i.cs	78
		//Error The type 'UserControl' does not support direct content.TestWPFControlPublicElement X:\jsc.svn\examples\merge\test\TestWPFControlPublicElement\TestWPFControlPublicElement\UserControl1.xaml	9
		//Error The type name 'IComponentConnector' could not be found in the namespace 'System.Windows.Markup'. This type has been forwarded to assembly 'System.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089' Consider adding a reference to that assembly.TestWPFControlPublicElement X:\jsc.svn\examples\merge\test\TestWPFControlPublicElement\TestWPFControlPublicElement\obj\Debug\UserControl1.g.cs	41

		[STAThread]
		static void Main(string[] args)
		{
			// X:\jsc.svn\examples\merge\test\TestWPFControlPublicElement\TestWPFControlPublicElement\obj\Debug\UserControl1.g.cs
			// 

			var u = new UserControl1();

			// http://stackoverflow.com/questions/2093855/public-class-modifier-for-wpf-control


			// Severity	Code	Description	Project	File	Line
			//Error The type 'UserControl' does not support direct content.TestWPFControlPublicElement X:\jsc.svn\examples\merge\test\TestWPFControlPublicElement\TestWPFControlPublicElement\UserControl1.xaml    9

			//u.button
			u.button.Content = "hello";

			var w = new Window();

			w.Content = u;

			w.ShowDialog();
		}
	}
}
