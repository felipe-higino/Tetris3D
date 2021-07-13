using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.TetrisInput;
using System;

namespace Systems.PieceControlling
{
    public class TetrisPieceRotation : MonoBehaviour
    {
        [SerializeField]
        private Transform toRotate;

        private A_TetrisInput inputService => A_TetrisInput.Instance;

        private void Start()
        {
            if (null == inputService)
            {
                Debug.LogError("no input service found");
                return;
            }

            inputService.OnRotateClockwise += OnRotateClockwise;
        }

        private void OnDestroy()
        {
            inputService.OnRotateClockwise -= OnRotateClockwise;
        }

        private void OnRotateClockwise()
        {
            toRotate.Rotate(-transform.forward, 90f);
        }
    }

}
