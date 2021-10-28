using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

namespace GameCore.BoardGames
{
    public class BoardMap : MonoBehaviour
    {
        [SerializeField] private List<BoardNode> nodes;

        public int Count => nodes.Count;

        private void Start()
        {
            RefreshNodeLabels();
        }

        public BoardNode GetNode(int targetNodeIndex) => nodes[targetNodeIndex];
        
        public Vector3 GetPoint(int index) => nodes[index].GetPosition();

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
        private void RefreshNodeLabels()
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].SetIndex(i, nodes.Count);
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(nodes[i]);
#endif
            }
        }
    }
}