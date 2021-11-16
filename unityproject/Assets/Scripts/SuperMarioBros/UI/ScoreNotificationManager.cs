using System;
using UnityEngine;
using UnityEngine.UI;

namespace SuperMarioBros.UI
{
    public class ScoreNotificationManager : MonoBehaviour
    {
        public float velocity = 50;
        public float durationTime = 1;
        
        private float _elapsedTime;

        private void Start()
        {
            _elapsedTime = 0;
        }

        public void Update()
        {
            float delta = Time.deltaTime;
            
            _elapsedTime+=delta;
            
            if(_elapsedTime > durationTime)
                Destroy(gameObject);
            
            transform.position += velocity * delta * Vector3.up;
        }
    }
}