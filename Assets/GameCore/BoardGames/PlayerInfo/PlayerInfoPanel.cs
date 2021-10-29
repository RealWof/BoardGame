using UnityEngine;
using UnityEngine.UI;
using TMPro;

using GameCore.Utils;
using GameCore.SkinSystem;

namespace GameCore.BoardGames
{
    public class PlayerInfoPanel : MonoBehaviour
    {
        [Header("Skin")]
        [SerializeField] private SkinContainer _skinContainer;
        [SerializeField] private Image _skinImage;
        [SerializeField] private Button _skinButton;

        [Header("Name")]
        [SerializeField] private TMP_InputField _inputField;

        [Header("Type")]
        [SerializeField] private Image _typeIcon;
        [SerializeField] private Button _typeButton;
        [SerializeField] private Sprite _playerSprite;
        [SerializeField] private Sprite _botSprite;

        private int _index;
        private int _currentSkin = 0;
        private bool _isBot;

        private void Start()
        {
            _skinButton.onClick.AddListener(AtSkinClick);
            _typeButton.onClick.AddListener(AtTypeClick);

            var skin = _skinContainer.GetByIndex(0);
            _skinImage.sprite = skin.GetByIndex(_currentSkin);
        }

        public PlayerInfo GetData() => new PlayerInfo()
        {
            Name = _inputField.text,
            Index = _index,
            Skin = _currentSkin,
            IsBot = _isBot,
        };

        private void AtTypeClick()
        {
            _isBot = !_isBot;
            _typeIcon.sprite = _isBot ? _botSprite : _playerSprite;
        }

        private void AtSkinClick()
        {
            var skin = _skinContainer.GetByIndex(0);
            var nextSkin = Helpers.GetCycledID(_currentSkin, skin.Count, 1);
            _skinImage.sprite = skin.GetByIndex(nextSkin);
            _currentSkin = nextSkin;
        }
    }
}