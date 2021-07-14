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

        private Vector2Int[] maskCells;
        private Vector2Int piecePivot;
        private Degrees rotationMemory;
        private int RotationLoop
        {
            get => (int)rotationMemory;
            set
            {
                var index = value;
                if (index > 3)
                    index = 0;
                if (index < 0)
                    index = 3;

                rotationMemory = (Degrees)index;
            }
        }

        public void SpawnMask()
        {
            PieceSpawner.SpawnTetrisPiece(tetrisPiece, maskGrid,
                out var pieceCells, out var pivotCell);

            maskGrid.indexesWithColor2 = pieceCells.ToArray();
            maskCells = pieceCells;
            rotationMemory = Degrees._0;
        }

        public void MoveMaskHorizontally(bool isRight)
        {
            tetrisPieceMask.MoveMaskHorizontally(maskCells, isRight,
                out var wallCollision, out var deslocation);

            //deslocating pivot and body
            for (int i = 0; i < maskCells.Length; i++)
            {
                maskCells[i] = new Vector2Int(
                    maskCells[i].x + deslocation.x,
                    maskCells[i].y + deslocation.y);
            }
            piecePivot = new Vector2Int(
                    piecePivot.x + deslocation.x,
                    piecePivot.y + deslocation.y);

            maskGrid.indexesWithColor2 = maskCells.ToArray();
        }

        public void MoveMaskDown()
        {
            tetrisPieceMask.MoveMaskDown(maskCells,
                out var deslocation, out var didMovementEnded);

            //deslocating pivot and body
            for (int i = 0; i < maskCells.Length; i++)
            {
                maskCells[i] = new Vector2Int(
                    maskCells[i].x + deslocation.x,
                    maskCells[i].y + deslocation.y);
            }
            piecePivot = new Vector2Int(
                    piecePivot.x + deslocation.x,
                    piecePivot.y + deslocation.y);
            maskGrid.indexesWithColor2 = maskCells.ToArray();
        }

        public void RotateMask()
        {
            RotationLoop++;
            tetrisPieceMask.RotateMask(maskCells, piecePivot, tetrisPiece,
                rotationMemory, out var newCells, out var newPivot);

            maskCells = newCells;
            piecePivot = newPivot;
            maskGrid.indexesWithColor2 = maskCells;
        }

        #endregion mask tests ----------------------------------------------------------------------

    }
}

#endif