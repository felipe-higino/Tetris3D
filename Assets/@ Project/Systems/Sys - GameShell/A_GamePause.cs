using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.GameShell.Pause
{
    public abstract class A_GamePause
    {
        public delegate void Del_GamePauseToggle(bool isPaused);
        public virtual event Del_GamePauseToggle OnGamePauseSetted;
        public virtual bool CanPause { get; set; }
        public virtual bool IsPaused { get; protected set; } = false;

        public virtual void PauseGame()
        {
            if (!CanPause)
                return;
            IsPaused = true;
            OnGamePauseSetted?.Invoke(IsPaused);
        }

        public virtual void UnpauseGame()
        {
            if (!CanPause)
                return;
            IsPaused = false;
            OnGamePauseSetted?.Invoke(IsPaused);
        }
    }
}