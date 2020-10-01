// Mono Framework
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

// Unity Framework
using UnityEngine;

namespace FrameLord
{
	public static class LocalStorage
	{
		private const string EncryptionModeKey = "encmod";

		/// <summary>
		/// Decrypt the specified string. hashKey should be setted properly
		/// </summary>
		public static string GetStringSecure(string key, string defVal = null)
		{
			var encryptMode = (EncryptionMode) PlayerPrefs.GetInt(EncryptionModeKey, (int) EncryptionMode.SIMPLE);

			switch (encryptMode)
			{
				case EncryptionMode.PLAIN:
					return GetString(key, defVal);

				case EncryptionMode.SIMPLE:
					if (PlayerPrefs.HasKey(key))
					{
						string cipherStr = PlayerPrefs.GetString(key);
						return SimpleEncryptUtil.DecryptString(cipherStr);
					}
					else
					{
						return defVal;
					}

				case EncryptionMode.RIJNDAE:
					if (PlayerPrefs.HasKey(key))
					{
						string cipherStr = PlayerPrefs.GetString(key);
						return RijndaelEncryptUtil.DecryptString(cipherStr);
					}
					else
					{
						return defVal;
					}
			}

			return null;
		}

		/// <summary>
		/// Returns the string stored in the specified key
		/// </summary>
		public static string GetString(string key, string defVal = null)
		{
			//Debug.LogFormat("GetString. key: {0}", key);
			return PlayerPrefs.GetString(key, defVal);
		}

		/// <summary>
		/// Set a string in the specified key
		/// </summary>
		public static void SetString(string key, string val)
		{
			// Store the encrypted string using PlayerPrefs
			PlayerPrefs.SetString(key, val);
		}

		/// <summary>
		/// Store a string encrypted. hashKey should be setted properly
		/// </summary>
		public static void SetStringSecure(string key, string val)
		{
			var encryptMode = EncryptionMode.SIMPLE;
			PlayerPrefs.SetInt(EncryptionModeKey, (int) EncryptionMode.SIMPLE);

			string cipherStr;

			switch (encryptMode)
			{
				case EncryptionMode.PLAIN:
					SetString(key, val);
					break;

				case EncryptionMode.SIMPLE:
					cipherStr = SimpleEncryptUtil.EncryptString(val);
					PlayerPrefs.SetString(key, cipherStr);
					break;

				case EncryptionMode.RIJNDAE:
					cipherStr = RijndaelEncryptUtil.EncryptString(val);
					PlayerPrefs.SetString(key, cipherStr);
					break;
			}
		}

		/// <summary>
		/// Returns the int stored in the specified key
		/// </summary>
		public static int GetInt(string key, int defValue)
		{
			return PlayerPrefs.GetInt(key, defValue);
		}

		/// <summary>
		/// Set a int in the specified key
		/// </summary>
		public static void SetInt(string key, int val)
		{
			PlayerPrefs.SetInt(key, val);
		}

		/// <summary>
		/// Returns the long stored in the specified key
		/// </summary>
		public static long GetLong(string key, long defValue)
		{
			//string keya = key + "a";
			if (PlayerPrefs.HasKey(key))
			{
				return Convert.ToInt64(PlayerPrefs.GetString(key));

				/*uint partH = (uint) PlayerPrefs.GetInt(keya);
				uint partL = (uint) PlayerPrefs.GetInt(key + "b");
				long res = (partH << 32) | partL;
				
				Debug.LogFormat("GetLong Val1: {0} Val2: {1} -> res: {2}", partH, partL, res);
				
				return res;*/
			}
			else
			{
				return defValue;
			}
		}

		/// <summary>
		/// Set a long in the specified key
		/// </summary>
		public static void SetLong(string key, long val)
		{
			PlayerPrefs.SetString(key, Convert.ToString(val));
		}

		/// <summary>
		/// Return the date stored in the specified key
		/// </summary>
		public static DateTime GetDate(string key, DateTime defValue)
		{
			return new DateTime(GetLong(key, defValue.Ticks));
		}

		/// <summary>
		/// Set a date in the specified key
		/// </summary>
		public static void SetDate(string key, DateTime date)
		{
			SetLong(key, date.Ticks);
		}

		/// <summary>
		/// Returns the float stored in the specified key
		/// </summary>
		public static float GetFloat(string key, float defValue)
		{
			return PlayerPrefs.GetFloat(key, defValue);
		}

		/// <summary>
		/// Set a float in the specified key
		/// </summary>
		public static void SetFloat(string key, float val)
		{
			PlayerPrefs.SetFloat(key, val);
		}

		/// <summary>
		/// Returns the byte array stored in the specified key
		/// </summary>
		public static byte[] GetByteArray(string key, byte[] defValue)
		{
			string val = PlayerPrefs.GetString(key, "");
			if (val == "") return defValue;

			return StringUtil.DecodeFrom64ToByteArray(val);
		}

		/// <summary>
		/// Set a byte array in the specified key
		/// </summary>
		public static void SetByteArray(string key, byte[] val)
		{
			PlayerPrefs.SetString(key, StringUtil.EncodeTo64(val));
		}

		/// <summary>
		/// Delete all the keys and they values
		/// </summary>
		public static void DeleteAll()
		{
			Debug.Log("WARNING. PLAYERPREFS IS BEING REMOVED ---------");

			PlayerPrefs.DeleteAll();
		}

		/// <summary>
		/// Delete the specified key (and its value)
		/// </summary>
		public static void DeleteKey(string key)
		{
			PlayerPrefs.DeleteKey(key);
		}

		/// <summary>
		/// Returns true if the key exists
		/// </summary>
		public static bool HasKey(string key)
		{
			return PlayerPrefs.HasKey(key);
		}

		/// <summary>
		/// Force a save to file
		/// </summary>
		public static void Save()
		{
			PlayerPrefs.Save();
		}

	}
}