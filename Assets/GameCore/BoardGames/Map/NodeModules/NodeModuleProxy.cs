using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameCore.BoardGames
{
    public class NodeModuleProxy : MonoBehaviour
    {
        [SerializeField] private List<GameObject> controllersObj;

        private Dictionary<ModuleType, INodeModuleController> controllers;

        INodeModuleController GetController(ModuleType moduleType) => controllers[moduleType];

        private void Awake()
        {
            controllers = controllersObj.Select(x => x.GetComponent<INodeModuleController>()).ToDictionary(x => x.ModuleType);
        }

        public void OperateModule(PlayerContainer container, INodeModule nodeModule, Action onComplete)
        {
            var controller = GetController(nodeModule.ModuleType);
            controller.OperateModule(container, nodeModule, onComplete);
        }
    }
}