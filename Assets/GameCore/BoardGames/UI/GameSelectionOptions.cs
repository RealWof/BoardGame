using UnityEngine;
using UnityEngine.UI;

namespace GameCore.BoardGames
{
    public class GameSelectionOptions : MonoBehaviour
    {
        public event System.Action OnApplyClick;
        
        [SerializeField] private Slider _chipSlider;
        [SerializeField] private Slider _dicesSlider;
        [SerializeField] private Button _apply;
        [SerializeField] private PlayerInfoPanel[] _playerPanels;

        public int CountChips { get; set; } = 2;
        public int CountDices { get; set; } = 2;

        public PlayerInfo[] PlayerInfos { get; private set; }

        private void Start()
        {
            _chipSlider.value = CountChips;
            _dicesSlider.value = CountDices;

            SetPlayerCount(CountChips);

            _chipSlider.onValueChanged.AddListener((x) => SetPlayerCount((int)x));
            _dicesSlider.onValueChanged.AddListener((x) => CountDices = (int)x);

            _apply.onClick.AddListener(AtApplyClick);
        }

        private void AtApplyClick()
        {
            PlayerInfos = new PlayerInfo[CountChips];
            for (int i = 0; i < CountChips; i++)
            {
                PlayerInfos[i] = _playerPanels[i].GetData();
            }
            OnApplyClick?.Invoke();
        }

        private void SetPlayerCount(int value)
        {
            CountChips = value;
            for (int i = 0; i < _playerPanels.Length; i++)
            {
                _playerPanels[i].gameObject.SetActive(i < value);
            }
        }
    }
}