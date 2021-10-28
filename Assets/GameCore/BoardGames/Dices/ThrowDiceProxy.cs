using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.BoardGames
{
    /// <summary>
    /// Посредник для броска кубиков
    /// </summary>
    public class ThrowDiceProxy : MonoBehaviour
    {
        [SerializeField] private DiceController diceController;
        [SerializeField] private ThrowDiceController throwDiceController;

        private Action<IList<int>> callback;

        /// <summary>
        /// Метод броска кубиков, принимает информацию о бросающем и куда передать значения
        /// Если бросает игрок, то отдается управление кнопке броска
        /// Если бросает бот, то вызывается бросок, без кнопки.
        /// </summary>
        public void ThrowDices(PlayerContainer container, Action<IList<int>> callback)
        {
            this.callback = callback;
            diceController.OnEnd += AtDicesThrown;
            if (container.PlayerInfo.IsBot)
            {
                diceController.Go();
            }
            else
            {
                throwDiceController.SetCallback(() =>
                {
                    diceController.Go();
                    throwDiceController.SetButtonActive(false);
                });
                throwDiceController.SetButtonActive(true);
            }
        }

        private void AtDicesThrown(IList<int> values)
        {
            diceController.OnEnd -= AtDicesThrown;
            callback?.Invoke(values);
        }
    }
}