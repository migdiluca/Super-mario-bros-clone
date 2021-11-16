using System.Collections;
using UnityEngine;

namespace SuperMarioBros.Items
{
    public class MushroomManager : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private CapsuleCollider2D capsuleCollider2D;
        public GameObject mushroom;

        private bool spawned = false;
        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                StartCoroutine(DestroyObject());
            }
        }

        private IEnumerator DestroyObject()
        {
            capsuleCollider2D.enabled = false;
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(5);
            Destroy(mushroom);
        }
    }
}