using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ArgumentsViaReflection
{
	class ProgramArguments
	{
		public string Option1 = "hello";
		public string Option2 = "world";

		public int Integer1 = 0;
		public FileInfo File1 = new FileInfo("file1");
		public DirectoryInfo Dir2 = new DirectoryInfo("dir1");

		public class Tag
		{
			public string Name = "Tag";
		}

		public Tag Tag1 = new Tag { Name = "Tag1" };
		public Tag Tag2 = new Tag { Name = "Tag2" };

		public Tag[] Tags;
	}
}
