using SuperMarioBros.GameManager;
using UnityEngine;

namespace SuperMarioBros.Player.States
{
    public class DyingState : FrameLord.FSM.State
    {
        public float dyingJumpVelocity = 160;
        
        private Rigidbody2D _rigidbody2D;
        private GameObject _player;
        private Animator _animator;

        public void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _animator = _player.GetComponent<Animator>();
            _rigidbody2D = _player.GetComponent<Rigidbody2D>();
        }
        
        protected override void OnEnterState()
        {
            foreach(Collider2D c in _player.GetComponents<Collider2D>())
            {
                c.enabled = false;
            }
            _rigidbody2D.velocity = Vector2.up * dyingJumpVelocity;
            _animator.SetBool("isAlive", false);
            MusicManager.Instance.Stop();
            SfkManager.Instance.Play(SfkManager.Instance.MarioDies);
        }
    }
}