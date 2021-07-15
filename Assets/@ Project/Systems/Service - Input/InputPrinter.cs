using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.TetrisInput
{
    public class InputPrinter : MonoBehaviour
    {
        private void Start()
        {
            var inputService = FindObjectOfType<A_TetrisInput>();
            if (null == inputService)
            {
                Debug.LogError("no input service found");
                return;
            }

            inputService.OnDash += () => Debug.Log("dash");
            inputService.OnRotateClockwise += () => Debug.Log("rotate clockwise");
        }
    }
}
