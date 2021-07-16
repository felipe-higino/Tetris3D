using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.GridSystem;
using Systems.TetrisModel;

namespace Systems.TetrisGame
{
    public class PieceMovementManager : MonoBehaviour
    {
        public event Action OnPieceMoveDown;
        public event Action<int> OnPieceMoveHorizontally;
        public event Action<Degrees, int> OnPieceRotate;
        public event Action<Vector2Int[]> OnPieceRequireSolidify;

        [SerializeField]
        private SceneGrid piecesGrid;
        [SerializeField]
        private TetrisPieceMask tetrisPieceMask;

        private Vector2Int[] pieceCells;
        private Vector2Int piecePivot;
        private Degrees rotationMemory;

        private int GetNextRotationIndex()
        {
            var index = (int)rotationMemory + 1;
            if (index > 3)
                index = 0;
            if (index < 0)
                index = 3;

            return index;
        }

        public void SpawnPiece(SO_TetrisPiece tetrisPiece)
        {
            PieceSpawner.SpawnTetrisPiece(tetrisPiece, piecesGrid,
                out var newPieceCells, out var newPivotCell);

            this.pieceCells = newPieceCells;
            this.piecePivot = newPivotCell;
            this.rotationMemory = Degrees._0;

            UpdateGridGizmos();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns> Did movement ended? </returns>
        public void MovePieceDown()
        {
            tetrisPieceMask.MoveMaskDown(pieceCells,
                out var deslocation, out var didMovementEnded);

            //deslocating pivot and body
            for (int i = 0; i < pieceCells.Length; i++)
            {
                pieceCells[i] = new Vector2Int(
                    pieceCells[i].x,
                    pieceCells[i].y + deslocation);
            }
            piecePivot = new Vector2Int(
                    piecePivot.x,
                    piecePivot.y + deslocation);

            if (didMovementEnded)
            {
                OnPieceRequireSolidify?.Invoke(pieceCells);
            }
            else
            {
                OnPieceMoveDown?.Invoke();
            }

            //gizmos
            UpdateGridGizmos();
        }

        public void MovePieceHorizontally(int direction)
        {
            if (direction == 0)
            {
                return;
            }
            var isRight = true;
            if (direction < 0)
            {
                isRight = false;
            }

            tetrisPieceMask.MoveMaskHorizontally(pieceCells, isRight,
                out var _, out var deslocation);

            //deslocating pivot and body
            for (int i = 0; i < pieceCells.Length; i++)
            {
                pieceCells[i] = new Vector2Int(
                    pieceCells[i].x + deslocation,
                    pieceCells[i].y);
            }
            piecePivot = new Vector2Int(
                    piecePivot.x + deslocation,
                    piecePivot.y);

            if (deslocation != 0)
            {
                OnPieceMoveHorizontally?.Invoke(deslocation);
            }
            UpdateGridGizmos();
        }

        public void RotatePieceClockwise(SO_TetrisPiece tetrisPiece)
        {
            var requiredRotation = (Degrees)GetNextRotationIndex();
            tetrisPieceMask.RotateMask(pieceCells, piecePivot, tetrisPiece,
                requiredRotation, out var newCells, out var newPivot,
                out var didChangeRotation, out var fixPosition);

            pieceCells = newCells;
            piecePivot = newPivot;
            if (didChangeRotation)
            {
                rotationMemory = requiredRotation;
                OnPieceRotate?.Invoke(requiredRotation, fixPosition);
            }

            UpdateGridGizmos();
        }

        private void UpdateGridGizmos()
        {
#if UNITY_EDITOR
            piecesGrid.indexesWithColor2 = pieceCells;
#endif
        }

    }
}