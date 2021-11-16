using FrameLord.EventDispatcher;

namespace SuperMarioBros.Player.Connections
{
    public class ConPlayerDying : FrameLord.FSM.StateConnection
    {
        protected override void OnInit()
        {
            GameEventDispatcher.Instance.AddListener(EvnPlayerDying.Name, OnPlayerDying);
        }

        void OnPlayerDying(System.Object sender, GameEvent e)
        {
            _isFinished = true;
        }
    }
}