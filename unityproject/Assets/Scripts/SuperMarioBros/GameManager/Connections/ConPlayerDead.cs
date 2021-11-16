using FrameLord.EventDispatcher;
using SuperMarioBros.GameEvents;
using UnityEngine;

namespace SuperMarioBros.GameManager.Connections
{
    public class ConPlayerDead : FrameLord.FSM.StateConnection
    {
        protected override void OnInit()
        {
            GameEventDispatcher.Instance.AddListener(EvnPlayerDied.Name, OnPlayerDead);
        }

        void OnPlayerDead(System.Object sender, GameEvent e)
        {
            _isFinished = true;
        }
    }
}