namespace SuperMarioBros.Player.Connections
{
    public class EvnPlayerDying : FrameLord.EventDispatcher.GameEvent
    {
        public const string Name = "EvnPlayerDying";

        /// <summary>
        /// Constructor
        /// </summary>
        public EvnPlayerDying()
        {
            eventName = Name;
        }
    }
}