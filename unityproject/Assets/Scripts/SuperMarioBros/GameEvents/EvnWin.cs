namespace SuperMarioBros.GameEvents
{
    public class EnvWin : FrameLord.EventDispatcher.GameEvent
    {
        public const string Name = "EvnWin";

        /// <summary>
        /// Constructor
        /// </summary>
        public EnvWin()
        {
            eventName = Name;
        }
    }
}