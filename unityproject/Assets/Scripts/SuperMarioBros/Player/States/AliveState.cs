using System;
using System.Collections;
using System.Runtime.CompilerServices;
using FrameLord.EventDispatcher;
using SuperMarioBros.Camera;
using SuperMarioBros.Enemies.Events;
using SuperMarioBros.GameEvents;
using SuperMarioBros.GameManager;
using SuperMarioBros.Player.Connections;
using SuperMarioBros.Player.Events;
using UnityEngine;

namespace SuperMarioBros.Player.States
{
    public class AliveState : FrameLord.FSM.State
    {
        public int dieHeight = -500;
        public float blinkFrequency = 0.05f;
        
        private Rigidbody2D _rigidbody2D;
        private Collider2D _collider2D;
        private PlayerManager _playerController;
        private GameObject _player;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private CameraController _cameraController;

        private float moveForce = 520.0f;
        private float maxSpeed = 120;
        private float maxDownSpeed = 200;

        private float _blinkAccumTime = 0f;
        
        private bool boost;
        private bool crouch;

        private bool enteringPipe = false;
        private bool firstJump = false;
        private bool isJumping = false;
        private float initialJumpForce = 10000;
        private float constantJumpForce = 1300;

        private float jumpTime = 0.22f;
        private float jumpTimeCounter;

        private float horizontalDirection = 0;
        private float verticalForce = 0f;

        private float _remainingInvulnerability = 0;

        private bool _isInvulnerable = false;
        
        public void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _animator = _player.GetComponent<Animator>();
            _playerController = _player.GetComponent<PlayerManager>();
            _rigidbody2D = _player.GetComponent<Rigidbody2D>();
            _spriteRenderer = _player.GetComponent<SpriteRenderer>();
            _collider2D = _player.GetComponent<Collider2D>();
            _cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        }
        
        protected override void OnEnterState()
        {
            MusicManager.Instance.Play(MusicManager.Instance.aboveGround);

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
            GameEventDispatcher.Instance.AddListener(EvnPlayerCollisionEntered.Name, PlayerOnCollisionEnter);
            GameEventDispatcher.Instance.AddListener(EvnPlayerOnTriggerStay.Name, PlayerOnTriggerStay);
            GameEventDispatcher.Instance.AddListener(EvnEnemyHitPlayer.Name, OnPlayerHit);

        }

        private void UnsubscribeEvn()
        {
            GameEventDispatcher.Instance.RemoveListener(EvnPlayerCollisionEntered.Name, PlayerOnCollisionEnter);
            GameEventDispatcher.Instance.RemoveListener(EvnPlayerOnTriggerStay.Name, PlayerOnTriggerStay);
            GameEventDispatcher.Instance.RemoveListener(EvnEnemyHitPlayer.Name, OnPlayerHit);
        }
        
        public void Update()
        {
            if(!Mathf.Approximately(_remainingInvulnerability, 0))
            {

                _remainingInvulnerability = Math.Max(0, _remainingInvulnerability - Time.deltaTime);

                _blinkAccumTime += Time.deltaTime;
                if (_blinkAccumTime > blinkFrequency)
                {
                    _blinkAccumTime %= blinkFrequency;
                    _spriteRenderer.enabled = !_spriteRenderer.enabled;
                }
            }
            else
            {
                if (_isInvulnerable)
                {
                    _isInvulnerable = false;
                    _spriteRenderer.enabled = true;
                    _player.layer = LayerMask.NameToLayer("Player");
                }
            }
            
            IsDieHeight();
            boost = Input.GetKey(KeyCode.X);
            crouch = Input.GetKey(KeyCode.DownArrow);
            
            horizontalDirection = Input.GetAxisRaw("Horizontal");
           
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if(_playerController.isGrounded)
                    _spriteRenderer.flipX = false;
                _animator.SetBool("directionRight", true);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                if(_playerController.isGrounded)
                    _spriteRenderer.flipX = true;
                _animator.SetBool("directionRight", false);
            }
            
            if (Input.GetKeyDown(KeyCode.UpArrow) && _playerController.isGrounded)
            {
                SfkManager.Instance.Play(_playerController.isBig ? SfkManager.Instance.JumpSmall 
                        : SfkManager.Instance.JumpSuper);

                isJumping = true;
                firstJump = true;
                jumpTimeCounter = jumpTime;
            }
            else if (Input.GetKey(KeyCode.UpArrow) && isJumping)
            {
                if (jumpTimeCounter > 0)
                {
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }
            else if(Input.GetKeyUp(KeyCode.UpArrow) || _playerController.isGrounded)
            {
                isJumping = false;
            }
            _animator.SetBool("jumping", isJumping);
            _animator.SetBool("isGrounded", _playerController.isGrounded);
            _animator.SetFloat("speed", _rigidbody2D.velocity.x) ;
            float animationSpeed = 1;
            if (Math.Abs(_rigidbody2D.velocity.x) > 0)
                animationSpeed = _rigidbody2D.velocity.x * 1.5f / maxSpeed;
            _animator.SetFloat("animationSpeed", animationSpeed);
        }

