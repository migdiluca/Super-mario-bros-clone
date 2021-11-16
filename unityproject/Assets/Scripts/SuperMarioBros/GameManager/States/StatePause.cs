using FrameLord.EventDispatcher;
using SuperMarioBros.GameEvents;
using UnityEngine;

namespace SuperMarioBros.GameManager.States
{
    public class StatePause : FrameLord.FSM.State
    {
        protected override void OnEnterState()
        {
            Time.timeScale = 0f;
            SfkManager.Instance.Play(SfkManager.Instance.Pause);
            GameEventDispatcher.Instance.Dispatch(this, new EvnGamePaused());
        }
        
        protected override void OnLeaveState()
        {
            Time.timeScale = 1f;
            GameEventDispatcher.Instance.Dispatch(this, new EnvGameUnpaused());
        }
    }
}
