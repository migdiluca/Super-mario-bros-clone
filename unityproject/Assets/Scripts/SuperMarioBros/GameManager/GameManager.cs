using System;
using FrameLord.Core;
using FrameLord.EventDispatcher;
using SuperMarioBros.GameEvents;
using UnityEngine;

namespace SuperMarioBros.GameManager
{
    public class GameManager : MonoBehaviorSingleton<GameManager>
    {
        public int startingLives = 3;
        public int maxTime = 400;
        public int scoreCalculationSpeed = 20;
        
        private int _lives;
        private float _elapsedTime;
        private float _remainingTime;
        private ScoreManager _scoreManager;
        private int _coins;
        
        

        enum State
        {
            Playing, Win
        }

        private State _state;
        
        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(this);
            GameEventDispatcher.Instance.AddListener(EvnPlayerDied.Name, OnPlayerDied);
            GameEventDispatcher.Instance.AddListener(EnvWin.Name, (o, e)=>NotifyWin());
            Reset();
        }

        // Update is called once per frame
        void Update()
        {
            switch (_state)
            {
                case State.Playing:
                    _elapsedTime += Time.deltaTime;
                    _remainingTime = maxTime - _elapsedTime;
                    break;
                case State.Win:
                    float sub = _remainingTime - scoreCalculationSpeed;
                    break;
            }

        }

        public void Reset()
        {
            _lives = startingLives;
            _elapsedTime = 0;
            _scoreManager = new ScoreManager();
            _coins = 0;
            _state = State.Playing;
            _remainingTime = 0;
        }

        public bool IsGameOver()
        {
            return _lives == 0;
        }

        public bool IsTimeUp()
        {
            return _elapsedTime > maxTime;
        }

        private void OnPlayerDied(System.Object o, GameEvent e)
        {
            _lives = IsTimeUp() ? 0 : Mathf.Max(_lives - 1, 0);
            
            GameEventDispatcher.Instance.Dispatch(this, new EvnPlayerDead());
        }

        public void NotifyWin()
        {
            _state = State.Win;
        }
        
        public void NotifyPickedUpCoin(Vector2 position)
        {
            _coins++;
            
            _scoreManager.NotifyPickedUpCoin(position);
        }

        public void NotifyGoombaDied(Vector2 pos)
        {
            _scoreManager.NotifyGoombaDied(_elapsedTime, pos);
        }

        public void NotifyPickedUpMushroom(Vector2 pos)
        {
            _scoreManager.NotifyPickedUpMushroom(pos);
        }

        public int GetRemainingTime()
        {
            return (int) _remainingTime;
        }

        public int GetScore()
        {
            return _scoreManager.GetScore();
        }

        public int GetCoins()
        {
            return _coins;
        }

        public int GetLives()
        {
            return _lives;
        }
    }
}
