using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameCore.BoardGames
{
    public class TeleportNodeModuleController : MonoBehaviour, INodeModuleController
    {
        public ModuleType ModuleType => ModuleType.Teleport;

        [SerializeField] private MovementController movementController;
        [SerializeField] private ThrowDiceProxy throwDiceProxy;

        private TeleportNodeModule teleportNodeModule;

        private PlayerContainer container;
        private Action onComplete;

        public void OperateModule(PlayerContainer container, INodeModule nodeModule, Action onComplete)
        {
            this.container = container;
            this.onComplete = onComplete;
            teleportNodeModule = (TeleportNodeModule)nodeModule;

            if (teleportNodeModule.NeedTrowDice)
            {
                throwDiceProxy.ThrowDices(container, CheckTeleport);
            }
            else
            {
                DoTeleport();
            }
        }

        private void CheckTeleport(IList<int> values)
        {
            var summ = values.Sum();
            if (CanTeleport(summ))
            {
                DoTeleport();
            }
            else
            {
                onComplete?.Invoke();
            }
        }

        private bool CanTeleport(int value) => teleportNodeModule.MoreThen < value;

        private void DoTeleport()
        {
            var from = container.CurrentNodeIndex;
            var to = teleportNodeModule.Link.Index;
            movementController.Move(container.Chip, from, to, 1, onComplete);
            container.CurrentNodeIndex = to;
        }
    }
}