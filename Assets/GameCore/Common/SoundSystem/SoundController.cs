using System.Collections.Generic;
using UnityEngine;

using GameCore.DataManagment;

namespace GameCore.SoundSystem
{
    public class SoundController : SimpleSingleton<SoundController>
    {
        public static float musicVolume { get => PlayerPrefsData.GetFloat("MusicVolume", 0.5f); set => PlayerPrefsData.SetFloat("MusicVolume", value); }
        public static float soundVolume { get => PlayerPrefsData.GetFloat("SoundVolume", 0.5f); set => PlayerPrefsData.SetFloat("SoundVolume", value); }

        [System.Serializable]
        public class Sound
        {
            public string key;

            public enum Type { sound, music }
            public Type type = Type.sound;

            [Range(0, 1)]
            public float volume = 1;
            public AudioClip audioClip;
            public AudioSource audioSource;

            public void Init()
            {
                if (audioClip == null || audioSource == null) return;
                audioSource.clip = audioClip;
                UpdateVolume();

                if (audioSource.playOnAwake) Play();
            }

            public float GetVolume()
            {
                switch (type)
                {
                    case Type.sound: return volume * SoundController.soundVolume;
                    case Type.music: return volume * SoundController.musicVolume;
                    default: break;
                }
                return 0;
            }

            public void UpdateVolume()
            {
                if (audioSource == null) return;
                audioSource.volume = GetVolume();
            }

            public void Play(AudioSource otherSource = null)
            {
                if (audioClip == null) return;

                if (otherSource)
                {
                    otherSource.volume = GetVolume();
                    otherSource.PlayOneShot(audioClip);
                }
                else if (audioSource)
                {
                    audioSource.volume = GetVolume();
                    if (audioSource.loop)
                    {
                        audioSource.Play();
                    }
                    else
                    {
                        audioSource.PlayOneShot(audioClip);
                    }
                }
            }

            public void Stop()
            {
                if (audioSource == null) return;
                audioSource.Stop();
            }

            public void Pause()
            {
                if (audioSource == null) return;
                audioSource.Pause();
            }

            public void UnPause()
            {
                if (audioSource == null) return;
                audioSource.UnPause();
            }
        }

        // Общий audioSource
        public AudioSource commonSource;
        // Звуки
        public List<Sound> sounds;

        //private Dictionary<string, AudioSource> dictionary;

        public Sound this[string name]
        {
            get
            {
                for (int i = 0; i < sounds.Count; i++)
                {
                    if (sounds[i].key.Equals(name)) return sounds[i];
                }
                return null;
                //return dictionary.ContainsKey(name) ? dictionary[name] : null;
            }
        }

        #region Current Volume

        [Range(0, 1)]
        public float currentMusicValue = 0.5f;
        private float old_currentMusicValue = 0;
        [Range(0, 1)]
        public float currentSoundValue = 0.5f;
        private float old_currentSoundValue = 0;

        private void OnValidate()
        {
            if (Mathf.Approximately(currentMusicValue, old_currentMusicValue) == false)
            {
                old_currentMusicValue = currentMusicValue;
                musicVolume = currentMusicValue;
                ReValueAllSounds();
            }
            if (Mathf.Approximately(currentSoundValue, old_currentSoundValue) == false)
            {
                old_currentSoundValue = currentMusicValue;
                soundVolume = currentSoundValue;
                ReValueAllSounds();
            }
        }

        #endregion

        private void Awake()
        {
            currentMusicValue = musicVolume;
            currentSoundValue = soundVolume;

            for (int i = 0; i < sounds.Count; i++)
            {
                sounds[i].Init();
            }

            //if (dictionary == null || dictionary.Count == 0)
            //{
            //    dictionary = new Dictionary<string, AudioSource>();
            //    for (int i = 0; i < sounds.Count; i++)
            //    {
            //        if (sounds[i].audioSource)
            //            dictionary.Add(sounds[i].key, sounds[i].audioSource);
            //    }
            //}
        }

        // Проигрывание по ключу
        public void Play(string key)
        {
            //bool findKey = false;
            Sound sound = instance[key];
            if (sound != null)
            {
                sound.Play(sound.audioSource == null ? commonSource : null);
            }
            else
            {
                Debug.LogError(string.Format("SoundController Play Key '{0}' not found!", key));
            }
            //for (int i = 0; i < sounds.Count; i++)
            //{
            //    if (sounds[i].key.Equals(key))
            //    {
            //        findKey = true;
            //        sounds[i].Play(sounds[i].audioSource == null ? commonSource : null);
            //        break;
            //    }
            //}
            //if (findKey == false)
            //{
            //    Debug.Log(string.Format("SoundController Play Key '{0}' not found!", key));
            //}
        }

        // Останавливаем конкретный звук
        public void Stop(string key)
        {
            Sound sound = instance[key];
            if (sound != null)
            {
                sound.Stop();
            }
            else
            {
                Debug.LogError(string.Format("SoundController Play Key '{0}' not found!", key));
            }
        }

        // Останавливаем все звуки
        public void StopAll()
        {
            for (int i = 0; i < sounds.Count; i++)
            {
                sounds[i].Stop();
            }
            if (commonSource) commonSource.Stop();
        }

        // Установка всех звуков на паузу
        public void PauseAll()
        {
            for (int i = 0; i < sounds.Count; i++)
            {
                sounds[i].Pause();
            }
            if (commonSource) commonSource.Stop();
        }

        // Снитие с паузы всех звуков
        public void UnPauseAll()
        {
            for (int i = 0; i < sounds.Count; i++)
            {
                sounds[i].UnPause();
            }
            if (commonSource) commonSource.Stop();
        }

        // Обновление громкостей всех звуков
        public void ReValueAllSounds()
        {
            for (int i = 0; i < sounds.Count; i++)
            {
                sounds[i].UpdateVolume();
            }
        }
    }
}