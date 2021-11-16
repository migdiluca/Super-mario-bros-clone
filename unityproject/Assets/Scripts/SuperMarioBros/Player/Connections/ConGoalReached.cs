using FrameLord.EventDispatcher;
using SuperMarioBros.GameEvents;

namespace SuperMarioBros.Player.Connections
{
    public class ConGoalReached : FrameLord.FSM.StateConnection
    {
        protected override void OnInit()
        {
            GameEventDispatcher.Instance.AddListener(EvnGoalReached.Name, OnGoalReached);
        }

        void OnGoalReached(System.Object sender, GameEvent e)
        {
            _isFinished = true;
        }
    }
}