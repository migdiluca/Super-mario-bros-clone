using System;
using System.Collections;
using FrameLord.EventDispatcher;
using SuperMarioBros.Environment;
using SuperMarioBros.GameEvents;
using SuperMarioBros.Player.Connections;
using SuperMarioBros.Player.Events;
using UnityEngine;

namespace SuperMarioBros.Player
{
    public class PlayerManager : MonoBehaviour
    {
        
        public float movementVelocity = 130;

        public float enemyJumpBoostForce = 8000;
        
        public float dyingJumpVelocity = 160;

        public float invulnerabilityTime = 3f;
        
        public bool isBig = false;
    
        private Rigidbody2D rigidbody2D;

        private CapsuleCollider2D capsuleCollider2D;

        private Animator animator;

        private bool alreadyCollideBrick = false;

        public bool isGrounded;

        [SerializeField]
        Transform groundCheck;
    
        [SerializeField]
        Transform groundCheckL;
    
        [SerializeField]
        Transform groundCheckR;
    
        // Start is called before the first frame update
        void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        }

        public void SetGrounded()
        {
            if(Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")) ||
               Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground")) ||
               Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground")))
            {
                isGrounded = true;
                alreadyCollideBrick = false;
            }
            else
            {
                isGrounded = false;
            }
        }

        public void SetIsBig(bool isBig)
        {
            this.isBig = isBig;
            ChangeSize(isBig);
        }
        
        public void OnTriggerStay2D(Collider2D other)
        {
            GameEventDispatcher.Instance.Dispatch(this, new EvnPlayerOnTriggerStay(other));
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            GameEventDispatcher.Instance.Dispatch(this, new EvnPlayerCollisionEntered(other));
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            GameEventDispatcher.Instance.Dispatch(this, new EvnPlayerTriggerEnter(other));
        }

        public void JumpBoost()
        {
            rigidbody2D.AddForce(new Vector2(0,enemyJumpBoostForce));
            alreadyCollideBrick = false;
        }

        public bool DidAlreadyCollideBrick()
        {
            return alreadyCollideBrick;
        }

        public void NotifyBrickCollision()
        {
            alreadyCollideBrick = true;
        }

        private void ChangeSize(bool big)
        {
            if (big)
            {
                capsuleCollider2D.offset = new Vector2(0,18);
                capsuleCollider2D.size = new Vector2(10,28);
            }
            else
            {
                capsuleCollider2D.offset = new Vector2(0,9);
                capsuleCollider2D.size = new Vector2(10,14);
            }
        }
    }
}
