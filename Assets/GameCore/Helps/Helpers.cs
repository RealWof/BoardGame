using System.Collections.Generic;
using UnityEngine;

namespace GameCore.Utils
{
    public static class Helpers
    {

        #region id's

        public static int GetCycledID(int id, int length, int add = 1)
        {
            id += add;
            if (id >= length)
            {
                id = 0;
            }
            if (id < 0)
            {
                id = length - 1;
            }
            return id;
        }

        #endregion id's

        #region Arrays

        public static string GetArrayString<T>(string _name, T[] list, string separator = "|")
        {
            string result = _name;
            for (int i = 0; i < list.Length; i++)
            {
                if (i > 0 && i < list.Length - 1) result += separator;
                result += list[i].ToString();
            }
            return result;
        }

        public static string GetArrayString<T>(string _name, List<T> list, string separator = "|")
        {
            string result = _name;
            for (int i = 0; i < list.Count; i++)
            {
                if (i > 0 && i < list.Count) result += separator;
                result += list[i].ToString();
            }
            return result;
        }

        public static Vector3 StringToVector3(string sVector)
        {
            if (sVector.StartsWith("("))
            {
                if (sVector.EndsWith(")"))
                    sVector = sVector.Substring(1, sVector.Length - 2);
                else
                    sVector = sVector.Substring(1, sVector.Length - 3);
            }
            string[] sArray = sVector.Split(',');
            Vector3 result = new Vector3(
                float.Parse(sArray[0]),
                float.Parse(sArray[1]),
                float.Parse(sArray[2]));

            return result;
        }
        public static Vector2 StringToVector2(string sVector)
        {
            if (sVector.StartsWith("("))
            {
                if (sVector.EndsWith(")"))
                    sVector = sVector.Substring(1, sVector.Length - 2);
                else
                    sVector = sVector.Substring(1, sVector.Length - 3);
            }
            string[] sArray = sVector.Split(',');
            Vector2 result = new Vector2(
                float.Parse(sArray[0]),
                float.Parse(sArray[1]));

            return result;
        }

        #endregion Arrays
    }
}