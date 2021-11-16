using SuperMarioBros.Environment;
using FrameLord.EventDispatcher;
using SuperMarioBros.Enemies.Events;
using SuperMarioBros.GameManager;
using SuperMarioBros.Player;
using UnityEngine;

namespace SuperMarioBros.Enemies
{
    public class GoombaController : MonoBehaviour
    {
        public float bounceVelocity = 80;
        public int xDirection = 1;
        public float walkingSpeed = 130;
        public int bounceDeadJumpForce = 1400000;
        
        enum State
        {
            Alive, Dying
        }

        private Animator animator;
        private State _state;
        private Rigidbody2D rigidbody2D;
        private BoxCollider2D boxCollider2D;
        private SpriteRenderer spriteRenderer;

        // Start is called before the first frame update
        void Start()
        {
            _state = State.Alive;
            boxCollider2D = GetComponent<BoxCollider2D>();
            rigidbody2D = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            rigidbody2D.velocity = new Vector2(walkingSpeed * xDirection, rigidbody2D.velocity.y);
        }

        // Update is called once per frame
        void Update()
        {
            if(_state == State.Alive)
                rigidbody2D.velocity = new Vector2(walkingSpeed * xDirection, rigidbody2D.velocity.y);
        }

        private void OnCollisionStay2D(Collision2D col)
        {
            if (_state == State.Alive)
            {
                if (col.gameObject.CompareTag("Brick"))
                {
                    BrickManager brickManager = col.gameObject.GetComponent<BrickManager>();
                    if (brickManager._state.Equals(BrickManager.State.BOUNCING))
                    {
                        BounceDead();
                    }
                }
                else if (col.gameObject.CompareTag("QuestionBlock"))
                {
                    QuestionBlock brickManager = col.gameObject.GetComponent<QuestionBlock>();
                    if (brickManager._state.Equals(QuestionBlock.State.BOUNCING))
                    {
                        BounceDead();
                    }
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (_state == State.Dying)
                return;
            
            Vector2 hit = col.GetContact(0).normal;

            float dot = Vector2.Dot(hit, Vector2.right);

            if (col.gameObject.CompareTag("Player"))
            {
                if (Mathf.Approximately(dot, 0))
                {
                    col.gameObject.GetComponent<PlayerManager>().JumpBoost();
                    Die();
                }
                else
                {
                    GameEventDispatcher.Instance.Dispatch(this, new EvnEnemyHitPlayer());
                }
            }
            else
            {
                if (Mathf.Approximately(dot, 0))
                {
                    return;
                }
        
                if (dot > 0)
                {
                    xDirection = 1;
                }
                else
                {
                    xDirection = -1;
                }
                
            }
        }

        private void Die()
        {
            GameManager.GameManager.Instance.NotifyGoombaDied(transform.position);
            SfkManager.Instance.Play(SfkManager.Instance.Stomp);
            
            _state = State.Dying;
            rigidbody2D.velocity = Vector2.zero;
            boxCollider2D.enabled = false;
            rigidbody2D.isKinematic = true;
            animator.SetBool("isDead", true);
        }

        private void BounceDead()
        {
            GameManager.GameManager.Instance.NotifyGoombaDied(transform.position);
            _state = State.Dying;
            rigidbody2D.AddForce(new Vector2(0, bounceDeadJumpForce));
            boxCollider2D.enabled = false;
            spriteRenderer.flipY = true;
        }
    }
}
