using System;
using FrameLord.EventDispatcher;
using SuperMarioBros.GameEvents;
using UnityEngine;

namespace SuperMarioBros.Player.States
{
    public class WinState : FrameLord.FSM.State
    {
        private float _acumTime;
        
        private GameObject _player;
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _spriteRenderer = _player.GetComponent<SpriteRenderer>();
        }

        protected override void OnEnterState()
        {
            _spriteRenderer.enabled = false;
            _acumTime = 0;
        }

        private void Update()
        {
            _acumTime += Time.deltaTime;
            if(_acumTime > 1f)
                Win();
        }

        private void Win()
        {
            GameEventDispatcher.Instance.Dispatch(this, new EnvWin());
        }
    }
}