using UnityEngine;

namespace GameCore.Localization
{
    public static class Localization
    {
        private static string GetLocalize(string key)
        {
            // TODO: �������� ��������� �����������, ���� ���������� ����
            return key;
        }
        public static System.Action OnLocalizationChanged;

        public static string Localize(this string key) => GetLocalize(key);
    }
}