using System;

namespace GameCore.BoardGames
{
    public interface INodeModuleController
    {
        ModuleType ModuleType { get; }

        void OperateModule(PlayerContainer container, INodeModule nodeModule, Action onComplete);
    }
}