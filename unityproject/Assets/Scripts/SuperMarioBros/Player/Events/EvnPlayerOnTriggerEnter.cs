using System;
using FrameLord.EventDispatcher;
using UnityEngine;
using Object = System.Object;

namespace SuperMarioBros.Player.Events
{
    public class EvnPlayerTriggerEnter : FrameLord.EventDispatcher.GameEvent
    {
        public const string Name = "EvnPlayerTriggerEnter";
        public Collider2D other;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public EvnPlayerTriggerEnter(Collider2D other)
        {
            eventName = Name;
            this.other = other;
        }
    }
}