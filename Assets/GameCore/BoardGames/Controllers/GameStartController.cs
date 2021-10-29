using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.BoardGames
{
    public interface IGameStartController
    {
        void InitializeGame(Action<IList<PlayerInfo>, int> callback);
    }

    public class GameStartController : MonoBehaviour, IGameStartController
    {
        [SerializeField] private GameSelectionOptions _gameSelectionOptions;

        private Action<IList<PlayerInfo>, int> _callback;

        private void Start()
        {
            _gameSelectionOptions.OnApplyClick += AtStartApply;
        }

        public void InitializeGame(Action<IList<PlayerInfo>, int> callback)
        {
            _callback = callback;
            ChangeActiveGameSelection(true);
        }

        private void AtStartApply()
        {
            _callback.Invoke(_gameSelectionOptions.PlayerInfos, _gameSelectionOptions.CountDices);
            ChangeActiveGameSelection(false);
        }

        private void ChangeActiveGameSelection(bool active)
        {
            _gameSelectionOptions.gameObject.SetActive(active);
        }
    }
}