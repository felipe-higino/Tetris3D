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
        [SerializeField]
        private GameClock gameClock;
        [SerializeField]
        private RandomizeTetrisPiece pieceRandomizer;
        [SerializeField]
        private PieceMovementManager pieceMovementManager;
        [SerializeField]
        private SceneGrid solidPiecesGrid;
        [SerializeField]
        private A_TetrisInput inputs;

        public SO_TetrisPiece CurrentPiece { get; private set; }

        [ContextMenu("start new game")]
        public void StartNewGame()
        {
            gameClock.StopClock();

            SpawnNewPiece();
            ClearGrid();

            gameClock.StartClock();
        }

        private void Awake()
        {
            gameClock.OnClockTick += Tick;
            pieceMovementManager.OnPieceRequireSolidify += SolidifyPiece;
        }

        private void OnDestroy()
        {
            gameClock.OnClockTick -= Tick;
            pieceMovementManager.OnPieceRequireSolidify -= SolidifyPiece;
        }

        private void Tick()
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
            if (pieceRandomizer.Piece == null)
            {
                pieceRandomizer.RandomizePiece();
            }

            pieceMovementManager.SpawnPiece(pieceRandomizer.Piece);
            CurrentPiece = pieceRandomizer.Piece;
            pieceRandomizer.RandomizePiece();
        }

        //TODO: performance check
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

        //TODO: RNG piece spawn
        //TODO: gravity piece movement through game clock
        //TODO: score mark (line check)
        //TODO: game end check
    }
}
