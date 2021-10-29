using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameCore.BoardGames
{
    public class WinGamePopUp : MonoBehaviour
    {
        public event System.Action OnApplyClick;

        [SerializeField] private TextMeshProUGUI _playerName;
        [SerializeField] private Image _skin;
        [SerializeField] private Button _apply;

        private void Start()
        {
            _apply.onClick.AddListener(AtApplyClick);
        }

        public void SetInfo(PlayerContainer container)
        {
            _playerName.text = container.PlayerInfo.Name;
            _skin.sprite = container.Chip.Skin;
        }

        private void AtApplyClick()
        {
            OnApplyClick?.Invoke();
        }
    }
}