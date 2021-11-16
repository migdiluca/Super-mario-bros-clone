namespace SuperMarioBros.GameEvents
{

    public class EvnGamePaused : FrameLord.EventDispatcher.GameEvent
        {
            public const string Name = "EvnGamePaused";
    
            /// <summary>
            /// Constructor
            /// </summary>
            public EvnGamePaused()
            {
                eventName = Name;
            }
        }
}