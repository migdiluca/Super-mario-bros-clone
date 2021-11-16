namespace SuperMarioBros.GameEvents
{
  public class EvnPlayerDead : FrameLord.EventDispatcher.GameEvent
    {
        public const string Name = "EvnPlayerDead";

        /// <summary>
        /// Constructor
        /// </summary>
        public EvnPlayerDead()
        {
            eventName = Name;
        }
    }
}