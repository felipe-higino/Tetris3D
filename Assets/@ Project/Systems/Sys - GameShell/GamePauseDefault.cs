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
            if (!CanPause)
                return;
            base.PauseGame();
            Time.timeScale = 0f;
        }

        public override void UnpauseGame()
        {
            if (!CanPause)
                return;
            base.UnpauseGame();
            Time.timeScale = 1f;
        }
    }

}
