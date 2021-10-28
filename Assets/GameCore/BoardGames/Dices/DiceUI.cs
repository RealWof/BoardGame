using UnityEngine;
using UnityEngine.UI;
using TMPro;

using GameCore.SkinSystem;

namespace GameCore.BoardGames
{
    [System.Serializable]
    public class DiceUI : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI text;

        private Skin skin;

        private int value = 0;
        public int Value => value;

        public void SetSkin(Skin skin)
        {
            this.skin = skin;
        }

        public void SetValue(int value)
        {
            this.value = value;
            if (image)
            {
                image.sprite = skin.GetByIndex(value - 1);
            }
            if (text)
            {
                text.text = value.ToString();
            }
        }
    }
}