using UnityEngine;

namespace SuperMarioBros.GameEvents
{

    public class EvnScoreIncrement : FrameLord.EventDispatcher.GameEvent
    {
        public const string Name = "EvnScoreIncrement";
        public Vector2 Position;
        public int Increment;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public EvnScoreIncrement(int increment, Vector2 pos)
        {
            eventName = Name;
            Position = pos;
            Increment = increment;
        }
    }
}