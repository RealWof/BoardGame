using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.BoardGames
{
    public interface IGameStartController
    {
        void InitializeGame(Action<IList<PlayerInfo>, int> callback);
    }

    /// <summary>
    /// Инициатор старта игры
    /// </summary>
    public class GameStartController : MonoBehaviour, IGameStartController
    {
        [SerializeField] private GameSelectionOptions gameSelectionOptions;

        private Action<IList<PlayerInfo>, int> callback;

        private void Start()
        {
            gameSelectionOptions.OnApplyClick += AtStartApply;
        }

        private void AtStartApply()
        {
            callback.Invoke(gameSelectionOptions.PlayerInfos, gameSelectionOptions.CountDices);
            ChangeActiveGameSelection(false);
        }

        public void InitializeGame(Action<IList<PlayerInfo>, int> callback)
        {
            this.callback = callback;
            ChangeActiveGameSelection(true);
        }

        private void ChangeActiveGameSelection(bool active)
        {
            gameSelectionOptions.gameObject.SetActive(active);
        }
    }
}