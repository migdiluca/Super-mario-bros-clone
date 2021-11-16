using UnityEngine;

namespace SuperMarioBros.Player.Events
{
    public class EvnPlayerOnTriggerStay : FrameLord.EventDispatcher.GameEvent
    {
        public const string Name = "EvnPlayerOnTriggerStay";
        public Collider2D other;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public EvnPlayerOnTriggerStay(Collider2D other)
        {
            eventName = Name;
            this.other = other;
        }
    }
}