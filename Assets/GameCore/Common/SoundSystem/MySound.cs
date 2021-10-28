using UnityEngine;
using UnityEngine.UI;

namespace GameCore.SoundSystem
{
    public class MySound : MonoBehaviour
    {
        [SerializeField] private string key;
        [SerializeField] private bool isButton = false;
        [SerializeField] private Button button;

        private bool added = false;

        private void OnEnable()
        {
            if (isButton)
            {
                if (button == null) button = GetComponent<Button>();
                if (button && added == false)
                {
                    added = true;
                    button.onClick.AddListener(() => SoundController.instance.Play(key));
                }
            }
        }

        public void Play()
        {
            SoundController.instance.Play(key);
        }
    }
}