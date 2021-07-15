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
        public delegate void Del_Solidify(SO_TetrisPiece data, Vector2Int[] positions);

        public event Action OnGameStart;
        public event Action OnGridCompress;
        public event Del_Solidify OnSolidify;
        public event Action<SO_TetrisPiece> OnSpawnPiece;

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
        public PieceMovementManager PieceMovementManager => pieceMovementManager;

        [SerializeField]
        private SceneGrid solidPiecesGrid;
        public SceneGrid SolidPiecesGrid => solidPiecesGrid;

        [SerializeField]
        private A_TetrisInput inputs;

        private GameClock pieceFallClock;

        [ContextMenu("start new game")]
        public void StartNewGame()
        {
            ClearSolidPieces();
            SpawnNewPiece();
            OnGameStart?.Invoke();
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
            var isNotInputingDown = !(inputs.IsDashing || inputs.IsMovingDown);
            if (isNotInputingDown)
                pieceMovementManager.MovePieceDown();
        }

        private void OnSolidifyPiece(Vector2Int[] pieceCells)
        {
            foreach (var cell in pieceCells)
            {
                solidPiecesGrid.GridSystem
                    .SetCellState(cell.y, cell.x, true);
            }

            OnSolidify?.Invoke(CurrentPiece, pieceCells);

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
            solidPiecesGrid.GridSystem.CompressGrid();
            OnGridCompress?.Invoke();

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
            OnSpawnPiece?.Invoke(pieceRandomizer.Piece);

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
