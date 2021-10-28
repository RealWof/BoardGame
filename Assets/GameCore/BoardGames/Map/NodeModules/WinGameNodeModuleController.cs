using System;
using UnityEngine;

namespace GameCore.BoardGames
{
    public class WinGameNodeModuleController : MonoBehaviour, INodeModuleController
    {
        public event Action<PlayerContainer> OnGameWin;

        public ModuleType ModuleType => ModuleType.WinGame;

        public void OperateModule(PlayerContainer container, INodeModule nodeModule, Action onComplete)
        {
            nodeModule = (WinGameNodeModule)nodeModule;
            OnGameWin?.Invoke(container);
            onComplete?.Invoke();
        }
    }
}
