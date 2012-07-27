using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RLib
{
	public sealed class TempDirectory : IDisposable
	{
		private readonly string path;

		public TempDirectory()
		{
			path = Path.Combine(
                Path.GetTempPath(),
                Guid.NewGuid().ToString()
            );
			Directory.CreateDirectory(path);
		}
		
		public TempDirectory(DirectoryInfo parentDirectory)
		{
			if(parentDirectory == null) {
				throw new ArgumentNullException("parentDirectory");
			}
			
			path = Path.Combine(
				parentDirectory.FullName,
				Guid.NewGuid().ToString());
			
			Directory.CreateDirectory(path);
		}

		/// <summary>
		/// Allows the TempDirectory to be used anywhere a string is required.
		/// </summary>
		/// <param name="directory"></param>
		/// <returns></returns>
		public static implicit operator string(TempDirectory directory)
		{
			return directory.path;
		}

		public override string ToString()
		{
			return path;
		}

		public void Dispose()
		{
			ActionRunner.RunSafely(() => Directory.Delete(path, true));
		}
	}
}
