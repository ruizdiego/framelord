// Mono Framework
using System;

namespace FrameLord
{
	/// <summary>
	/// File utility class
	/// </summary>
	public static class FileUtil
	{
		/// <summary>
		/// Returns the file path of the specified file
		/// </summary>
		public static string GetFilePath(string strFilename)
		{
			int idx = strFilename.LastIndexOf("/", StringComparison.InvariantCulture);

			if (idx == -1)
				idx = strFilename.LastIndexOf("\\", StringComparison.InvariantCulture);

			if (idx == -1)
				return "";

			return strFilename.Substring(0, idx);

		}

		/// <summary>
		/// Returns just the filename of a fill specified file
		/// </summary>
		public static string GetFileName(string strFilename)
		{
			int idx = strFilename.LastIndexOf("/", StringComparison.InvariantCulture);

			if (idx == -1)
				idx = strFilename.LastIndexOf("\\", StringComparison.InvariantCulture);

			if (idx == -1)
				return strFilename;

			return strFilename.Substring(idx + 1, strFilename.Length - idx - 1);

		}

		/// <summary>
		/// Extracts from the file name, the file extension 
		/// </summary>
		public static string ExtractFileExt(string strFilename)
		{
			int idx = strFilename.LastIndexOf(".", StringComparison.InvariantCulture);

			if (idx != -1)
				return strFilename.Substring(0, idx);
			else
				return strFilename;
		}

		/// <summary>
		/// Changes the file extension
		/// </summary>
		public static string ChangeFileExt(string strFilename, string strExt)
		{
			int idx = strFilename.LastIndexOf('.');

			if (idx != 1)
				return strFilename.Substring(0, idx) + strExt;
			else
				return strFilename;
		}

		/// <summary>
		/// Returns the file extension
		/// </summary>
		public static string GetFileExt(string strFilename)
		{
			int idx = strFilename.LastIndexOf('.');

			if (idx != 1)
				return strFilename.Substring(idx, strFilename.Length - idx);
			else
				return strFilename;
		}
	}
}

