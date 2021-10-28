using UnityEngine;

using Sirenix.OdinInspector;

namespace GameCore.DebugUtils
{
    public class FpsLimitSetter : MonoBehaviour
    {
        [SerializeField] private bool useLimit = false;
        [SerializeField] private int defaultLimit = 60;

        private void Awake()
        {
            if (useLimit)
            {
                SetFPSLimit(defaultLimit);
            }
        }

        [Button]
        private void Set30() => SetFPSLimit(30);

        [Button]
        private void Set60() => SetFPSLimit(60);

        [Button]
        private void Set120() => SetFPSLimit(120);

        [Button]
        private void SetFPSLimit(int count)
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = count;
        }
    }
}