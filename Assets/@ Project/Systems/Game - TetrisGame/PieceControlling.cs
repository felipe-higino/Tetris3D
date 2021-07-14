using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.TetrisInput;
using System;

namespace Systems.TetrisGame
{
    public class PieceControlling : MonoBehaviour
    {
        [SerializeField]
        private A_TetrisInput inputs;

        private void Start()
        {
            inputs.OnMoveLeft += MoveLeft;
            inputs.OnMoveRight += MoveRight;
            inputs.OnRotateClockwise += RotateClockwise;
        }

        private void OnDestroy()
        {
            inputs.OnMoveLeft -= MoveLeft;
            inputs.OnMoveRight -= MoveRight;
            inputs.OnRotateClockwise -= RotateClockwise;
        }

        private void MoveLeft()
        {
            throw new NotImplementedException();
        }

        private void MoveRight()
        {
            throw new NotImplementedException();
        }

        private void RotateClockwise()
        {
            throw new NotImplementedException();
        }
    }
}
