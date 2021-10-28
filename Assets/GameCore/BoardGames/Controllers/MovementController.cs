using System;
using System.Linq;
using UnityEngine;

using DG.Tweening;

namespace GameCore.BoardGames
{
    /// <summary>
    /// Двигает фишки на поле
    /// </summary>
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private BoardMap boardMap;

        public void Move(IChip chip, int currentIndex, int amount, Action callback)
        {
            int current = currentIndex;
            int next = Mathf.Clamp(current + amount, 0, boardMap.Count - 1);
            Vector3[] points = boardMap.GetPoints(current, next).ToArray();
            var duration = 0.1f * points.Length;
            MoveAtPath(chip, points, duration, callback);
        }

        public void Move(IChip chip, int from, int to, float duration, Action callback)
        {
            var startPoint = boardMap.GetPoint(from);
            var endPoint = boardMap.GetPoint(to);

            var points = new Vector3[] { startPoint, endPoint };
            MoveAtPath(chip, points, duration, callback);
        }

        private void MoveAtPath(IChip chip, Vector3[] points, float duration, Action callback)
        {
            chip.Root.DOPath(points, duration, PathType.CatmullRom, PathMode.TopDown2D)
                .OnComplete(() => callback?.Invoke()).SetLookAt(0.01f);
        }
    }
}