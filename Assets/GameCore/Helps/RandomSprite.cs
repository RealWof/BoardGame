using UnityEngine;

namespace GameCore.Utils
{
    public class RandomSprite : MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites;

        private void Start()
        {
            if (TryGetComponent<SpriteRenderer>(out var renderer))
            {
                renderer.sprite = sprites[Random.Range(0, sprites.Length)];
            }
        }
    }
}