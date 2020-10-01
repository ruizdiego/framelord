// .NET Framework
using System.IO;
using System.IO.Compression;
using System.Text;

namespace FrameLord
{
	/// <summary>
	/// Compression/Decompression utility class
	/// </summary>
	public class ZipUnZipUtil
	{
		/// <summary>
		/// Copy from one stream to other
		/// </summary>
		static void CopyTo(Stream src, Stream dest)
		{
			byte[] bytes = new byte[4096];

			int cnt;

			while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
			{
				dest.Write(bytes, 0, cnt);
			}
		}

		/// <summary>
		/// Compress the specified string and return it as a byte array
		/// </summary>
		public static byte[] Zip(string str)
		{
			var bytes = Encoding.UTF8.GetBytes(str);

			using (var msi = new MemoryStream(bytes))
			using (var mso = new MemoryStream())
			{
				using (var gs = new GZipStream(mso, CompressionMode.Compress))
				{
					CopyTo(msi, gs);
				}

				return mso.ToArray();
			}
		}

		/// <summary>
		/// Decompress the specified byte array and return is as a string
		/// </summary>
		public static string Unzip(byte[] bytes)
		{
			using (var msi = new MemoryStream(bytes))
			using (var mso = new MemoryStream())
			{
				using (var gs = new GZipStream(msi, CompressionMode.Decompress))
				{
					CopyTo(gs, mso);
				}

				return Encoding.UTF8.GetString(mso.ToArray());
			}
		}
	}
}