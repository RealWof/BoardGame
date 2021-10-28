using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameCore.BoardGames
{
    /// <summary>
    /// ”правл€ет ходом одного игрока
    /// </summary>
    public class TurnController : MonoBehaviour
    {
        public event System.Action OnTurnEnd;

        [SerializeField] private BoardMap boardMap;
        [SerializeField] private MovementController movementController;
        [SerializeField] private NodeModuleProxy nodeModuleProxy;
        [SerializeField] private ThrowDiceProxy throwDiceProxy;

        private PlayerContainer container;
        private int targetNodeIndex;

        private int currentValue;
        public int CurrentValue => currentValue;

        public void StartTurn(PlayerContainer container)
        {
            //Debug.Log("StartTurn " + container.Ship.Root.gameObject.name);
            this.container = container;

            throwDiceProxy.ThrowDices(container, AtDicesThrown);
        }

        private void AtDicesThrown(IList<int> values)
        {
            var summ = values.Sum();
            targetNodeIndex = Mathf.Clamp(container.CurrentNodeIndex + summ, 0, boardMap.Count - 1);
            movementController.Move(container.Chip, container.CurrentNodeIndex, summ, AtMoveCompleted);
        }

        private void AtMoveCompleted()
        {
            //Debug.Log("AtMoveCompleted");
            container.CurrentNodeIndex = targetNodeIndex;

            var node = boardMap.GetNode(targetNodeIndex);
            var module = node.NodeModule;
            nodeModuleProxy.OperateModule(container, module, OnTurnComplete);
        }

        private void OnTurnComplete()
        {
            //Debug.Log("OnTurnComplete" + container.Ship.Root.gameObject.name);
            OnTurnEnd?.Invoke();
        }
    }
}