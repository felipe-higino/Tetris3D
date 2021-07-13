using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.TetrisInput
{
    public abstract class A_TetrisInput : MonoBehaviour
    {
        public abstract event Action OnMoveLeft;
        public abstract event Action OnMoveRight;
        public abstract event Action OnDash;
        public abstract event Action OnRotateClockwise;

        public abstract bool MoveLeft { get; }
        public abstract bool MoveRight { get; }
        public abstract bool ClockwiseInput { get; }
        public abstract bool DashInput { get; }
    }
}