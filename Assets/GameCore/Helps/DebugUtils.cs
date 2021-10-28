///============================================
/// Created by Wof 2018 © (andrewdiden@mail.ru)
///============================================

using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using System.Reflection;
#endif

namespace GameCore.Utils
{
    public class DebugUtils
    {
        static public void DrawString(string text, Vector3 worldPos, Color? colour = null)
        {
#if UNITY_EDITOR
            Handles.BeginGUI();

            var restoreColor = GUI.color;

            if (colour.HasValue) GUI.color = colour.Value;
            var view = SceneView.currentDrawingSceneView;
            Vector3 screenPos = view.camera.WorldToScreenPoint(worldPos);

            if (screenPos.y < 0 || screenPos.y > Screen.height || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.z < 0)
            {
                GUI.color = restoreColor;
                Handles.EndGUI();
                return;
            }

            Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text));
            GUI.Label(new Rect(screenPos.x - (size.x / 2), -screenPos.y + view.position.height + 4, size.x, size.y), text);
            GUI.color = restoreColor;
            Handles.EndGUI();
#endif
        }

        public static void ClearConsole()
        {
#if UNITY_EDITOR
            Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
            Type logEntries = assembly.GetType("UnityEditorInternal.LogEntries");
            MethodInfo clearConsoleMethod = logEntries.GetMethod("Clear");
            clearConsoleMethod.Invoke(new object(), null);
#endif
        }

        public static void RepaintSceneView()
        {
#if UNITY_EDITOR
            UnityEditor.SceneView.RepaintAll();
#endif
        }

        public static void WithGizmoColor(Color color, Action action)
        {
            Color oldColor = Gizmos.color;
            Gizmos.color = color;
            action.Invoke();
            Gizmos.color = oldColor;
        }

        #region ProgressBar

        public static void ProgressBar(string prName, ref int amountDone, int amountMax)
        {
#if UNITY_EDITOR
            float progressBar = 0.0f;
            progressBar = amountDone++ / (float)amountMax;
            int percentage = (int)(progressBar * 100f);
            //Debug.Log(string.Format("progressBar = {0} | amountDone = {1} | amountMax = {2} | percentage = {3}", progressBar, amountDone, amountMax, percentage));
            if (amountMax == -1)
            {
                EditorUtility.DisplayProgressBar(prName, "???% done... (???/???)", progressBar);
            }
            else
            {
                EditorUtility.DisplayProgressBar(prName, string.Format("{0}% done... ({1}/{2})", percentage, amountDone, amountMax), progressBar);
            }
#endif
        }

        // Закрытие Editor прогрессбара
        public static void ClearProgressBar()
        {
#if UNITY_EDITOR
            EditorUtility.ClearProgressBar();
#endif
        }

        #endregion

        #region Dialogs

        public static bool Dialog(string title, string message)
        {
#if UNITY_EDITOR
            return EditorUtility.DisplayDialog(title, message, "Yes", "No");
#else
            return false;
#endif
        }

        #endregion
    }
}