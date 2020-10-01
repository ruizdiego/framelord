
namespace FrameLord
{
	public class Hasher
	{
		/// <summary>
		/// Returns the hash of the specified string
		/// </summary>
		public static byte[] Hash(string str)
		{
			System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
			byte[] toEncode = encoding.GetBytes(str);

			byte b1 = 0;
			byte b2 = 0;

			int len = toEncode.Length / 2;

			for (int i = 0; i < len; i++)
			{
				b1 ^= toEncode[i * 2];
				b2 ^= toEncode[i * 2 + 1];
			}

			string str1 = b1.ToString();
			len = 3 - str1.Length;
			for (int i = 0; i < len; i++)
			{
				str1 = "0" + str1;
			}

			string str2 = b2.ToString();
			len = 3 - str2.Length;
			for (int i = 0; i < len; i++)
			{
				str2 = "0" + str2;
			}

			string randomNum1 = UnityEngine.Random.Range(0, 999).ToString();
			len = 3 - randomNum1.Length;
			for (int i = 0; i < len; i++)
			{
				randomNum1 = "0" + randomNum1;
			}

			string randomNum2 = UnityEngine.Random.Range(0, 999).ToString();
			len = 3 - randomNum2.Length;
			for (int i = 0; i < len; i++)
			{
				randomNum2 = "0" + randomNum2;
			}

			string strOut = randomNum1 + str1 + randomNum2 + str2;

			return encoding.GetBytes(strOut);
		}
	}
}
