using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameCore.BoardGames
{
    public class NodeModuleProxy : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _controllersObj;

        private Dictionary<ModuleType, INodeModuleController> _controllers;

        INodeModuleController GetController(ModuleType moduleType) => _controllers[moduleType];

        private void Awake()
        {
            _controllers = _controllersObj.Select(x => x.GetComponent<INodeModuleController>()).ToDictionary(x => x.ModuleType);
        }

        public void OperateModule(PlayerContainer container, INodeModule nodeModule, Action onComplete)
        {
            var controller = GetController(nodeModule.ModuleType);
            controller.OperateModule(container, nodeModule, onComplete);
        }
    }
}