using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.GridSystem;
using Systems.TetrisPiece;

namespace Systems.TetrisGame
{
    public class PiecesManager : MonoBehaviour
    {
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

        public void MovePieceDown()
        {
            tetrisPieceMask.MoveMaskDown(pieceCells,
                out var deslocation, out var didMovementEnded);

            //deslocating pivot and body
            for (int i = 0; i < pieceCells.Length; i++)
            {
                pieceCells[i] = new Vector2Int(
                    pieceCells[i].x + deslocation.x,
                    pieceCells[i].y + deslocation.y);
            }
            piecePivot = new Vector2Int(
                    piecePivot.x + deslocation.x,
                    piecePivot.y + deslocation.y);

            if (didMovementEnded)
            {
                OnPieceRequireSolidify?.Invoke(pieceCells);
            }

            //gizmos
            UpdateGridGizmos();
        }

        public void MovePieceHorizontally(bool isRight)
        {
            tetrisPieceMask.MoveMaskHorizontally(pieceCells, isRight,
                out var _, out var deslocation);

            //deslocating pivot and body
            for (int i = 0; i < pieceCells.Length; i++)
            {
                pieceCells[i] = new Vector2Int(
                    pieceCells[i].x + deslocation.x,
                    pieceCells[i].y + deslocation.y);
            }
            piecePivot = new Vector2Int(
                    piecePivot.x + deslocation.x,
                    piecePivot.y + deslocation.y);

            UpdateGridGizmos();
        }

        public void RotatePieceClockwise(SO_TetrisPiece tetrisPiece)
        {
            var requiredRotation = (Degrees)GetNextRotationIndex();
            tetrisPieceMask.RotateMask(pieceCells, piecePivot, tetrisPiece,
                requiredRotation, out var newCells, out var newPivot,
                out var didChangeRotation);

            pieceCells = newCells;
            piecePivot = newPivot;
            if (didChangeRotation)
                rotationMemory = requiredRotation;

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