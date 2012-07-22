using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

namespace Mvc
{
	public class EmbeddedResourcePathProvider : VirtualPathProvider
	{
		public override bool FileExists(string virtualPath)
		{
			return EmbeddedFileExists(virtualPath) || Previous.FileExists(virtualPath);
		}

		private static bool EmbeddedFileExists(string virtualPath)
		{
			var dir = HostingEnvironment.MapPath(Path.GetDirectoryName(virtualPath));
			if (!File.Exists(dir))
				return false;
			try
			{
				var assembly = Assembly.LoadFile(dir);
				return assembly.GetManifestResourceNames().Contains(Path.GetFileName(virtualPath));
			}
			catch
			{
				return false;
			}
		}

		private class EmbeddedVirtualFile : VirtualFile
		{
			public EmbeddedVirtualFile(string virtualPath) : base(virtualPath)
			{
			}

			public override Stream Open()
			{
				var dir = HostingEnvironment.MapPath(Path.GetDirectoryName(VirtualPath));
				var assembly = Assembly.LoadFile(dir);
				return assembly.GetManifestResourceStream(Path.GetFileName(VirtualPath));
			}
		}

		public override VirtualFile GetFile(string virtualPath)
		{
			return 
				EmbeddedFileExists(virtualPath) 
				? new EmbeddedVirtualFile(virtualPath) 
				: Previous.GetFile(virtualPath);
		}

		public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
		{
			return
				EmbeddedFileExists(virtualPath)
				? new CacheDependency(new[]{HostingEnvironment.MapPath(Path.GetDirectoryName(virtualPath))})
				: Previous.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
		}
	}
}