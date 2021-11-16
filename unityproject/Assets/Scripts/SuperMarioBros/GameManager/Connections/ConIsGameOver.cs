using FrameLord.EventDispatcher;
using SuperMarioBros.GameEvents;

namespace SuperMarioBros.GameManager.Connections
{
    public class ConIsGameOver : FrameLord.FSM.StateConnection
    {
        protected override void OnCheckCondition()
        {
            if (GameManager.Instance.IsGameOver())
                _isFinished = true;
        }
    }
}