package OtherFooNamespace.InnerFooNamespace
{
	import OtherFooNamespace.InnerFooNamespace.More.Foo;
	
	public class Bar
	{
		public var Foo1:OtherFooNamespace.InnerFooNamespace.Foo;
		public var Foo2:OtherFooNamespace.InnerFooNamespace.More.Foo;

		public function get PropertyFoo1():OtherFooNamespace.InnerFooNamespace.Foo{ return Foo1; }
		public function get PropertyFoo2():OtherFooNamespace.InnerFooNamespace.More.Foo{ return Foo2; }

		function GetString():String
		{
			return "Bar";
		}
		
	}
}