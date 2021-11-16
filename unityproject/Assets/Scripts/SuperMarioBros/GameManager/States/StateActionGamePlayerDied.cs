using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SuperMarioBros.GameManager.States
{
    public class StateActionGamePlayerDied : FrameLord.FSM.State
    {
        protected override void OnEnterState()
        {
            
        }
        
        protected override void OnLeaveState()
        {
            SceneManager.LoadScene("SceneGame");
        }
    }
}
