using System;
using System.Collections;
using System.Collections.Generic;
using SuperMarioBros.GameManager;
using SuperMarioBros.Player;
using UnityEngine;

namespace SuperMarioBros.Environment
{
    public class QuestionBlock : MonoBehaviour
    {
        public bool secret = false;
        public float bounceHeight = 8;
        public float bounceSpeed = 100;

        public int mushroomSpeed = 30;

        public SpriteRenderer spriteRenderer;
        public Sprite usedSprite;

        public Stack<Transform> children; 

        private Vector2 originalPosition;

        public enum State
        {
            STILL,
            BOUNCING,
            BACK_TO_NORMAL,
            USED
        }

        public State _state;

        private bool canBounce = true;

        void Start()
        {
            if (secret)
                GetComponent<Animator>().enabled = false;
            spriteRenderer = GetComponent<SpriteRenderer>();
            _state = State.STILL;
            originalPosition = transform.position;
            
            children = new Stack<Transform>();
            for (int i = 0; i < transform.childCount; ++i)
                children.Push(transform.GetChild(i));
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_state == State.STILL)
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
                    GameObject child = children.Pop().gameObject;
                    child.SetActive(true);
                    if (child.CompareTag("Mushroom"))
                    {
                        SfkManager.Instance.Play(SfkManager.Instance.PowerUpAppears);
                        
                        StartCoroutine(PopMushroom(child));
                    }
                    else if (child.CompareTag("Coin"))
                    {
                        SfkManager.Instance.Play(SfkManager.Instance.Coin);
                        
                        GameManager.GameManager.Instance.NotifyPickedUpCoin(child.transform.position);
                    }

                    StartCoroutine(Bounce());
                    if (children.Count == 0)
                    {
                        GetComponent<Animator>().enabled = false;
                        spriteRenderer.sprite = usedSprite;
                    }
                }
            }
        }
        private IEnumerator PopMushroom(GameObject mushroom)
        {
            while (true)
            {
                mushroom.transform.localPosition = new Vector2(0,
                    mushroom.transform.localPosition.y + mushroomSpeed * Time.deltaTime);
                if (mushroom.transform.localPosition.y >= 16)
                    break;
                yield return null;
            }

            mushroom.GetComponent<Rigidbody2D>().isKinematic = false;
            mushroom.GetComponent<RandomWalker>().enabled = true;
        }
        
        private IEnumerator Bounce()
        {
            _state = State.BOUNCING;
            
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
            
            while (true)
            {
                float yComponent = transform.position.y - bounceSpeed * Time.deltaTime;
                if (yComponent < originalPosition.y)
                    yComponent = originalPosition.y;
                transform.position = new Vector2(transform.position.x, yComponent);
                if (transform.position.y <= originalPosition.y)
                {
                    if (children.Count == 0)
                        _state = State.USED;
                    else
                        _state = State.STILL;
                    break;
                }
                
                yield return null;
            }
        }
    }
}