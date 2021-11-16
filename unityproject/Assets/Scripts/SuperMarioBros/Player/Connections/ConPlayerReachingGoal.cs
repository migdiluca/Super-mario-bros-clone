using FrameLord.EventDispatcher;
using SuperMarioBros.GameEvents;

namespace SuperMarioBros.Player.Connections
{
    public class ConPlayerReachingGoal : FrameLord.FSM.StateConnection
    {
        protected override void OnInit()
        {
            GameEventDispatcher.Instance.AddListener(EnvPlayerReachingGoal.Name, OnPlayerReachingGoal);
        }

        void OnPlayerReachingGoal(System.Object sender, GameEvent e)
        {
            _isFinished = true;
        }
    }
}