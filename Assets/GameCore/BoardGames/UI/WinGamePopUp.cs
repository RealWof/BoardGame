using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameCore.BoardGames
{
    public class WinGamePopUp : MonoBehaviour
    {
        public event System.Action OnApplyClick;

        [SerializeField] private TextMeshProUGUI playerName;
        [SerializeField] private Image skin;
        [SerializeField] private Button apply;

        private void Start()
        {
            apply.onClick.AddListener(AtApplyClick);
        }

        public void SetInfo(PlayerContainer container)
        {
            playerName.text = container.PlayerInfo.Name;
            skin.sprite = container.Chip.Skin;
        }

        private void AtApplyClick()
        {
            OnApplyClick?.Invoke();
        }
    }
}