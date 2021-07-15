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
        [SerializeField]
        private RandomizeTetrisPiece randomizer;
        [SerializeField]
        private TetrisGameRules gameRules;
        [SerializeField]
        private PieceMovementManager pieceMovementManager;


        [Header("Grids")]
        [Space(15)]
        [SerializeField]
        private SceneGrid filledGrid;
        [SerializeField]
        private SceneGrid movablePieceGrid;
        [SerializeField]
        private SceneGrid maskGrid;

        private SO_TetrisPiece tetrisPiece => gameRules.CurrentPiece;

        private void Start()
        {
            StartNewGame();
        }

        public void SpawnPiece()
        {
            PieceSpawner.SpawnTetrisPiece(tetrisPiece, movablePieceGrid,
                out var pieceCells, out var pivotCell);

            movablePieceGrid.indexesWithColor2 = pieceCells.ToArray();
        }

        public void StartNewGame()
        {
            gameRules.StartNewGame();
        }

        public void SpawnMask()
        {
            pieceMovementManager.SpawnPiece(tetrisPiece);
        }

        public void MoveMaskHorizontally(bool isRight)
        {
            pieceMovementManager.MovePieceHorizontally(isRight ? +1 : -1);
        }

        public void MoveMaskDown()
        {
            pieceMovementManager.MovePieceDown();
        }

        public void RotateMask()
        {
            pieceMovementManager.RotatePieceClockwise(tetrisPiece);
        }
    }
}

#endif