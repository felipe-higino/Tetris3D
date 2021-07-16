using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.TetrisInput;
using Libs;
using Systems.GameShell;

namespace Systems.TetrisGame
{
    public class PieceControlling : MonoBehaviour
    {
        [Header("Core references")]
        [Space(15)]
        [SerializeField]
        private A_TetrisInput inputs;
        [SerializeField]
        private PieceMovementManager pieceMovementManager;

        [Header("Parameter references")]
        [Space(15)]
        [SerializeField]
        private TetrisGameRules tetrisGameRules;


        [Header("Parameters")]
        [Space(15)]
        [SerializeField]
        private float dashClockInterval = 0.05f;
        [SerializeField]
        private float timeBetweenDashes = 0.1f;
        private GameClock dashClock;

        private bool IsGamePaused => FluxInputs.Instance.GamePause.IsPaused;

        private void Awake()
        {
            dashClock = gameObject.AddComponent<GameClock>();
            dashClock.clock = dashClockInterval;
        }

        private void Start()
        {
            inputs.OnMoveHorizontally += OnMoveHorizontally;
            inputs.OnRotateClockwise += RotateClockwise;
            inputs.OnDash += DashDown;
            inputs.OnMoveDown += MoveDown;
        }

        private void OnDestroy()
        {
            inputs.OnMoveHorizontally -= OnMoveHorizontally;
            inputs.OnRotateClockwise -= RotateClockwise;
            inputs.OnDash -= DashDown;
            inputs.OnMoveDown -= MoveDown;
        }

        private void OnMoveHorizontally(int direction)
        {
            if (IsGamePaused)
                return;

            pieceMovementManager.MovePieceHorizontally(direction);
        }

        private void RotateClockwise()
        {
            if (IsGamePaused)
                return;

            pieceMovementManager.RotatePieceClockwise(tetrisGameRules.CurrentPiece);
        }

        private void DashDown()
        {
            if (IsGamePaused)
                return;

            dashClock.OnClockTick += () =>
            {
                var didMovementEnded = pieceMovementManager.MovePieceDown();
                if (didMovementEnded)
                {
                    dashClock.StopClock();
                    dashClock.CleanAllEvents();
                }
            };
            dashClock.StartClock();
        }

        private void MoveDown()
        {
            if (IsGamePaused)
                return;

            pieceMovementManager.MovePieceDown();
        }
    }
}
