using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

using GameCore.Utils;

namespace GameCore.BoardGames
{
    public class GameLoopController : MonoBehaviour
    {
        [SerializeField] private List<Chip> chips;

        [SerializeField] private int currentID;

        [SerializeField] private WinGamePopUp winPopUp;

        [SerializeField] private TurnController turnController;
        [SerializeField] private GameStartController gameStartController;
        [SerializeField] private DiceController diceController;
        [SerializeField] private WinGameNodeModuleController winGameNodeModuleController;

        private int maxCount;
        private PlayerContainer[] containers;

        private bool isWin = false;

        public PlayerContainer Winner { get; private set; }

        private void Start()
        {
            turnController.OnTurnEnd += EndTurn;
            winGameNodeModuleController.OnGameWin += OnWinGame;
            winPopUp.OnApplyClick += NewGame;
            NewGame();
        }

        private void NewGame()
        {
            isWin = false;
            winPopUp.gameObject.SetActive(false);
            ResetChipsPositions();
            gameStartController.InitializeGame(
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
            isWin = true;
            winPopUp.SetInfo(container);
            winPopUp.gameObject.SetActive(true);
        }

        private void SetChips(IList<PlayerInfo> playerInfos)
        {
            maxCount = playerInfos.Count;
            containers = new PlayerContainer[maxCount];
            for (int i = 0; i < playerInfos.Count; i++)
            {
                containers[i] = new PlayerContainer()
                {
                    CurrentNodeIndex = 0,
                    Chip = chips[i],
                    PlayerInfo = playerInfos[i]
                };
            }
            for (int i = 0; i < chips.Count; i++)
            {
                chips[i].gameObject.SetActive(i < maxCount);
            }
        }

        private void ResetChipsPositions()
        {
            chips.ForEach(x => x.ResetPosition());
        }

        private void SetDices(int count)
        {
            diceController.CountDices = count;
        }

        [Button, DisableInEditorMode]
        private void Next()
        {
            currentID = Helpers.GetCycledID(currentID, maxCount, 1);
            StartTurn();
        }

        [Button, DisableInEditorMode]
        private void Previous()
        {
            currentID = Helpers.GetCycledID(currentID, maxCount, -1);
            StartTurn();
        }

        [Button, DisableInEditorMode]
        private void StartTurn()
        {
            turnController.StartTurn(containers[currentID]);
            chips[currentID].SetSelection();
        }

        [Button, DisableInEditorMode]
        private void EndTurn()
        {
            chips[currentID].SetDefault();
            if (!isWin)
                Next();
        }
    }
}