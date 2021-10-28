using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameCore.UIComponents
{
    public class SliderTextValue : MonoBehaviour
    {

        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI text;

        [SerializeField] private Slider changeMax;
        [SerializeField] private bool useSplitValue = false;
        [SerializeField] private string splitFormat = "{0}:{1}";

        private void OnValidate()
        {
            if (slider == null) slider = GetComponentInChildren<Slider>();
            if (text == null) text = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Awake()
        {
            slider.onValueChanged.AddListener(ChangeValue);
        }

        private void ChangeValue(float value)
        {
            if (text)
            {
                if (useSplitValue)
                {
                    text.text = string.Format(splitFormat, value, Mathf.Abs(value - slider.maxValue));
                }
                else
                {
                    text.text = ((int)value).ToString();
                }
            }
            if (changeMax)
            {
                changeMax.maxValue = value;
                changeMax.onValueChanged.Invoke(changeMax.value);
            }
        }

        public void Refresh()
        {
            slider.onValueChanged.Invoke(slider.value);
            ChangeValue(slider.value);
        }
    }
}