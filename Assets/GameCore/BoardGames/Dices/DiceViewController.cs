using GameCore.SkinSystem;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.BoardGames
{
    /// <summary>
    /// Визуальная смена кубиков
    /// </summary>
    public class DiceViewController : MonoBehaviour
    {
        [SerializeField] private SkinContainer skinContainer;
        [SerializeField] private List<DiceUI> dices;
        [SerializeField] private GameObject diceControllerObj;

        private IDiceController diceController;

        private void Start()
        {
            diceController = diceControllerObj.GetComponent<IDiceController>();
            diceController.OnSetDiceCount += SetDices;
            Init();
        }

        public void Init()
        {
            diceController.OnSingleDiceChange += SetValue;

            foreach (var item in dices)
            {
                item.SetSkin(skinContainer.GetByIndex(0));
            }
        }

        private void SetValue(int index, int value)
        {
            dices[index].SetValue(value);
        }

        private void SetDices(int count)
        {
            for (int i = 0; i < dices.Count; i++)
            {
                dices[i].gameObject.SetActive(i < count);
            }
        }
    }
}