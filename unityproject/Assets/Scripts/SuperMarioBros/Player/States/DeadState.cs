using FrameLord.EventDispatcher;
using SuperMarioBros.GameEvents;
using UnityEngine.UIElements;

namespace SuperMarioBros.Player.States
{
    public class DeadState : FrameLord.FSM.State
    {
        protected override void OnEnterState()
        {
            GameEventDispatcher.Instance.Dispatch(this, new EvnPlayerDied());
        }
        
        protected override void OnLeaveState()
        {
            
        }
    }
}