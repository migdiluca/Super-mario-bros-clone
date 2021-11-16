using UnityEngine;

namespace SuperMarioBros.Player.Events
{
    public class EvnPlayerCollisionEntered : FrameLord.EventDispatcher.GameEvent
    {
        public const string Name = "EvnPlayerCollisionEntered";
        public Collision2D other;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public EvnPlayerCollisionEntered(Collision2D other)
        {
            eventName = Name;
            this.other = other;
        }
    }
}