namespace SuperMarioBros.GameEvents
{
    public class EvnFlagIsDown : FrameLord.EventDispatcher.GameEvent
    {
        public const string Name = "EvnFlagIsDown";

        /// <summary>
        /// Constructor
        /// </summary>
        public EvnFlagIsDown()
        {
            eventName = Name;
        }
    }
}