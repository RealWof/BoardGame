using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.BoardGames
{
    public class ThrowDiceProxy : MonoBehaviour
    {
        [SerializeField] private DiceController _diceController;
        [SerializeField] private ThrowDiceController _throwDiceController;

        private Action<IList<int>> _callback;

        public void ThrowDices(PlayerContainer container, Action<IList<int>> callback)
        {
            _callback = callback;
            _diceController.OnEnd += AtDicesThrown;
            if (container.PlayerInfo.IsBot)
            {
                _diceController.Go();
            }
            else
            {
                _throwDiceController.SetCallback(() =>
                {
                    _diceController.Go();
                    _throwDiceController.SetButtonActive(false);
                });
                _throwDiceController.SetButtonActive(true);
            }
        }

        private void AtDicesThrown(IList<int> values)
        {
            _diceController.OnEnd -= AtDicesThrown;
            _callback?.Invoke(values);
        }
    }
}