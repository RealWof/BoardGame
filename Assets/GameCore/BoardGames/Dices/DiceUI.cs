using UnityEngine;
using UnityEngine.UI;
using TMPro;

using GameCore.SkinSystem;

namespace GameCore.BoardGames
{
    [System.Serializable]
    public class DiceUI : MonoBehaviour
    {
        [SerializeField] private Image _image;

        private Skin _skin;

        public void SetSkin(Skin skin) => _skin = skin;

        public void SetValue(int value) => _image.sprite = _skin.GetByIndex(value - 1);
    }
}