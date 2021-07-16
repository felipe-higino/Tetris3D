using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.GameShell;

namespace Systems.GameShell.Pause
{
    public class GamePauseCaller : MonoBehaviour
    {
        public void DO_Pause()
        {
            FluxInputs.Instance.GamePause.PauseGame();
        }

        public void DO_Unpause()
        {
            FluxInputs.Instance.GamePause.UnpauseGame();
        }
    }
}
