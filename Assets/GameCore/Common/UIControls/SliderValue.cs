using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameCore.UIComponents
{
    public class SliderValue : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        private TextMeshProUGUI text;

        private void Start()
        {
            text = GetComponent<TextMeshProUGUI>();
            slider.onValueChanged.AddListener((x) => text.text = x.ToString());
        }
    }
}