using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Libs;

namespace Systems.TetrisInput
{
    public class TetrisInputClassicInputSystem : A_TetrisInput
    {
        public override event Action OnRotateClockwise;
        public override event Action OnMoveDown;
        public override event Action OnDash;
        public override event Action<int> OnMoveHorizontally;

        public override int HorizontalMovement
        {
            get
            {
                var direction = 0;
                if (Input.GetKey(MoveLeftKeycode))
                {
                    direction -= 1;
                }
                if (Input.GetKey(MoveRightKeycode))
                {
                    direction += 1;
                }
                return direction;
            }
        }
        public override bool IsRotatingClockwise => Input.GetKey(RotationKeycode);
        public override bool IsMovingDown => Input.GetKey(MoveDownKeycode);
        public override bool IsDashing => Input.GetKey(DashKeycode);

        private const KeyCode RotationKeycode = KeyCode.UpArrow;
        private const KeyCode MoveLeftKeycode = KeyCode.LeftArrow;
        private const KeyCode MoveRightKeycode = KeyCode.RightArrow;
        private const KeyCode MoveDownKeycode = KeyCode.DownArrow;
        private const KeyCode DashKeycode = KeyCode.Space;

        [SerializeField]
        private float verticalClockInterval = 0.3f;
        [SerializeField]
        private float horizontalClockInterval = 0.3f;
        private GameClock moveDownClock;
        private GameClock rotationClock;
        private GameClock horizontalClock;

        protected override void Awake()
        {
            base.Awake();

            moveDownClock = gameObject.AddComponent<GameClock>();
            rotationClock = gameObject.AddComponent<GameClock>();
            horizontalClock = gameObject.AddComponent<GameClock>();

            moveDownClock.clock = verticalClockInterval;
            rotationClock.clock = verticalClockInterval;
            horizontalClock.clock = horizontalClockInterval;
        }

        private void Update()
        {
            if (Input.GetKeyDown(DashKeycode))
            {
                Debug.Log("dash");
                OnDash?.Invoke();
            }

            if (Input.GetKeyDown(MoveLeftKeycode))
            {
                OnMoveHorizontally?.Invoke(-1);
            }
            if (Input.GetKeyDown(MoveRightKeycode))
            {
                OnMoveHorizontally?.Invoke(+1);
            }

            UpdateHorizontalClock();

            CheckCallInputOrClock(
                Input.GetKeyDown(MoveDownKeycode),
                Input.GetKeyUp(MoveDownKeycode),
                moveDownClock, OnMoveDown);

            CheckCallInputOrClock(
                Input.GetKeyDown(RotationKeycode),
                Input.GetKeyUp(RotationKeycode),
                rotationClock, OnRotateClockwise);

        }

        private void UpdateHorizontalClock()
        {
            var direction = HorizontalMovement;

            if (direction == 0)
            {
                horizontalClock.StopClock();
                horizontalClock.CleanAllEvents();
                return;
            }

            if (horizontalClock.IsClockActive)
            {
                return;
            }

            horizontalClock.OnClockTick += () =>
            {
                OnMoveHorizontally?.Invoke(HorizontalMovement);
            };
            horizontalClock.StartClock();
        }

        private void CheckCallInputOrClock(bool isKeyDown, bool isKeyUp,
            GameClock gameClock, Action callback)
        {
            if (isKeyDown)
            {
                callback?.Invoke();
                gameClock.OnClockTick += () =>
                {
                    callback?.Invoke();
                };
                gameClock.StartClock();
            }

            if (isKeyUp)
            {
                gameClock.StopClock();
                gameClock.CleanAllEvents();
            }
        }
    }

}
