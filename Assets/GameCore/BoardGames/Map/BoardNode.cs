using UnityEngine;
using TMPro;

using GameCore.Localization;

namespace GameCore.BoardGames
{
    public enum ModuleType
    {
        None,
        WinGame,
        Teleport,
        Scores
    }

    public class BoardNode : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer view;
        [SerializeField] private TextMeshProUGUI label;

        [SerializeField] private GameObject module;

        private INodeModule nodeModule;
        public INodeModule NodeModule => nodeModule;

        private int index;
        public int Index => index;

        private void Awake()
        {
            nodeModule = module.GetComponent<INodeModule>();
        }

        public Vector3 GetPosition() => transform.position;

        public void SetSprite(Sprite sprite)
        {
            view.sprite = sprite;
        }

        public void SetIndex(int index, int max)
        {
            this.index = index;
            if (index == 0)
            {
                label.text = "Start".Localize();
            }
            else if (index == max - 1)
            {
                label.text = "End".Localize();
            }
            else
            {
                label.text = index.ToString();
            }
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(label);
#endif
        }
    }
}