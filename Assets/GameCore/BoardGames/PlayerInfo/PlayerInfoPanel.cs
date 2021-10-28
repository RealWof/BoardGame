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
        [SerializeField] private SkinContainer skinContainer;
        [SerializeField] private Image skinImage;
        [SerializeField] private Button skinButton;

        [Header("Name")]
        [SerializeField] private TMP_InputField inputField;

        [Header("Type")]
        [SerializeField] private Image typeIcon;
        [SerializeField] private Button typeButton;
        [SerializeField] private Sprite playerSprite;
        [SerializeField] private Sprite botSprite;

        private PlayerInfo playerInfo;
        private int index;
        private int currentSkin = 0;
        private bool isBot;

        private void Start()
        {
            skinButton.onClick.AddListener(AtSkinClick);
            typeButton.onClick.AddListener(AtTypeClick);

            var skin = skinContainer.GetByIndex(0);
            skinImage.sprite = skin.GetByIndex(currentSkin);
        }

        public void SetData(PlayerInfo playerInfo, int index)
        {
            this.playerInfo = playerInfo;
            this.index = index;
        }

        public PlayerInfo GetData()
        {
            return new PlayerInfo()
            {
                Name = inputField.text,
                Index = index,
                Skin = currentSkin,
                IsBot = isBot,
            };
        }

        private void AtTypeClick()
        {
            isBot = !isBot;
            typeIcon.sprite = isBot ? botSprite : playerSprite;
        }

        private void AtSkinClick()
        {
            var skin = skinContainer.GetByIndex(0);
            var nextSkin = Helpers.GetCycledID(currentSkin, skin.Count, 1);
            skinImage.sprite = skin.GetByIndex(nextSkin);
            currentSkin = nextSkin;
        }

        
    }
}