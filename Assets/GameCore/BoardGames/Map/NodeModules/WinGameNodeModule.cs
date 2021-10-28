using UnityEngine;

namespace GameCore.BoardGames
{
    public class WinGameNodeModule : MonoBehaviour, INodeModule
    {
        public ModuleType ModuleType => ModuleType.WinGame;
    }
}