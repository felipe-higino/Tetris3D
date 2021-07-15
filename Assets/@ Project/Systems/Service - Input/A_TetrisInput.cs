using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.TetrisInput
{
    public abstract class A_TetrisInput : MonoBehaviour
    {
        public static A_TetrisInput Instance { get; private set; }

        public abstract event Action<int> OnMoveHorizontally;
        public abstract event Action OnRotateClockwise;
        public abstract event Action OnMoveDown;
        public abstract event Action OnDash;

        public abstract int HorizontalMovement { get; }
        public abstract bool IsRotatingClockwise { get; }
        public abstract bool IsMovingDown { get; }
        public abstract bool IsDashing { get; }

        protected virtual void Awake()
        {
            if (null == Instance)
                Instance = this;
            else
                Destroy(this.gameObject);
        }
    }
}