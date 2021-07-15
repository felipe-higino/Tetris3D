using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.GridSystem;
using Systems.TetrisPiece;
using Systems.TetrisInput;
using Libs;

namespace Systems.TetrisGame
{
    public class TetrisGameRules : MonoBehaviour
    {
        public SO_TetrisPiece CurrentPiece { get; private set; }

        [Header("Parameters")]
        [Space(15)]
        [SerializeField]
        private float pieceFallClockInterval = 1f;

        [Header("References")]
        [Space(15)]
        [SerializeField]
        private RandomizeTetrisPiece pieceRandomizer;
        [SerializeField]
        private PieceMovementManager pieceMovementManager;
        [SerializeField]
        private SceneGrid solidPiecesGrid;
        [SerializeField]
        private A_TetrisInput inputs;

        private GameClock pieceFallClock;

        [ContextMenu("start new game")]
        public void StartNewGame()
        {
            SpawnNewPiece();
            ClearGrid();
        }

        private void Awake()
        {
            pieceFallClock = gameObject.AddComponent<GameClock>();
            pieceFallClock.clock = pieceFallClockInterval;

            pieceFallClock.OnClockTick += PieceFall;
            pieceMovementManager.OnPieceRequireSolidify += SolidifyPiece;
        }

        private void OnDestroy()
        {
            pieceFallClock.OnClockTick -= PieceFall;
            pieceMovementManager.OnPieceRequireSolidify -= SolidifyPiece;
        }

        private void PieceFall()
        {
            var isNotMovingDown = !(inputs.IsDashing || inputs.IsMovingDown);
            if (isNotMovingDown)
                pieceMovementManager.MovePieceDown();
        }

        private void SolidifyPiece(Vector2Int[] pieceCells)
        {
            foreach (var cell in pieceCells)
            {
                solidPiecesGrid.GridSystem
                    .SetCellState(cell.y, cell.x, true);
            }
            solidPiecesGrid.UpdateGizmosWithSolidCells();
            SpawnNewPiece();
        }

        private void SpawnNewPiece()
        {
            pieceFallClock.StopClock();
            pieceFallClock.StartClock();
            if (pieceRandomizer.Piece == null)
            {
                pieceRandomizer.RandomizePiece();
            }

            pieceMovementManager.SpawnPiece(pieceRandomizer.Piece);
            CurrentPiece = pieceRandomizer.Piece;
            pieceRandomizer.RandomizePiece();
        }

        private void ClearGrid()
        {
            for (int row = 0; row < solidPiecesGrid.GridSystem.RowsCount; row++)
            {
                for (int column = 0; column < solidPiecesGrid.GridSystem.ColumnsCount; column++)
                {
                    solidPiecesGrid.GridSystem.SetCellState(row, column, false);
                }
            }
            solidPiecesGrid.UpdateGizmosWithSolidCells();
        }

        //TODO: score mark (line check)
        //TODO: game end check
    }
}
