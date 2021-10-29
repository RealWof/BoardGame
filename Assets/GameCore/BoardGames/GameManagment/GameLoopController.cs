using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

using GameCore.Utils;

namespace GameCore.BoardGames
{
    public class GameLoopController : MonoBehaviour
    {
        [SerializeField] private List<Chip> _chips;

        [SerializeField] private int _currentID;

        [SerializeField] private WinGamePopUp _winPopUp;

        [SerializeField] private TurnController _turnController;
        [SerializeField] private GameStartController _gameStartController;
        [SerializeField] private DiceController _diceController;
        [SerializeField] private WinGameNodeModuleController _winGameNodeModuleController;

        public PlayerContainer Winner { get; private set; }

        private int _maxCount;
        private PlayerContainer[] _containers;

        private bool _isWin = false;

        private void Start()
        {
            _turnController.OnTurnEnd += EndTurn;
            _winGameNodeModuleController.OnGameWin += OnWinGame;
            _winPopUp.OnApplyClick += NewGame;
            NewGame();
        }

        private void NewGame()
        {
            _isWin = false;
            _winPopUp.gameObject.SetActive(false);
            ResetChipsPositions();
            _gameStartController.InitializeGame(
                (chips, dices) =>
                {
                    SetChips(chips);
                    SetDices(dices);
                    StartTurn();
                });
        }

        private void OnWinGame(PlayerContainer container)
        {
            Debug.Log($"Win player [{container.PlayerInfo.Name}]");
            Winner = container;
            _isWin = true;
            _winPopUp.SetInfo(container);
            _winPopUp.gameObject.SetActive(true);
        }

        private void SetChips(IList<PlayerInfo> playerInfos)
        {
            _maxCount = playerInfos.Count;
            _containers = new PlayerContainer[_maxCount];
            for (int i = 0; i < playerInfos.Count; i++)
            {
                _containers[i] = new PlayerContainer()
                {
                    CurrentNodeIndex = 0,
                    Chip = _chips[i],
                    PlayerInfo = playerInfos[i]
                };
            }
            for (int i = 0; i < _chips.Count; i++)
            {
                _chips[i].gameObject.SetActive(i < _maxCount);
            }
        }

        private void ResetChipsPositions() => _chips.ForEach(x => x.ResetPosition());

        private void SetDices(int count) => _diceController.CountDices = count;

        [Button, DisableInEditorMode]
        private void Next()
        {
            _currentID = Helpers.GetCycledID(_currentID, _maxCount, 1);
            StartTurn();
        }

        [Button, DisableInEditorMode]
        private void Previous()
        {
            _currentID = Helpers.GetCycledID(_currentID, _maxCount, -1);
            StartTurn();
        }

        [Button, DisableInEditorMode]
        private void StartTurn()
        {
            _turnController.StartTurn(_containers[_currentID]);
            _chips[_currentID].SetSelection();
        }

        [Button, DisableInEditorMode]
        private void EndTurn()
        {
            _chips[_currentID].SetDefault();
            if (!_isWin)
                Next();
        }
    }
}