using System;
using FrameLord.Core;
using FrameLord.EventDispatcher;
using SuperMarioBros.GameEvents;
using UnityEngine;

namespace SuperMarioBros.GameManager
{
    public class SfkManager : MonoBehaviorSingleton<SfkManager>
    {
        public AudioClip GameOver,
            MarioDies,
            RunningOutOfTime,
            WorldClear,
            StageClear,
            OneUp,
            BrickSmash,
            Bump,
            Coin,
            DownTheFlagpole,
            JumpSmall,
            JumpSuper,
            Pause,
            PipeTravel,
            PowerUp,
            PowerUpAppears,
            Stomp;

        private AudioSource _audioSource;
        private void Start()
        {
            DontDestroyOnLoad(this);
            _audioSource = GetComponent<AudioSource>();
            

        }

        public void Play(AudioClip a)
        {
            _audioSource.PlayOneShot(a);
        }
        
    }
}