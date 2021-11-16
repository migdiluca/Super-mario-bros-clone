namespace SuperMarioBros.GameEvents
{
    public class EvnEnteredCastleDoor : FrameLord.EventDispatcher.GameEvent
    {
        public const string Name = "EvnEnteredCastleDoor";

        /// <summary>
        /// Constructor
        /// </summary>
        public EvnEnteredCastleDoor()
        {
            eventName = Name;
        }
    }
}