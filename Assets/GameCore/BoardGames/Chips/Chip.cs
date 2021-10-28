using UnityEngine;

using DG.Tweening;

using GameCore.SkinSystem;

namespace GameCore.BoardGames
{
    public class Chip : MonoBehaviour, IChip
    {
        [SerializeField] private SpriteRenderer view;
        [SerializeField] private SkinContainer skinContainer;

        public Sprite Skin => view.sprite;
        public Transform Root => transform;

        private Tween selectionTween;

        private Vector3 startPosition;
        private Vector3 startRotation;

        private void Start()
        {
            startPosition = transform.position;
            startRotation = transform.eulerAngles;
        }

        public void GetRandomSkin()
        {
            var skin = skinContainer.GetRandomSkin();
            view.sprite = skin.GetRandom();
        }

        /// <summary>
        /// Выбор корабля, показательно активности хода
        /// </summary>
        public void SetSelection()
        {
            view.sortingOrder = 5;
            selectionTween = view.transform.DOScale(view.transform.localScale + Vector3.one * 0.1f, 0.5f)
                .SetEase(Ease.InOutCubic)
                .SetLoops(-1, LoopType.Yoyo);
        }

        /// <summary>
        /// Сброс выделения корабля, при окончании хода
        /// </summary>
        public void SetDefault()
        {
            view.sortingOrder = 3;
            selectionTween.Kill();
            view.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutCubic);
        }

        /// <summary>
        /// Сброс позиции к стартовой точке (в начале новой игры)
        /// </summary>
        public void ResetPosition()
        {
            transform.position = startPosition;
            transform.eulerAngles = startRotation;
        }
    }
}