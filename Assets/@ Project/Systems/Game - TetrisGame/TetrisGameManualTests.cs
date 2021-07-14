#if UNITY_EDITOR

using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.TetrisPiece;
using Systems.GridSystem;

namespace Systems.TetrisGame
{
    public class TetrisGameManualTests : MonoBehaviour
    {
        [Header("Components references")]
        [Space(15)]
        [SerializeField]
        private TetrisPieceMask tetrisPieceMask;

        [Header("Data")]
        [Space(15)]
        [SerializeField]
        private SO_TetrisPiece tetrisPiece;

        [Header("Grids")]
        [Space(15)]
        [SerializeField]
        private SceneGrid filledGrid;
        [SerializeField]
        private SceneGrid movablePieceGrid;
        [SerializeField]
        private SceneGrid maskGrid;


        public void SpawnPiece()
        {
            PieceSpawner.SpawnTetrisPiece(tetrisPiece, movablePieceGrid,
                out var pieceCells, out var pivotCell);

            movablePieceGrid.indexesWithColor2 = pieceCells.ToArray();
        }

        #region mask tests --------------------------------------------------------------

        // private Vector2Int[] maskCells;
        // private Vector2Int piecePivot;
        // private Degrees rotationMemory;

        // private int GetNextRotationIndex()
        // {
        //     var index = (int)rotationMemory + 1;
        //     if (index > 3)
        //         index = 0;
        //     if (index < 0)
        //         index = 3;

        //     return index;
        // }

        [SerializeField]
        private PiecesManager piecesManager;

        public void SpawnMask()
        {
            piecesManager.SpawnPiece(tetrisPiece);
            // PieceSpawner.SpawnTetrisPiece(tetrisPiece, maskGrid,
            //     out var pieceCells, out var pivotCell);

            // maskCells = pieceCells;
            // piecePivot = pivotCell;
            // rotationMemory = Degrees._0;

            // //gizmos
            // maskGrid.indexesWithColor2 = pieceCells;
        }

        public void MoveMaskHorizontally(bool isRight)
        {
            piecesManager.MovePieceHorizontally(isRight);
            // tetrisPieceMask.MoveMaskHorizontally(maskCells, isRight,
            //     out var wallCollision, out var deslocation);

            // //deslocating pivot and body
            // for (int i = 0; i < maskCells.Length; i++)
            // {
            //     maskCells[i] = new Vector2Int(
            //         maskCells[i].x + deslocation.x,
            //         maskCells[i].y + deslocation.y);
            // }
            // piecePivot = new Vector2Int(
            //         piecePivot.x + deslocation.x,
            //         piecePivot.y + deslocation.y);

            // //gizmos
            // maskGrid.indexesWithColor2 = maskCells;
        }

        public void MoveMaskDown()
        {
            piecesManager.MovePieceDown();
            // tetrisPieceMask.MoveMaskDown(maskCells,
            //     out var deslocation, out var didMovementEnded);

            // //deslocating pivot and body
            // for (int i = 0; i < maskCells.Length; i++)
            // {
            //     maskCells[i] = new Vector2Int(
            //         maskCells[i].x + deslocation.x,
            //         maskCells[i].y + deslocation.y);
            // }
            // piecePivot = new Vector2Int(
            //         piecePivot.x + deslocation.x,
            //         piecePivot.y + deslocation.y);

            // //gizmos
            // maskGrid.indexesWithColor2 = maskCells;
        }

        public void RotateMask()
        {
            piecesManager.RotatePieceClockwise(tetrisPiece);
            // var requiredRotation = (Degrees)GetNextRotationIndex();
            // tetrisPieceMask.RotateMask(maskCells, piecePivot, tetrisPiece,
            //     requiredRotation, out var newCells, out var newPivot,
            //     out var didChangeRotation);

            // maskCells = newCells;
            // piecePivot = newPivot;
            // if (didChangeRotation)
            //     rotationMemory = requiredRotation;

            // //gizmos
            // maskGrid.indexesWithColor2 = maskCells;
        }

        #endregion mask tests ----------------------------------------------------------------------

    }
}

#endif