using System;
using FrameLord.EventDispatcher;
using SuperMarioBros.GameEvents;
using UnityEngine;
using SuperMarioBros.GameManager;

namespace SuperMarioBros.Environment
{
    public class FlagManager : MonoBehaviour
    {

        public float velocity = 50;
        public Collider2D goingDownLimit;

        private Rigidbody2D _rigidbody2D;
        
        // Start is called before the first frame update
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            
            GameEventDispatcher.Instance.AddListener(EnvPlayerReachingGoal.Name, OnPlayerReachingGoal);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void OnPlayerReachingGoal(System.Object obj, GameEvent ge)
        {
            MusicManager.Instance.Stop();
            SfkManager.Instance.Play(SfkManager.Instance.DownTheFlagpole);
            
            StartGoingDown();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other == goingDownLimit)
            {
                StopGoingDown();
            }
        }

        void StartGoingDown()
        {
            _rigidbody2D.velocity = Vector2.down * velocity;
        }

        void StopGoingDown()
        {
            _rigidbody2D.velocity = Vector2.zero;
            GameEventDispatcher.Instance.Dispatch(this, new EvnFlagIsDown());
            GameEventDispatcher.Instance.RemoveListener(EnvPlayerReachingGoal.Name, OnPlayerReachingGoal);
        }

        private void OnDestroy()
        {
            GameEventDispatcher.Instance.RemoveListener(EnvPlayerReachingGoal.Name, OnPlayerReachingGoal);
        }
    }
}
