namespace SuperMarioBros.Enemies.Events
{
    public class EvnEnemyHitPlayer : FrameLord.EventDispatcher.GameEvent
    {
        public const string Name = "EvnEnemyHitPlayer";

        /// <summary>
        /// Constructor
        /// </summary>
        public EvnEnemyHitPlayer()
        {
            eventName = Name;
        }
    }
}