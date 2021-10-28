using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Sirenix.OdinInspector;

namespace GameCore.BoardGames
{
    /// <summary>
    /// Броски кубиков
    /// </summary>
    public class DiceController : MonoBehaviour, IDiceController
    {
        public event System.Action OnStart;
        public event System.Action<int, int> OnSingleDiceChange;
        public event System.Action<IList<int>> OnEnd;
        public event System.Action<int> OnSetDiceCount;

        [Header("Debug")]
        [SerializeField] private int debugResult = 0; // выставленное значение следующего выпада
        [SerializeField] private bool isFast = false; // пропускать ли прокрутку кубиков

        private int[] values;

        private int countDices;
        public int CountDices { get => countDices; set { countDices = value; OnSetDiceCount.Invoke(countDices); } }

        [Button, DisableInEditorMode]
        public void Go()
        {
            StartCoroutine(GoDices(CountDices, isFast));
        }

        private IEnumerator GoDices(int count, bool fast = false)
        {
            values = new int[count];
            var delay = 0f;
            for (int i = 0; i < count; i++)
            {
                var currentCount = Random.Range(5, 10);
                var currentDelay = Random.Range(0.1f, 0.3f);
                delay += currentCount * currentDelay;
                if (fast)
                {
                    currentCount = 1;
                    currentDelay = 0;
                }
                StartCoroutine(Go(i, currentCount, currentDelay));
            }
            if (fast) delay = 0;
            yield return new WaitForSeconds(delay);

            // Если указано > 0, то результат будет равен заданному числу, независимо от выпавших значений
            if (debugResult != 0)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = 0;
                }
                values[0] = debugResult;
            }

            OnEnd?.Invoke(values);
        }

        private IEnumerator Go(int index, int count, float delay)
        {
            yield return RandomDice(count, delay, (int x) => { values[index] = x; OnSingleDiceChange?.Invoke(index, x); });
        }

        private IEnumerator RandomDice(int countChange, float delayChange, System.Action<int> action)
        {
            WaitForSeconds wfs = new WaitForSeconds(delayChange);
            int randValue;
            for (int i = 0; i < countChange; i++)
            {
                randValue = Random.Range(1, 7);
                action?.Invoke(randValue);
                yield return wfs;
            }
            yield return null;
        }

    }
}