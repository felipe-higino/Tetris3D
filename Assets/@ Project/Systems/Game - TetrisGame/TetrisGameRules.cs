using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.GridSystem;
using Systems.Tetris.Model;
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
            ClearSolidPieces();
            SpawnNewPiece();
        }

        private void Awake()
        {
            pieceFallClock = gameObject.AddComponent<GameClock>();
            pieceFallClock.clock = pieceFallClockInterval;

            pieceFallClock.OnClockTick += OnPieceFall;
            pieceMovementManager.OnPieceRequireSolidify += OnSolidifyPiece;
        }

        private void OnDestroy()
        {
            pieceFallClock.OnClockTick -= OnPieceFall;
            pieceMovementManager.OnPieceRequireSolidify -= OnSolidifyPiece;
        }

        private void OnPieceFall()
        {
            var isNotMovingDown = !(inputs.IsDashing || inputs.IsMovingDown);
            if (isNotMovingDown)
                pieceMovementManager.MovePieceDown();
        }

        private void OnSolidifyPiece(Vector2Int[] pieceCells)
        {
            foreach (var cell in pieceCells)
            {
                solidPiecesGrid.GridSystem
                    .SetCellState(cell.y, cell.x, true);
            }
            solidPiecesGrid.UpdateGizmosWithSolidCells();

            var filledRows = solidPiecesGrid.GridSystem.GetCompletedRows();
            if (filledRows.Count > 0)
            {
                CutRows(filledRows.ToArray(), SpawnNewPiece);
            }
            else
            {
                SpawnNewPiece();
            }

        }

        private void CutRows(int[] filledRows, Action onCutEndCallback)
        {
            var rowsCount = solidPiecesGrid.GridSystem.RowsCount;
            var columnsCount = solidPiecesGrid.GridSystem.ColumnsCount;

            for (int arrayIndex = 0; arrayIndex < filledRows.Length; arrayIndex++)
            {
                var row = filledRows[arrayIndex];
                for (int column = 0; column < columnsCount; column++)
                {
                    solidPiecesGrid.GridSystem.SetCellState(row, column, false);
                    //TODO: play animations?
                }
            }

            //compress rows algorythm
            for (int row = 0; row < rowsCount; row++)
            {
                var thisRowIsEmpty = true;
                for (int x = 0; x < columnsCount; x++)
                {
                    solidPiecesGrid.GridSystem.GetCellState(row, x,
                        out var isFilled, out var _);
                    if (isFilled)
                    {
                        thisRowIsEmpty = false;
                        break;
                    }
                }

                if (thisRowIsEmpty)
                {
                    var targetRow = row;
                    var nextFilledRow = -1;
                    var filledYIndexes = new List<int>();

                    for (int y = row + 1; y < rowsCount; y++)
                    {
                        var found = false;
                        for (int column = 0; column < columnsCount; column++)
                        {
                            solidPiecesGrid.GridSystem.GetCellState(y, column,
                                out var isFilled, out var _);
                            if (isFilled)
                            {
                                found = true;
                                filledYIndexes.Add(column);
                            }
                        }
                        if (found)
                        {
                            nextFilledRow = y;
                            break;
                        }
                    }

                    if (nextFilledRow > -1)
                    {
                        foreach (var column in filledYIndexes)
                        {
                            solidPiecesGrid.GridSystem
                                .SetCellState(nextFilledRow, column, false);

                            solidPiecesGrid.GridSystem
                                .SetCellState(targetRow, column, true);
                        }
                    }
                }
            }

            solidPiecesGrid.UpdateGizmosWithSolidCells();

            //TODO: delayed animation before invoke?
            onCutEndCallback?.Invoke();

        }

        private void ClearSolidPieces()
        {
            solidPiecesGrid.GridSystem.ClearGrid();
            solidPiecesGrid.UpdateGizmosWithSolidCells();
        }

        private void SpawnNewPiece()
        {
            pieceFallClock.StopClock();

            if (CheckEndGame())
            {
                return;
            }

            pieceFallClock.StartClock();
            if (pieceRandomizer.Piece == null)
            {
                pieceRandomizer.RandomizePiece();
            }

            pieceMovementManager.SpawnPiece(pieceRandomizer.Piece);
            CurrentPiece = pieceRandomizer.Piece;
            pieceRandomizer.RandomizePiece();
        }

        private bool CheckEndGame()
        {
            var rows = solidPiecesGrid.GridSystem.RowsCount;
            var columns = solidPiecesGrid.GridSystem.ColumnsCount;

            var endGame = false;
            for (int column = 0; column < columns; column++)
            {
                solidPiecesGrid.GridSystem.GetCellState(rows - 1, column,
                    out var isFilled, out var _);

                if (isFilled)
                {
                    endGame = true;
                }
            }

            if (endGame)
            {
                EndGame();
            }

            return endGame;
        }

        private void EndGame()
        {
            Debug.Log("GAME FINISHED!");
        }

        //TODO: score mark (line check)
    }
}
