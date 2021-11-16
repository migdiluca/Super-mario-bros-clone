using UnityEngine.SceneManagement;

namespace SuperMarioBros.GameManager.States
{
    public class StateWin : FrameLord.FSM.State
    {
        protected override void OnEnterState()
        {
            SceneManager.LoadScene("SceneWin");
        }
        
        protected override void OnLeaveState()
        {
            
        }
    }
}