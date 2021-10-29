using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameCore.BoardGames
{
    public class TurnController : MonoBehaviour
    {
        public event System.Action OnTurnEnd;

        [SerializeField] private BoardMap _boardMap;
        [SerializeField] private MovementController _movementController;
        [SerializeField] private NodeModuleProxy _nodeModuleProxy;
        [SerializeField] private ThrowDiceProxy _throwDiceProxy;

        private PlayerContainer _container;
        private int _targetNodeIndex;

        private int _currentValue;
        public int CurrentValue => _currentValue;

        public void StartTurn(PlayerContainer container)
        {
            _container = container;
            _throwDiceProxy.ThrowDices(container, AtDicesThrown);
        }

        private void AtDicesThrown(IList<int> values)
        {
            var summ = values.Sum();
            _targetNodeIndex = Mathf.Clamp(_container.CurrentNodeIndex + summ, 0, _boardMap.Count - 1);
            _movementController.Move(_container.Chip, _container.CurrentNodeIndex, summ, AtMoveCompleted);
        }

        private void AtMoveCompleted()
        {
            _container.CurrentNodeIndex = _targetNodeIndex;
            var node = _boardMap.GetNode(_targetNodeIndex);
            var module = node.NodeModule;
            _nodeModuleProxy.OperateModule(_container, module, OnTurnComplete);
        }

        private void OnTurnComplete()
        {
            OnTurnEnd?.Invoke();
        }
    }
}