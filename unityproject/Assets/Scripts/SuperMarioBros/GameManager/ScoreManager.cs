using System;
using FrameLord.EventDispatcher;
using SuperMarioBros.GameEvents;
using UnityEngine;

namespace SuperMarioBros.GameManager
{
    public class ScoreManager
    {
        public int GoombaKillScore = 100;
        public int CoinScore = 200;
        public int MushroomScore = 1000;
        public float ComboThreshold = 0.7f;
        
        private int _comboCount;
        private float? _lastKillTime;

        private int _score;
        
        public ScoreManager()
        {
            _comboCount = 0;
            _score = 0;
        }

        public int GetScore()
        {
            return _score;
        }

        public void NotifyGoombaDied(float time, Vector2 pos)
        {
            EnemyDied(time, pos, GoombaKillScore);
        }

        public void NotifyPickedUpCoin(Vector2 pos)
        {
            _score += CoinScore;
            GameEventDispatcher.Instance.Dispatch(this, new EvnScoreIncrement(CoinScore, pos));
        }

        public void NotifyPickedUpMushroom(Vector2 pos)
        {
            _score += MushroomScore;
            GameEventDispatcher.Instance.Dispatch(this, new EvnScoreIncrement(MushroomScore, pos));
        }

        private void EnemyDied(float time, Vector2 pos, int killScore)
        {
            CalculateCombo(time);
            int inc = CalculateScore(killScore);

            GameEventDispatcher.Instance.Dispatch(this, new EvnScoreIncrement(inc, pos));
        }

        private void CalculateCombo(float time)
        {
            if (_lastKillTime.HasValue && time - _lastKillTime < ComboThreshold)
                _comboCount++;
            else
                _comboCount = 0;
            
            _lastKillTime = time;
        }

        private int CalculateScore(int baseScore)
        {
            int scoreInc = (int)(baseScore * Math.Pow(2, _comboCount));

            _score += scoreInc;

            return scoreInc;
        }
    }
}