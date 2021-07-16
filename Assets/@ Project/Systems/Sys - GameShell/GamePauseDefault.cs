using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.GameShell.Pause
{
    public class GamePauseDefault : A_GamePause
    {
        public override void PauseGame()
        {
            // Debug.Log("paused");
            Time.timeScale = 0f;

            base.PauseGame();
        }

        public override void UnpauseGame()
        {
            // Debug.Log("unpaused");
            Time.timeScale = 1f;

            base.UnpauseGame();
        }
    }

}
