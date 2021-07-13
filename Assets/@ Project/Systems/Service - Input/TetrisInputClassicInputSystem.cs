using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Systems.TetrisInput
{
    public class TetrisInputClassicInputSystem : A_TetrisInput
    {
        public override event Action OnMoveLeft;
        public override event Action OnMoveRight;
        public override event Action OnDash;
        public override event Action OnRotateClockwise;

        public override bool MoveLeft => Input.GetKeyDown(KeyCode.LeftArrow);
        public override bool MoveRight => Input.GetKeyDown(KeyCode.RightArrow);
        public override bool ClockwiseInput => Input.GetKeyDown(KeyCode.UpArrow);
        public override bool DashInput => Input.GetKeyDown(KeyCode.Space);

        private void Update()
        {
            if (!(MoveRight && MoveLeft))
            {
                if (MoveRight)
                {
                    // Debug.Log("move right");
                    OnMoveRight?.Invoke();
                }
                else if (MoveLeft)
                {
                    // Debug.Log("move left");
                    OnMoveLeft?.Invoke();
                }
            }

            if (ClockwiseInput)
            {
                // Debug.Log("clockwise");
                OnRotateClockwise?.Invoke();
            }

            if (DashInput)
            {
                // Debug.Log("dash");
                OnDash?.Invoke();
            }
        }
    }

}
