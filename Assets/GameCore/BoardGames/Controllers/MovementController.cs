using System;
using System.Linq;
using UnityEngine;

using DG.Tweening;

namespace GameCore.BoardGames
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private BoardMap _boardMap;

        public void Move(IChip chip, int currentIndex, int amount, Action callback)
        {
            var current = currentIndex;
            var next = Mathf.Clamp(current + amount, 0, _boardMap.Count - 1);
            var points = _boardMap.GetPoints(current, next).ToArray();
            var duration = 0.1f * points.Length;
            MoveAtPath(chip, points, duration, callback);
        }

        public void Move(IChip chip, int from, int to, float duration, Action callback)
        {
            var startPoint = _boardMap.GetPoint(from);
            var endPoint = _boardMap.GetPoint(to);

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