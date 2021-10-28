using System;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

namespace GameCore.BoardGames
{
    /// <summary>
    /// Контроллер кнопки броска кубиков
    /// </summary>
    public class ThrowDiceController : MonoBehaviour
    {        
        [SerializeField] private Button button;

        private Action callback;

        private Tween selectionTween;

        public void SetCallback(Action callback)
        {
            this.callback = callback;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => callback?.Invoke());
        }

        public void SetButtonActive(bool active)
        {
            SetInteractable(active);
            if (active)
            {
                SetSelection();
            }
            else
            {
                SetDefault();
            }
        }

        public void SetInteractable(bool value)
        {
            button.interactable = value;
        }

        public void SetSelection()
        {
            selectionTween = button.transform.DOScale(button.transform.localScale + Vector3.one * 0.1f, 0.5f)
                .SetEase(Ease.InOutCubic)
                .SetLoops(-1, LoopType.Yoyo);
        }

        public void SetDefault()
        {
            selectionTween.Kill();
            button.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutCubic);
        }
    }
}