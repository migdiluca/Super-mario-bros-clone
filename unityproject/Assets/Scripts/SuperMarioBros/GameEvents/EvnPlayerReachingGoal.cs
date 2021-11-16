namespace SuperMarioBros.GameEvents
{
    public class EnvPlayerReachingGoal : FrameLord.EventDispatcher.GameEvent
    {
        public const string Name = "EnvPlayerReachingGoal";

        /// <summary>
        /// Constructor
        /// </summary>
        public EnvPlayerReachingGoal()
        {
            eventName = Name;
        }
    }
}