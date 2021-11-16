namespace SuperMarioBros.GameEvents
{
    public class EnvGameUnpaused : FrameLord.EventDispatcher.GameEvent
    {
        public const string Name = "EnvGameUnpaused";
    
        /// <summary>
        /// Constructor
        /// </summary>
        public EnvGameUnpaused()
        {
            eventName = Name;
        }
    }
}