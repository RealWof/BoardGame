using UnityEngine;
using UnityEngine.UI;

namespace GameCore.BoardGames
{
    public class GameSelectionOptions : MonoBehaviour
    {
        public event System.Action OnApplyClick;
        
        [SerializeField] private Slider chipSlider;
        [SerializeField] private Slider dicesSlider;
        [SerializeField] private Button apply;
        [SerializeField] private PlayerInfoPanel[] playerPanels;

        public int CountChips { get; set; } = 2;
        public int CountDices { get; set; } = 2;

        public PlayerInfo[] PlayerInfos { get; private set; }

        private void Start()
        {
            chipSlider.value = CountChips;
            dicesSlider.value = CountDices;

            SetPlayerCount(CountChips);

            chipSlider.onValueChanged.AddListener((x) => SetPlayerCount((int)x));
            dicesSlider.onValueChanged.AddListener((x) => CountDices = (int)x);

            apply.onClick.AddListener(AtApplyClick);
        }

        private void AtApplyClick()
        {
            PlayerInfos = new PlayerInfo[CountChips];
            for (int i = 0; i < CountChips; i++)
            {
                PlayerInfos[i] = playerPanels[i].GetData();
            }
            OnApplyClick?.Invoke();
        }

        private void SetPlayerCount(int value)
        {
            CountChips = value;
            for (int i = 0; i < playerPanels.Length; i++)
            {
                playerPanels[i].gameObject.SetActive(i < value);
            }
        }
    }
}