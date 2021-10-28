using System.Collections.Generic;
using UnityEngine;

namespace GameCore.SkinSystem
{
    [System.Serializable]
    public class Skin
    {
        [SerializeField] private string name;
        [SerializeField] private Sprite[] sprites;

        public string Name => name;
        public IEnumerable<Sprite> Sprites => sprites;

        public int Count => sprites.Length;
        public Sprite GetByIndex(int index) => sprites.Length > index ? sprites[index] : null;
        public Sprite GetRandom() => sprites[Random.Range(0, sprites.Length)];
    }
}