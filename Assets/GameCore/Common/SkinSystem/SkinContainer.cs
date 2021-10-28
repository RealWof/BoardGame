using System.Collections.Generic;
using UnityEngine;

namespace GameCore.SkinSystem
{
    [CreateAssetMenu(fileName = "SkinContainer", menuName = "Configs/SkinContainer")]
    public class SkinContainer : ScriptableObject
    {
        [SerializeField] private List<Skin> skins;

        public IEnumerable<Skin> Skins => skins;

        public int Count => skins.Count;
        public Skin GetByIndex(int index) => skins.Count > index ? skins[index] : null;
        public Skin GetRandomSkin() => skins[Random.Range(0, skins.Count)];
    }
}