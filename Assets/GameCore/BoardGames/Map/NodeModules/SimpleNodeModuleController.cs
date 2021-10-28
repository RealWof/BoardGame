using System;
using UnityEngine;

namespace GameCore.BoardGames
{
    public class SimpleNodeModuleController : MonoBehaviour, INodeModuleController
    {
        public ModuleType ModuleType => ModuleType.None;

        public void OperateModule(PlayerContainer container, INodeModule nodeModule, Action onComplete)
        {
            onComplete?.Invoke();
        }
    }
}