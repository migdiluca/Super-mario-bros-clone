namespace SuperMarioBros.GameEvents
{
    public class EvnGoalReached : FrameLord.EventDispatcher.GameEvent
    {
        public const string Name = "EvnGoalReached";

        /// <summary>
        /// Constructor
        /// </summary>
        public EvnGoalReached()
        {
            eventName = Name;
        }
    }
}