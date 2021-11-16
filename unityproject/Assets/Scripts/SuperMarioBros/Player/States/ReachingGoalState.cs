using System;
using FrameLord.EventDispatcher;
using SuperMarioBros.GameEvents;
using SuperMarioBros.GameManager;
using SuperMarioBros.Player.Events;
using UnityEngine;

namespace SuperMarioBros.Player.States
{
    public class ReachingGoalState : FrameLord.FSM.State
    {
        public float slideDownVelocity = 100;
        
        private GameObject _player;
        private Rigidbody2D _rigidbody2D;
        private PlayerManager _playerManager;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;

        private Status _state;

        enum Status
        {
            WaitingForFlag, WalkingTowardsDoor, EnteringDoor
        }

        private float _animAcumTime;
        enum WalkingTowardsDoorStates
        {
            Start, Flipped, WalkingTowardsDoor
        }

        private WalkingTowardsDoorStates _animState = WalkingTowardsDoorStates.Start;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _rigidbody2D = _player.GetComponent<Rigidbody2D>();
            _playerManager = _player.GetComponent<PlayerManager>();
            _animator = _player.GetComponent<Animator>();
            _spriteRenderer = _player.GetComponent<SpriteRenderer>();
        }

        protected override void OnEnterState()
        {
            _state = Status.WaitingForFlag;
            _animator.SetBool("isFlag", true);
            _animator.SetFloat("animationSpeed", 1);
            SubscribeEvn();
        }

        protected override void OnLeaveState()
        {
            UnsubscribeEvn();
        }

        private void OnDestroy()
        {
            UnsubscribeEvn();
        }

        private void SubscribeEvn()
        {
            GameEventDispatcher.Instance.AddListener(EvnFlagIsDown.Name, OnFlagIsDown);
            GameEventDispatcher.Instance.AddListener(EvnPlayerTriggerEnter.Name, OnPlayerTriggerEnter);
        }

        private void UnsubscribeEvn()
        {
            GameEventDispatcher.Instance.RemoveListener(EvnFlagIsDown.Name, OnFlagIsDown);
            GameEventDispatcher.Instance.RemoveListener(EvnPlayerTriggerEnter.Name, OnPlayerTriggerEnter);
        }

        private void Update()
        {
            switch (_state)
            {
                case Status.WaitingForFlag:
                    if(!_playerManager.isGrounded)
                        _rigidbody2D.velocity = Vector2.down * slideDownVelocity;
                    break;
                case Status.WalkingTowardsDoor:
                    WalkingTowardsDoorAnim();
                    break;
                case Status.EnteringDoor:
                    _rigidbody2D.velocity = Vector2.zero;
                    break;
            }
            
        }

        private void OnPlayerTriggerEnter(System.Object o, GameEvent e)
        {
            Collider2D other = ((EvnPlayerTriggerEnter) e).other;
            
            if (other.gameObject.CompareTag("CastleDoor"))
            {
                OnEnteredCastle();
            }
        }

        private void OnFlagIsDown(System.Object obj, GameEvent e)
        {
            _state = Status.WalkingTowardsDoor;
        }

        private void OnEnteredCastle()
        {
            SfkManager.Instance.Play(SfkManager.Instance.StageClear);
            
            _state = Status.EnteringDoor;
            _rigidbody2D.velocity = Vector2.zero;
            GameEventDispatcher.Instance.Dispatch(this, new EvnGoalReached());
        }

        private void WalkingTowardsDoorAnim()
        {
            switch (_animState)
            {
                case WalkingTowardsDoorStates.Start:
                    _animator.SetFloat("animationSpeed", 0);
                    _player.transform.position += Vector3.right * 16;
                    _spriteRenderer.flipX = true;
                    _animState = WalkingTowardsDoorStates.Flipped;
                    break;
                case WalkingTowardsDoorStates.Flipped:
                    _animAcumTime += Time.deltaTime;
                    if (_animAcumTime > 1f)
                    {
                        _animator.SetBool("isFlag", false);
                        _spriteRenderer.flipX = false;
                        _animState = WalkingTowardsDoorStates.WalkingTowardsDoor;
                    }
                    break;
                case WalkingTowardsDoorStates.WalkingTowardsDoor:
                    _rigidbody2D.velocity = new Vector2(_playerManager.movementVelocity, _rigidbody2D.velocity.y);
                    _animator.SetFloat("speed", _playerManager.movementVelocity);
                    _animator.SetFloat("animationSpeed", 1);
                    break;
            }
            
        }
    }
}