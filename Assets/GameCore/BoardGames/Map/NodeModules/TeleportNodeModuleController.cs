using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameCore.BoardGames
{
    public class TeleportNodeModuleController : MonoBehaviour, INodeModuleController
    {
        public ModuleType ModuleType => ModuleType.Teleport;

        [SerializeField] private MovementController _movementController;
        [SerializeField] private ThrowDiceProxy _throwDiceProxy;

        private TeleportNodeModule _teleportNodeModule;

        private PlayerContainer _container;
        private Action _onComplete;

        public void OperateModule(PlayerContainer container, INodeModule nodeModule, Action onComplete)
        {
            _container = container;
            _onComplete = onComplete;

            _teleportNodeModule = (TeleportNodeModule)nodeModule;

            if (_teleportNodeModule.NeedTrowDice)
                _throwDiceProxy.ThrowDices(container, CheckTeleport);
            else
                DoTeleport();
        }

        private void CheckTeleport(IList<int> values)
        {
            var summ = values.Sum();
            if (CanTeleport(summ))
                DoTeleport();
            else
                _onComplete?.Invoke();
        }

        private bool CanTeleport(int value) => _teleportNodeModule.MoreThen < value;

        private void DoTeleport()
        {
            var from = _container.CurrentNodeIndex;
            var to = _teleportNodeModule.Link.Index;
            _movementController.Move(_container.Chip, from, to, 1, _onComplete);
            _container.CurrentNodeIndex = to;
        }
    }
}