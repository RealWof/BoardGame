using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

namespace GameCore.BoardGames
{
    public class BoardMap : MonoBehaviour
    {
        [SerializeField] private List<BoardNode> _nodes;

        public int Count => _nodes.Count;

        private void Start()
        {
            RefreshNodeLabels();
        }

        public BoardNode GetNode(int targetNodeIndex) => _nodes[targetNodeIndex];
        
        public Vector3 GetPoint(int index) => _nodes[index].GetPosition();

        public IEnumerable<Vector3> GetPoints(int start, int end)
        {
            var result = new List<Vector3>();
            for (int i = start; i <= end; i++)
            {
                result.Add(GetPoint(i));
            }
            return result;
        }

        [Button]
        private void RefreshNodes()
        {
            var all = GetComponentsInChildren<BoardNode>();
            _nodes = new List<BoardNode>(all);
        }

        [Button]
        private void RefreshNodeLabels()
        {
            for (int i = 0; i < _nodes.Count; i++)
            {
                _nodes[i].SetIndex(i, _nodes.Count);
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(_nodes[i]);
#endif
            }
        }
    }
}