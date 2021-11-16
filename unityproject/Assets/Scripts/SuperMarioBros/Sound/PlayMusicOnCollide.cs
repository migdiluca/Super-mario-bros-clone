using System;
using UnityEngine;

namespace SuperMarioBros.GameManager
{
    public class PlayMusicOnCollide : MonoBehaviour
    {
        public enum Music
        {
            AboveGround, UnderGround
        }

        public Music musicToPlay;

        private void OnTriggerEnter2D(Collider2D other)
        {
            AudioClip a;
            switch (musicToPlay)
            {
                case Music.UnderGround:
                    a = MusicManager.Instance.underGround;
                    break;
                case Music.AboveGround:
                default:
                    a = MusicManager.Instance.aboveGround;
                    break; 
            }
            
            MusicManager.Instance.Play(a);
        }
    }
}