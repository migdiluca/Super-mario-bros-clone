using System;
using SuperMarioBros.Enemies;
using UnityEngine;

namespace SuperMarioBros.Environment
{
    public class EnemyActivator : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<GoombaController>().enabled = true;
            }
        }
    }
}