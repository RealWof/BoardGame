using UnityEngine;

using DG.Tweening;

namespace GameCore.BoardGames
{
    public class Chip : MonoBehaviour, IChip
    {
        [SerializeField] private SpriteRenderer _view;

        public Sprite Skin => _view.sprite;
        public Transform Root => transform;

        private Tween _selectionTween;

        private Vector3 _startPosition;
        private Vector3 _startRotation;

        private void Start()
        {
            _startPosition = transform.position;
            _startRotation = transform.eulerAngles;
        }

        public void SetSelection()
        {
            _view.sortingOrder = 5;
            _selectionTween = _view.transform.DOScale(_view.transform.localScale + Vector3.one * 0.1f, 0.5f)
                .SetEase(Ease.InOutCubic)
                .SetLoops(-1, LoopType.Yoyo);
        }

        public void SetDefault()
        {
            _view.sortingOrder = 3;
            _selectionTween.Kill();
            _view.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutCubic);
        }

        public void ResetPosition()
        {
            transform.position = _startPosition;
            transform.eulerAngles = _startRotation;
        }
    }
}