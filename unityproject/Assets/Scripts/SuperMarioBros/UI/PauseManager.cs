using System;
using FrameLord.EventDispatcher;
using SuperMarioBros.GameEvents;
using UnityEngine;
using Object = System.Object;

namespace SuperMarioBros.UI
{
    public class PauseManager : MonoBehaviour
    {
        private void Start()
        {
            transform.GetChild(0).gameObject.SetActive(false);
            
            GameEventDispatcher.Instance.AddListener(EvnGamePaused.Name, OnGamePaused);
            GameEventDispatcher.Instance.AddListener(EnvGameUnpaused.Name, OnGameUnpaused);
        }

        private void OnDestroy()
        {
            GameEventDispatcher.Instance.RemoveListener(EvnGamePaused.Name, OnGamePaused);
            GameEventDispatcher.Instance.RemoveListener(EnvGameUnpaused.Name, OnGameUnpaused);
        }

        private void OnGamePaused(Object o, GameEvent e)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        
        private void OnGameUnpaused(Object o, GameEvent e)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}