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
        [SerializeField] private SpriteRenderer _view;
        [SerializeField] private TextMeshProUGUI _label;

        [SerializeField] private GameObject _module;

        private INodeModule _nodeModule;
        public INodeModule NodeModule => _nodeModule;

        private int index;
        public int Index => index;

        private void Awake()
        {
            _nodeModule = _module.GetComponent<INodeModule>();
        }

        public Vector3 GetPosition() => transform.position;

        public void SetSprite(Sprite sprite) => _view.sprite = sprite;

        public void SetIndex(int index, int max)
        {
            this.index = index;
            if (index == 0)
            {
                _label.text = "Start".Localize();
            }
            else if (index == max - 1)
            {
                _label.text = "End".Localize();
            }
            else
            {
                _label.text = index.ToString();
            }
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(_label);
#endif
        }
    }
}