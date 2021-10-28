using UnityEngine;

namespace GameCore.BoardGames
{
    public class SimpleNodeModule : MonoBehaviour, INodeModule
    {
        public ModuleType ModuleType => ModuleType.None;
    }
}