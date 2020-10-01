// Mono Framework
using System;
using System.Collections;
using System.Collections.Generic;

// Unity Framework
using UnityEngine;

namespace FrameLord
{
//Utilities to read / write data from the structures used by the json serializer.
//We use a "Hashtable" when writing and a "Hashtable" when reading 
    public class MiniJSONUtils
    {
        static public string GetString(Hashtable ht, string key, string defaultValue)
        {
            try
            {
                if (ht.ContainsKey(key))
                    return Convert.ToString(ht[key]);
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.ToString());
            }

            return defaultValue;
        }

        static public void SetString(Hashtable ht, string key, string value)
        {
            ht.Add(key, value);
        }

        static public void SetStringIfNotDefault(Hashtable ht, string key, string value, string defaultValue)
        {
            if (value != defaultValue)
                ht.Add(key, value);
        }

        static public int GetInt32(Hashtable ht, string key, int defaultValue)
        {
            try
            {
                if (ht.ContainsKey(key))
                    return Convert.ToInt32(ht[key]);
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.ToString());
            }

            return defaultValue;
        }

        static public void SetInt32(Hashtable ht, string key, int value)
        {
            ht.Add(key, value);
        }

        static public void SetInt32IfNotDefault(Hashtable ht, string key, int value, int defaultValue)
        {
            if (value != defaultValue)
                ht.Add(key, value);
        }

        static public long GetInt64(Hashtable ht, string key, long defaultValue)
        {
            try
            {
                if (ht.ContainsKey(key))
                    return Convert.ToInt64(ht[key]);
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.ToString());
            }

            return defaultValue;
        }

        static public void SetInt64(Hashtable ht, string key, long value)
        {
            ht.Add(key, value);
        }

        static public void SetInt64IfNotDefault(Hashtable ht, string key, long value, int defaultValue)
        {
            if (value != defaultValue)
                ht.Add(key, value);
        }

        static public bool GetBool(Hashtable ht, string key, bool defaultValue)
        {
            if (ht.ContainsKey(key))
            {
                if (ht[key] is bool)
                    return (bool) ht[key];
                else if (string.IsNullOrEmpty(ht[key].ToString()) == false)
                    return Convert.ToInt32(ht[key]) != 0;
            }

            return defaultValue;
        }

        static public void SetBool(Hashtable ht, string key, bool value)
        {
            ht.Add(key, value);
        }

        static public void SetBoolIfNotDefault(Hashtable ht, string key, bool value, bool defaultValue)
        {
            if (value != defaultValue)
                SetBool(ht, key, value);
        }

        static public void SetBoolAsInt(Hashtable ht, string key, bool value)
        {
            SetInt32(ht, key, value ? 1 : 0);
        }

        static public void SetBoolAsIntIfNotDefault(Hashtable ht, string key, bool value, bool defaultValue)
        {
            if (value != defaultValue)
                SetBoolAsInt(ht, key, value);
        }

        static public ArrayList GetArrayList(Hashtable ht, string key, ArrayList defaultValue)
        {
            if (ht.ContainsKey(key) && ht[key] is ArrayList)
                return (ArrayList) ht[key];

            return defaultValue;
        }

        static public void SetArrayList(Hashtable ht, string key, ArrayList value)
        {
            ht.Add(key, value);
        }

        static public List<int> GetListInt32(Hashtable ht, string key, List<int> defaultValue)
        {
            if (ht.ContainsKey(key) && ht[key] is ArrayList)
            {
                ArrayList arrayList = (ArrayList) ht[key];

                List<int> list = new List<int>();

                for (int i = 0; i < arrayList.Count; i++)
                    list.Add(Convert.ToInt32(arrayList[i]));

                return list;
            }

            return defaultValue;
        }

        static public void SetListInt32(Hashtable ht, string key, List<int> values)
        {
            if (values != null)
            {
                ArrayList arrayList = new ArrayList();

                for (int i = 0; i < values.Count; i++)
                    arrayList.Add(values[i]);

                SetArrayList(ht, key, arrayList);
            }
            else
            {
                SetArrayList(ht, key, null);
            }
        }

        static public void SetDictionaryInt32Int32(Hashtable ht, string key, Dictionary<int, int> values)
        {
            Hashtable hashtable = new Hashtable();

            foreach (KeyValuePair<int, int> keyValue in values)
                hashtable.Add(keyValue.Key, keyValue.Value);

            ht[key] = hashtable;
        }

        static public Dictionary<int, int> GetDictionaryInt32Int32(Hashtable ht, string key, Dictionary<int, int> defaultValue)
        {
            if (ht.ContainsKey(key) && ht[key] is Hashtable)
            {
                Dictionary<int, int> dictionary = new Dictionary<int, int>();

                foreach (DictionaryEntry entry in (Hashtable) ht[key])
                    dictionary.Add(Convert.ToInt32(entry.Key), Convert.ToInt32(entry.Value));

                return dictionary;
            }

            return defaultValue;
        }

        static public List<string> GetListString(Hashtable ht, string key, List<string> defaultValue)
        {
            if (ht.ContainsKey(key) && ht[key] is ArrayList)
            {
                ArrayList arrayList = (ArrayList) ht[key];

                List<string> list = new List<string>();

                for (int i = 0; i < arrayList.Count; i++)
                    list.Add(Convert.ToString(arrayList[i]));

                return list;
            }

            return defaultValue;
        }

        static public void SetListString(Hashtable ht, string key, List<string> values)
        {
            ArrayList arrayList = new ArrayList();

            for (int i = 0; i < values.Count; i++)
                arrayList.Add(values[i]);

            SetArrayList(ht, key, arrayList);
        }

        static public void SetBytes(Hashtable ht, string key, byte[] value)
        {
            ht.Add(key, StringUtil.EncodeTo64(value));
        }

        static public byte[] GetBytes(Hashtable ht, string key, byte[] defaultValue)
        {
            try
            {
                if (ht.ContainsKey(key) && ht[key] is string)
                    return StringUtil.DecodeFrom64ToByteArray((string) ht[key]);
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.ToString());
            }

            return defaultValue;
        }

        static public Hashtable GetJson(Hashtable ht, string key, Hashtable defaultValue)
        {
            try
            {
                if (ht.ContainsKey(key) && ht[key] is Hashtable)
                    return (Hashtable) ht[key];
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.ToString());
            }

            return defaultValue;
        }

        static public void SetJson(Hashtable ht, string key, Hashtable value)
        {
            ht.Add(key, value);
        }
    }

}