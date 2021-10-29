using UnityEngine;

namespace GameCore.BoardGames
{
    public class TeleportNodeModule : MonoBehaviour, INodeModule
    {
        public ModuleType ModuleType => ModuleType.Teleport;

        [SerializeField] private BoardNode _link;
        [SerializeField] private bool _needTrowDice;
        [SerializeField] private int _moreThen;

        public BoardNode Link => _link;
        public bool NeedTrowDice => _needTrowDice;
        public int MoreThen => _moreThen;

    }
}