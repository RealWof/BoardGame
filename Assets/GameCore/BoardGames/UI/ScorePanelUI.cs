using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameCore.BoardGames
{
    public class ScorePanelUI : MonoBehaviour
    {
        [SerializeField] private Sprite[] mainSprites;
        [SerializeField] private Image mainImage;
        [SerializeField] private TextMeshProUGUI playerName;
        [SerializeField] private TextMeshProUGUI score;
        [SerializeField] private TextMeshProUGUI energy;
    }
}