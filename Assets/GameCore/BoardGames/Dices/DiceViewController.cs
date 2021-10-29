using System.Collections.Generic;
using UnityEngine;

using GameCore.SkinSystem;

namespace GameCore.BoardGames
{
    /// <summary>
    /// Визуальная смена кубиков
    /// </summary>
    public class DiceViewController : MonoBehaviour
    {
        [SerializeField] private SkinContainer _skinContainer;
        [SerializeField] private List<DiceUI> _dices;
        [SerializeField] private GameObject _diceControllerObj;

        private IDiceController _diceController;

        private void Start()
        {
            _diceController = _diceControllerObj.GetComponent<IDiceController>();
            _diceController.OnSetDiceCount += SetDices;
            Init();
        }

        public void Init()
        {
            _diceController.OnSingleDiceChange += SetValue;

            foreach (var item in _dices)
            {
                item.SetSkin(_skinContainer.GetByIndex(0));
            }
        }

        private void SetValue(int index, int value) => _dices[index].SetValue(value);

        private void SetDices(int count)
        {
            for (int i = 0; i < _dices.Count; i++)
            {
                _dices[i].gameObject.SetActive(i < count);
            }
        }
    }
}