        private void PlayerOnTriggerStay(System.Object o, GameEvent e)
        {
            Collider2D other = ((EvnPlayerOnTriggerStay) e).other;
            
            if (!enteringPipe && other.gameObject.CompareTag("PipeEntrance"))
            {
                float rotation = other.gameObject.transform.rotation.z;

                GameObject pipeExit = other.gameObject.transform.GetChild(0).gameObject;

                if (rotation > 0 && _animator.GetBool("directionRight"))
                {
                    MusicManager.Instance.Stop();
                    SfkManager.Instance.Play(SfkManager.Instance.PipeTravel);
                    enteringPipe = true;
                    StartCoroutine(EnterPipe(false, pipeExit));
                } else if (Math.Abs(rotation) < 0.01 && crouch)
                {
                    MusicManager.Instance.Stop();
                    SfkManager.Instance.Play(SfkManager.Instance.PipeTravel);
                    enteringPipe = true;
                    StartCoroutine(EnterPipe(true, pipeExit));
                }
            }
        }
        
        private void PlayerOnCollisionEnter(System.Object o, GameEvent e)
        {
            Collision2D other = ((EvnPlayerCollisionEntered) e).other;
            
            if (other.gameObject.CompareTag("Mushroom"))
            {
                if(!_playerController.isBig)
                    SfkManager.Instance.Play(SfkManager.Instance.PowerUp);
                
                GameManager.GameManager.Instance.NotifyPickedUpMushroom(transform.position);
                _playerController.SetIsBig(true);
                _animator.SetBool("isBig", true);
            }
            if(other.gameObject.CompareTag("Flag"))
            {
                GameEventDispatcher.Instance.Dispatch(this, new EnvPlayerReachingGoal());
            }
        }

        private void OnPlayerHit(System.Object o, GameEvent e)
        {
            if(!Mathf.Approximately(_remainingInvulnerability, 0))
                return;
            
            if (_playerController.isBig)
            {
                _playerController.SetIsBig(false);
                _animator.SetBool("isBig", false);
                
                SfkManager.Instance.Play(SfkManager.Instance.PipeTravel);

                if (!_isInvulnerable)
                {
                    _remainingInvulnerability = _playerController.invulnerabilityTime;
                    _isInvulnerable = true;
                    _player.layer = LayerMask.NameToLayer("Invulnerable");
                }
            }
            else
            {
                _animator.SetBool("isAlive", false);
                GameEventDispatcher.Instance.Dispatch(this, new EvnPlayerDying());
            }
        }

        private void FixedUpdate()
        {            
            _playerController.SetGrounded();
            Vector2 velocity = _rigidbody2D.velocity;
            if (velocity.y <= -maxDownSpeed)
                _rigidbody2D.velocity = new Vector2(velocity.x,-maxDownSpeed);

            float horizontalForce = moveForce * horizontalDirection;
            if (boost && _playerController.isGrounded)
                horizontalForce *= 1;
            else if (!_playerController.isGrounded)
                horizontalForce *= 0.3f;
            if (horizontalForce * velocity.x < 0)
            {
                if(velocity.x < maxSpeed)
                    horizontalForce *= 0.5f;
            }

            float limitSpeed = boost ? maxSpeed * 1.65f : maxSpeed;
            if(Math.Abs(_rigidbody2D.velocity.x) < limitSpeed)
                _rigidbody2D.AddForce(new Vector2(horizontalForce, 0));

            if (isJumping)
            {
                float multiplier = 1;
                if (boost && _rigidbody2D.velocity.x > maxSpeed)
                    multiplier = 1.1f;
                float verticalForce = 0;
                if (firstJump)
                {
                    verticalForce = initialJumpForce * multiplier;
                    firstJump = false;
                }
                else if (jumpTimeCounter > 0)
                {
                    verticalForce = constantJumpForce * multiplier;
                }
                _rigidbody2D.AddForce(new Vector2(0, verticalForce));
            }
        }

        private void IsDieHeight()
        {
            if(_player.transform.position.y < dieHeight)
                GameEventDispatcher.Instance.Dispatch(this, new EvnPlayerDying());
        }
        
        private IEnumerator EnterPipe(bool down, GameObject pipeExit)
        {
            if (down)
            {     
                _rigidbody2D.isKinematic = true;
                _collider2D.enabled = false;
                while (true)
                {
                    _animator.SetFloat("speed", 0);

                    _rigidbody2D.velocity = new Vector2(0, -100);
                    if (_rigidbody2D.transform.position.y <= 5)
                        break;
                    yield return null;
                }

                _player.transform.position = pipeExit.transform.position;
                _cameraController.Center();
                _cameraController.SwapSkyblockColor();
                
                _rigidbody2D.isKinematic = false;
                _collider2D.enabled = true;
            }
            else
            {
                float inicialX = _rigidbody2D.position.x;
                _animator.SetFloat("speed", _playerController.movementVelocity);
                _animator.SetFloat("animationSpeed", 1);
                
                _rigidbody2D.isKinematic = true;
                _collider2D.enabled = false;
                
                _rigidbody2D.position = new Vector2(_rigidbody2D.position.x, _rigidbody2D.position.y + 2);
                while (true)
                {
                    _rigidbody2D.isKinematic = true;
                    _collider2D.enabled = false;
                    
                    _rigidbody2D.velocity = new Vector2(20, 0);
                    if (_rigidbody2D.transform.position.x > inicialX + 20)
                        break;
                    yield return null;
                }
                
                _player.transform.position = pipeExit.transform.position;
                _cameraController.Center();
                _cameraController.SwapSkyblockColor();
                
                _rigidbody2D.isKinematic = false;
                _collider2D.enabled = true;
            }
            
            enteringPipe = false;
        }
    }
}