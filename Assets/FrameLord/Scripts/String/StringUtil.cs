// Mono Framework
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

// Unity Framework
using UnityEngine;


namespace FrameLord
{
	/// <summary>
	/// A collection of helpers to manage strings.
	/// </summary>
	public class StringUtil
	{
		/// <summary>
		/// Compare the specified str1, str2 and ignoreCase.
		/// </summary>
		public static int Compare(string str1, string str2, bool ignoreCase)
		{
			if (ignoreCase)
			{
				return string.Compare(str1, str2, CultureInfo.InvariantCulture, CompareOptions.IgnoreCase);
			}
			else
			{
				return string.Compare(str1, str2, StringComparison.InvariantCulture);
			}
		}

		/// <summary>
		/// Compare the specified str1 and str2.
		/// </summary>
		public static int Compare(string str1, string str2)
		{
			return string.Compare(str1, str2, CultureInfo.InvariantCulture, CompareOptions.IgnoreCase);
		}

		/// <summary>
		/// Encodes the data to base64
		/// </summary>
		public static string EncodeTo64(string toEncode)
		{
			byte[] toEncodeAsBytes = System.Text.Encoding.ASCII.GetBytes(toEncode);
			return System.Convert.ToBase64String(toEncodeAsBytes);
		}

		/// <summary>
		/// Encodes the data to base64
		/// </summary>
		public static string EncodeTo64(byte[] toEncode)
		{
			return System.Convert.ToBase64String(toEncode);
		}

		/// <summary>
		/// Decodes the data from base64
		/// </summary>
		public static string DecodeFrom64(string encodedData)
		{
			byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);
			string str = System.Text.Encoding.ASCII.GetString(encodedDataAsBytes);

			return str;
		}

		/// <summary>
		/// Decodes the data from base64 to a byte array
		/// </summary>
		public static byte[] DecodeFrom64ToByteArray(string encodedData)
		{
			if (encodedData != null)
				return System.Convert.FromBase64String(encodedData);
			else
				return null;
		}

		/// <summary>
		/// Returns DateTime.Now in a fixed format (DD/MM/YYYY)
		/// </summary>
		public static string GetTimeNow()
		{
			return string.Format("{0}/{1}/{2}", System.DateTime.Now.Day, System.DateTime.Now.Month, System.DateTime.Now.Year);
		}

		/// <summary>
		/// Returns the specified date in a fixed format (DD/MM/YYYY)
		/// </summary>
		public static string FromDateToString(System.DateTime date)
		{
			return string.Format("{0}/{1}/{2}", date.Day, date.Month, date.Year);
		}

		/// <summary>
		/// The date format should be the returned by GetTimeNow (DD/MM/YYYY)
		/// </summary>
		public static System.DateTime FromStringToDate(string date)
		{
			string[] dateArr = date.Split(new char[] {'/'});

			if (dateArr != null && dateArr.Length == 3)
			{
				return new System.DateTime(System.Convert.ToInt32(dateArr[2]), System.Convert.ToInt32(dateArr[1]), System.Convert.ToInt32(dateArr[0]));
			}
			else
			{
				return System.DateTime.MinValue;
			}
		}

		/// <summary>
		/// Format a integer using the specified decSep character as decimal separator
		/// </summary>
		public static string FormatNumbers(int num, string decSep)
		{
			if (num <= 999)
			{
				return num.ToString();
			}
			else if (num > 999 && num <= 999999)
			{
				int p1 = num % 1000;
				int p2 = (int) (num / 1000);

				return string.Format("{0}{1}{2:000}", p2, decSep, p1);
			}
			else
			{
				int p1 = num % 1000;
				int p2 = (int) (num / 1000);
				int p3 = p2 % 1000;
				int p4 = (int) (p2 / 1000);

				return string.Format("{0}{1}{2:000}{3}{4:000}", p4, decSep, p3, decSep, p1);
			}
		}

		public static string IntToHMS(long seconds)
		{
			string str = "";

			long secs = seconds;
			long mins = (seconds / 60);
			long hours = (mins / 60);
			long days = hours / 24;

			secs %= 60;
			mins %= 60;
			hours %= 24;

			if (days > 0)
			{
				str = days + "D ";

				if (hours > 0)
					str += hours.ToString("00") + "H";
			}
			else if (hours > 0)
			{
				str = hours + "H ";

				if (mins > 0)
					str += mins.ToString("00") + "M";
			}
			else if (mins > 0)
			{
				str = mins + "M ";

				if (secs > 0)
					str += secs.ToString("00") + "S";
			}
			else
			{
				str = secs + "S";
			}

			//Debug.Log("Seconds: " + seconds + " - " + hours.ToString("00") + ":" + mins.ToString("00") + ":" + secs.ToString("00"));

			//str = hours.ToString("00") + ":" + mins.ToString("00") + ":" + secs.ToString("00");

			return str;
		}

		public static string ReplaceDirectorySeparatorChar(string dir)
		{
			return dir.Replace('/', Path.DirectorySeparatorChar);
		}

		public static string ASCIIGetString(byte[] bytes)
		{
#if UNITY_IPHONE || UNITY_ANDROID
			return System.Text.ASCIIEncoding.ASCII.GetString(bytes);
#else
        System.Text.UTF8Encoding enconding = new System.Text.UTF8Encoding();
        return enconding.GetString(bytes, 0, bytes.Length);
#endif
		}

		public static byte[] ASCIIGetBytes(string str)
		{
#if UNITY_IPHONE || UNITY_ANDROID
			System.Text.ASCIIEncoding enconding = new System.Text.ASCIIEncoding();
			return enconding.GetBytes(str);
#else
        System.Text.UTF8Encoding enconding = new System.Text.UTF8Encoding();
        return enconding.GetBytes(str);
#endif
		}

