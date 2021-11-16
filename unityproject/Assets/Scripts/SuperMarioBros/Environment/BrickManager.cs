using System;
using System.Collections;
using SuperMarioBros.Enemies;
using SuperMarioBros.GameManager;
using SuperMarioBros.Player;
using UnityEngine;

namespace SuperMarioBros.Environment
{
    public class BrickManager : MonoBehaviour
    {
        public GameObject brick;
        public ParticleSystem particles;

        public float bounceHeight = 8;
        public float bounceSpeed = 100;

        private int childPoppedIndex = 0;

        private SpriteRenderer spriteRenderer;
        private BoxCollider2D boxCollider2D;

        private Vector2 originalPosition;

        public enum State
        {
            STILL,
            BOUNCING,
            BACK_TO_NORMAL,
            BROKEN
        }

        public State _state = State.STILL;

        private void Start()
        {
            originalPosition = transform.position;
            spriteRenderer = GetComponent<SpriteRenderer>();
            boxCollider2D = GetComponent<BoxCollider2D>();
        }
        
        public void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Vector2 hit = other.GetContact(0).normal;

                float dot = Vector2.Dot(hit, Vector2.up);

                if (Mathf.Approximately(dot, 0))
                {
                    return;
                }

                PlayerManager playerManager = other.gameObject.GetComponent<PlayerManager>();

                if (dot > 0 && !playerManager.DidAlreadyCollideBrick())
                {
                    playerManager.NotifyBrickCollision();
                    if (playerManager.isBig)
                    {
                        SfkManager.Instance.Play(SfkManager.Instance.BrickSmash);
                        StartCoroutine(Bounce(true));
                    }
                    else
                    {
                        SfkManager.Instance.Play(SfkManager.Instance.Bump);
                        StartCoroutine(Bounce(false));
                    }
                }
            }
        }
        

        private IEnumerator Bounce(bool destroy)
        {
            _state = State.BOUNCING;
            if (destroy)
            {
                spriteRenderer.enabled = false;
                particles.Play();
            }
            
            while (true)
            {
                transform.position = new Vector2(transform.position.x,
                    transform.position.y + bounceSpeed * Time.deltaTime);
                if (transform.position.y >= originalPosition.y + bounceHeight)
                {
                    _state = State.BACK_TO_NORMAL;
                    break;
                }

                yield return null;
            }
            
            if (destroy)
                boxCollider2D.enabled = false;
            
            while (true)
            {
                float yComponent = transform.position.y - bounceSpeed * Time.deltaTime;
                if (yComponent < originalPosition.y)
                    yComponent = originalPosition.y;
                transform.position = new Vector2(transform.position.x, yComponent);
                if (transform.position.y <= originalPosition.y)
                {
                    _state = destroy ? State.BROKEN : State.STILL;
                    break;
                }

                yield return null;
            }

            if (destroy)
            {
                yield return new WaitForSeconds(particles.main.startLifetime.constantMax);
                Destroy(brick);
            }
        }
    }
}