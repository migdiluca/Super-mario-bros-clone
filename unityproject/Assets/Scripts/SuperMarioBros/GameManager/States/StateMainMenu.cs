using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SuperMarioBros.GameManager.States
{
    public class StateMainMenu : FrameLord.FSM.State
    {
        protected override void OnEnterState()
        {
            SceneManager.LoadScene("SceneMainMenu");
        }
        
        protected override void OnLeaveState()
        {
            SceneManager.LoadScene("SceneGame");

            GameManager.Instance.Reset();
        }
    }
}