		static public string JoinIntArray(int[] vals, string separator = ";")
		{
			string str = "";
			for (int i = 0; i < vals.Length; i++)
			{
				if (str != "")
					str += separator;
				str += vals[i].ToString();
			}

			return str;
		}

		static public string JoinIntArray(List<int> vals, string separator = ";")
		{
			string str = "";
			for (int i = 0; i < vals.Count; i++)
			{
				if (str != "")
					str += separator;
				str += vals[i].ToString();
			}

			return str;
		}

		static public string JoinStringArray(string[] strArray, string separator = ";")
		{
			string str = "";
			for (int i = 0; i < strArray.Length; i++)
			{
				if (str != "")
					str += separator;
				str += strArray[i];
			}

			return str;
		}

		static public int[] SplitIntArray(string s, char separator = ';')
		{
			string[] strs = s.Split(new char[] {separator}, StringSplitOptions.RemoveEmptyEntries);
			int[] ints = new int[strs.Length];

			for (int i = 0; i < strs.Length; i++)
				if (!int.TryParse(strs[i], out ints[i]))
					ints[i] = 0;

			return ints;
		}

		static public string[] SplitStringArray(string s, char separator = ';')
		{
			string[] strs = s.Split(new char[] {separator}, StringSplitOptions.RemoveEmptyEntries);

			return strs;
		}

		public const string postfix = "illion";

		public static string[] prefix =
		{
			"M", "B", "Tr", "Quadr", "Quint", "Sext", "Sept", "Oct", "Non", "Dec", "Vigint", "Trigint", "Quadragint", "Quinquagint", "Sexagint", "Septuagint", "Octogint",
			"Nonagint", "Cent"
		};

		public static string[] preprefix = {"Un", "Duo", "Tre", "Quattuor", "Quin", "Sex", "Septen", "Octo", "Novem"};

		public static string[] prefixAbbreviation = {"M", "B", "t", "q", "Q", "s", "S", "o", "n", "d", "V", "T", "qq", "Qq", "ss", "Ss", "O", "N", "C"};
		public static string[] prePrefixAbbreviation = {"U", "D", "T", "q", "Q", "s", "S", "O", "N"};

		static public string DoubleToTextNum(double b)
		{
			string str = "";

			int zerosCount = GetZerosCount(b);

			double d = b;

			if (zerosCount > 308)
			{
				str = "MAX";
			}
			else if (zerosCount < 6)
			{
				str = d.ToString("N0");
			}
			else //if (zerosCount >= 6)
			{
				str = d.ToString("N0");

				int firstCommaIndex = str.IndexOf(',');

				string pre = str.Substring(0, firstCommaIndex);

				int secondCommaIndex = str.IndexOf(',', firstCommaIndex + 1);

				string post = str.Substring(firstCommaIndex + 1, secondCommaIndex - firstCommaIndex - 1);

				str = pre + "." + post;
			}

			return str;
		}

		static public string DoubleToTextUnit(double b, bool abbreviation = false)
		{
			string str = "";

			int zerosCount = GetZerosCount(b);

			if (zerosCount > 308)
			{
				str = "";
			}
			else if (zerosCount < 6)
			{
				str = "";
			}
			else if (zerosCount >= 6 && zerosCount < 36)
			{
				int id = Mathf.FloorToInt((zerosCount - 3) / 3 - 1);

				if (abbreviation)
					str = prefixAbbreviation[id].Substring(0, 1); // + postfix;
				else
					str = prefix[id] + postfix;
			}
			else if (zerosCount >= 36)
			{
				int id = Mathf.FloorToInt((zerosCount - 36) / 3) % 10;
				int id2 = Mathf.FloorToInt(zerosCount / 33 - 1) + 9;

				if (zerosCount >= 306)
					id2 += 1;

				if (id >= 9)
				{
					id2 += 1;

					if (abbreviation)
						str = prefixAbbreviation[id2].Substring(0, 1); // + postfix;
					else
						str = prefix[id2] + postfix;
				}
				else
				{
					if (abbreviation)
						str = prePrefixAbbreviation[id].Substring(0, 1) + prefixAbbreviation[id2].Substring(0, 1);
					else
						str = preprefix[id] + prefix[id2].ToLower() + postfix;
				}
			}

			return str;
		}

		static public string DoubleToText(double b, bool abbreviation = false)
		{
			string str = "";

			str = DoubleToTextNum(b);
			string unit = DoubleToTextUnit(b, abbreviation);

			if (!string.IsNullOrEmpty(unit))
				str += " " + unit;

			return str;
		}

		static public int GetZerosCount(double b)
		{
			double d = Math.Abs(b);

			int digits = 0;

			if (d <= 1)
				digits = 1;
			else
				digits = (int) Math.Floor(Math.Log10(d) + 1);

			int zeros = digits - 1;

			return zeros;
		}

		static public string Replace(string stringToSearch, char charToFind, char charToSubstitute)
		{
			char[] chars = stringToSearch.ToCharArray();
			for (int i = 0; i < chars.Length; i++)
				if (chars[i] == charToFind)
					chars[i] = charToSubstitute;

			return new string(chars);
		}

		public static int GetAppVersion()
		{
			string ver = Application.version;
			int idx = ver.IndexOf(".", StringComparison.InvariantCulture);
			if (idx == -1)
			{
				return System.Convert.ToInt32(ver);
			}
			else
			{
				string decver = ver.Substring(idx + 1, ver.Length - idx - 1);
				return System.Convert.ToInt32(decver);
			}
		}
	}
}