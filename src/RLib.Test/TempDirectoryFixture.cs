using System;
using NUnit.Framework;
using System.IO;
using System.Reflection;

namespace RLib
{
	[TestFixture]
	public class TempDirectoryFixture
	{
		[Test]
		public void Constructor_NoParent_CreatesTempDirectory()
		{
			using (var tempDir = new TempDirectory())
			{	
				Assert.IsTrue(Directory.Exists(tempDir));
			}
		}
		
		[Test]
		public void Constructor_ParentDir_CreatesTempDirectoryInParentDirectory()
		{
			var parentPath = Path.GetDirectoryName(Assembly.GetAssembly(typeof(TempDirectoryFixture)).Location);
			var parentDir = new DirectoryInfo(parentPath);
			
			using (var tempDir = new TempDirectory(parentDir))
			{
				var tempDirInfo = new DirectoryInfo(tempDir);
				Assert.AreEqual(tempDirInfo.Parent.FullName, parentDir.FullName);
			}
		}
	}
}

