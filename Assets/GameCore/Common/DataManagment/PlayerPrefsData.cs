using System.Collections.Generic;
using UnityEngine;

namespace GameCore.DataManagment
{
    public static class PlayerPrefsData
    {
        #region Debug

#if UNITY_EDITOR

        [UnityEditor.MenuItem("Default", menuItem = "SWG/PlayerData/ClearPlayerPrefs")]
        private static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs was cleared!");
        }

#endif

        #endregion Debug

        #region Base

        #region String

        public static string GetString(string key, string defaultValue) => PlayerPrefs.GetString(key, defaultValue);
        public static void SetString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            Save();
        }

        #endregion String

        #region Float

        public static float GetFloat(string key, float defaultValue = 0f) => PlayerPrefs.GetFloat(key, defaultValue);
        public static void SetFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
            Save();
        }

        #endregion Float

        #region Int

        public static int GetInt(string key, int defaultValue = 0) => PlayerPrefs.GetInt(key, defaultValue);
        public static void SetInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            Save();
        }

        #endregion Int

        #region Bool

        public static bool GetBool(string name, bool defaultValue = false) => PlayerPrefs.GetInt(name, defaultValue ? 1 : 0) == 1 ? true : false;
        public static void SetBool(string name, bool value)
        {
            PlayerPrefs.SetInt(name, value ? 1 : 0);
            PlayerPrefs.Save();
        }

        #endregion Bool

        public static void Save()
        {
            PlayerPrefs.Save();
        }

        #endregion Base

        #region Arrays

        public static List<int> GetListInt(string key, string defaultValue, char separator = ':')
        {
            List<int> result = new List<int>();
            string[] values = GetString(key, defaultValue).Split(separator);
            for (int i = 0; i < values.Length; i++)
            {
                result.Add(int.Parse(values[i]));
            }
            return result;
        }

        public static void SetListInt(string key, List<int> list, char separator = ':')
        {
            SetString(key, GetStringByListInt(list));
        }

        public static string GetStringByListInt(List<int> list, char separator = ':')
        {
            string result = "";
            for (int i = 0; i < list.Count; i++)
            {
                result += list[i];
                if (i < list.Count - 1) result += separator;
            }
            return result;
        }

        #endregion Arrays
    }
}