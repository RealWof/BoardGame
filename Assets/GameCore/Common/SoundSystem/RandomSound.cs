using UnityEngine;

using Sirenix.OdinInspector;

namespace GameCore.SoundSystem
{
    public class RandomSound : MonoBehaviour
    {
        [SerializeField] private AudioClip[] sounds;
        [SerializeField] private AudioSource source;

        private void Awake()
        {
            if (source == null) source = GetComponent<AudioSource>();
        }

        [Button]
        public void Play(int id = -1)
        {
            id = GetID(id);
            if (source)
            {
                if (source.isPlaying)
                {
                    return;
                }
                if (sounds[id])
                {
                    source.clip = sounds[id];
                    source.Play();
                }
            }
        }

        public int GetID(int id = -1) { if (id == -1) id = Random.Range(0, sounds.Length); return id; }

        public AudioClip GetAudio(int id = -1) { return sounds[GetID(id)]; }
    }
}