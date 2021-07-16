using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.GameShell;

namespace Systems.GameShell.Pause
{
    public class SetCanPause : MonoBehaviour
    {
        public bool CanPause
        {
            set => GameLoopManager.Instance.GamePause.CanPause = value;
        }
    }
}
