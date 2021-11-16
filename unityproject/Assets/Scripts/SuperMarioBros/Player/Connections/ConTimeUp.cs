namespace SuperMarioBros.Player.Connections
{
    public class ConTimeUp : FrameLord.FSM.StateConnection
    {
        protected override void OnCheckCondition()
        {
            if (GameManager.GameManager.Instance.IsTimeUp())
                _isFinished = true;
        }
    }
}