using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameCore.BoardGames
{
    public class ScorePanelUI : MonoBehaviour
    {
        [SerializeField] private Sprite[] _mainSprites;
        [SerializeField] private Image _mainImage;
        [SerializeField] private TextMeshProUGUI _playerName;
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private TextMeshProUGUI _energy;
    }
}