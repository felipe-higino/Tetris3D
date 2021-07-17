using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.TetrisInput;
using Libs;

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
        private GameClock dashClock;

        private bool canControl = false;

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

            tetrisGameRules.OnGameStart += OnGameStart;
            tetrisGameRules.OnGameOver += OnGameOver;
        }

        private void OnDestroy()
        {
            inputs.OnMoveHorizontally -= OnMoveHorizontally;
            inputs.OnRotateClockwise -= RotateClockwise;
            inputs.OnDash -= DashDown;
            inputs.OnMoveDown -= MoveDown;

            tetrisGameRules.OnGameStart -= OnGameStart;
            tetrisGameRules.OnGameOver -= OnGameOver;
        }

        private void OnGameStart()
        {
            canControl = true;
        }

        private void OnGameOver()
        {
            canControl = false;
        }

        private void OnMoveHorizontally(int direction)
        {
            if (!canControl)
                return;

            pieceMovementManager.MovePieceHorizontally(direction);
        }

        private void RotateClockwise()
        {
            if (!canControl)
                return;

            pieceMovementManager.RotatePieceClockwise(tetrisGameRules.CurrentPiece);
        }

        private void DashDown()
        {
            if (!canControl)
                return;

            var clockEnd = false;
            TetrisGameRules.Del_Solidify cancelClock =
                (_, __) => clockEnd = true;

            tetrisGameRules.OnSolidify += cancelClock;

            dashClock.OnClockTick += () =>
            {
                // didMovementEnded false negative due to update frequency (race condition)
                // var didMovementEnded = pieceMovementManager.MovePieceDown();

                if (clockEnd)
                {
                    tetrisGameRules.OnSolidify -= cancelClock;
                    dashClock.StopClock();
                    dashClock.CleanAllEvents();
                }
                pieceMovementManager.MovePieceDown();
            };
            dashClock.StartClock();
        }

        private void MoveDown()
        {
            if (!canControl)
                return;

            pieceMovementManager.MovePieceDown();
        }
    }
}
