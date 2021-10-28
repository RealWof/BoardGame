using UnityEngine;

namespace GameCore.BoardGames
{
    public class TeleportNodeModule : MonoBehaviour, INodeModule
    {
        public ModuleType ModuleType => ModuleType.Teleport;

        [SerializeField] private BoardNode link;
        [SerializeField] private bool needTrowDice;
        [SerializeField] private int moreThen;

        public BoardNode Link => link;
        public bool NeedTrowDice => needTrowDice;
        public int MoreThen => moreThen;

    }
}