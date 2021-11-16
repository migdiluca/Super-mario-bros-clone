using System;
using FrameLord.EventDispatcher;
using JetBrains.Annotations;
using SuperMarioBros.GameEvents;
using UnityEngine;
using UnityEngine.UI;

namespace SuperMarioBros.UI
{
    public class HUDManager: MonoBehaviour
    {
        public Text timeValue, scoreValue, coinsValue, livesValue;

        [CanBeNull] public GameObject scoreNotificationPrefab;

        private TextMesh _gameObjectTextMesh;
        
        public void Start()
        {
            if (scoreNotificationPrefab == null)
                return;
            
            _gameObjectTextMesh = scoreNotificationPrefab.GetComponent<TextMesh>();
            GameEventDispatcher.Instance.AddListener(EvnScoreIncrement.Name, OnScoreIncrement);
        }

        public void Update()
        {
            DisplayTime();
            DisplayCoins();
            DisplayScore();
            DisplayLives();
        }

        private void OnScoreIncrement(System.Object o, GameEvent ev)
        {
            EvnScoreIncrement scoreInc = (EvnScoreIncrement) ev;

            _gameObjectTextMesh.text = scoreInc.Increment.ToString();
            scoreNotificationPrefab.transform.position = scoreInc.Position;
            GameObject go = Instantiate(scoreNotificationPrefab, scoreInc.Position, Quaternion.identity);

        }

        private void DisplayTime()
        {
            int remainingTime = GameManager.GameManager.Instance.GetRemainingTime();
            timeValue.text = $"{remainingTime:000}";
        }

        private void DisplayScore()
        {
            int score = GameManager.GameManager.Instance.GetScore();
            scoreValue.text = $"{score:000000}";
        }

        private void DisplayCoins()
        {
            int coins = GameManager.GameManager.Instance.GetCoins();
            coinsValue.text = $"{coins:00}";
        }

        private void DisplayLives()
        {
            int lives = GameManager.GameManager.Instance.GetLives();
            livesValue.text = $"{lives:0}";
        }
    }
}