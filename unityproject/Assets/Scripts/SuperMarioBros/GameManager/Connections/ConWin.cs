using FrameLord.EventDispatcher;
using SuperMarioBros.GameEvents;

namespace SuperMarioBros.GameManager.Connections
{
    public class ConWin : FrameLord.FSM.StateConnection
    {
        protected override void OnInit()
        {
            GameEventDispatcher.Instance.AddListener(EnvWin.Name, OnPlayerWin);
        }

        void OnPlayerWin(System.Object sender, GameEvent e)
        {
            _isFinished = true;
        }
    }
}