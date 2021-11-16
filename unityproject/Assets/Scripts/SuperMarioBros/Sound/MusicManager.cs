using FrameLord.Core;
using FrameLord.EventDispatcher;
using SuperMarioBros.GameEvents;
using UnityEngine;

namespace SuperMarioBros.GameManager
{
    public class MusicManager : MonoBehaviorSingleton<MusicManager>
    {
        public AudioClip aboveGround, underGround;

        private AudioSource _audioSource;
        private void Start()
        {
            DontDestroyOnLoad(this);
            _audioSource = GetComponent<AudioSource>();
            
            GameEventDispatcher.Instance.AddListener(EvnGamePaused.Name, OnPause);
            GameEventDispatcher.Instance.AddListener(EnvGameUnpaused.Name, OnUnpause);

        }

        public void Play(AudioClip a)
        {
            Stop();
            _audioSource.PlayOneShot(a);
        }

        public void Stop()
        {
            _audioSource.Stop();
        }

        private void OnPause(System.Object o, GameEvent e)
        {
            _audioSource.Pause();
        }

        private void OnUnpause(System.Object o, GameEvent e)
        {
            _audioSource.UnPause();
        }
    }
}