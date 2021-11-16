using System;
using SuperMarioBros.GameManager;
using UnityEngine;

namespace SuperMarioBros.Items
{
    public class Coin : MonoBehaviour
    {
        public GameObject coin;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                SfkManager.Instance.Play(SfkManager.Instance.Coin);
                
                GameManager.GameManager.Instance.NotifyPickedUpCoin(transform.position);
                Destroy(coin);
            }
        }
    }
}