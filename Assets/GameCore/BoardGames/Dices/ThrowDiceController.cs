using System;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

namespace GameCore.BoardGames
{
    public class ThrowDiceController : MonoBehaviour
    {        
        [SerializeField] private Button _button;

        private Tween _selectionTween;

        public void SetCallback(Action callback)
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => callback?.Invoke());
        }

        public void SetButtonActive(bool active)
        {
            SetInteractable(active);
            if (active)
                SetSelection();
            else
                SetDefault();
        }

        public void SetInteractable(bool value) => _button.interactable = value;

        public void SetSelection() => _selectionTween = _button.transform.DOScale(_button.transform.localScale + Vector3.one * 0.1f, 0.5f)
                .SetEase(Ease.InOutCubic)
                .SetLoops(-1, LoopType.Yoyo);

        public void SetDefault()
        {
            _selectionTween.Kill();
            _button.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutCubic);
        }
    }
}