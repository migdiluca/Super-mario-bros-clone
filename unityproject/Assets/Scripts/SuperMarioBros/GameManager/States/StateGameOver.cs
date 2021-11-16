using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SuperMarioBros.GameManager.States
{
    public class StateGameOver : FrameLord.FSM.State
    {
        protected override void OnEnterState()
        {
            SfkManager.Instance.Play(SfkManager.Instance.GameOver);
            SceneManager.LoadScene("SceneGameOver");
        }
        
        protected override void OnLeaveState()
        {
            
        }
    }
}

