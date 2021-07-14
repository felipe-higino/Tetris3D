using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.GridSystem;
using Systems.TetrisPiece;

namespace Systems.TetrisGame
{
    // public enum GameStates
    // {
    //     STARTED,
    //     PIECE_SOLIDIFIED,
    //     POINT_MARKED,

    // }
    public class TetrisGameRules : MonoBehaviour
    {
        [SerializeField]
        private GameClock gameClock;
        [SerializeField]
        private PiecesManager piecesManager;
        [SerializeField]
        private SceneGrid solidPiecesGrid;

        [SerializeField]
        private SO_TetrisPiece pieceRNG;

        private void Awake()
        {
            gameClock.OnClockTick += Tick;
            piecesManager.OnPieceRequireSolidify += SolidifyPiece;
        }

        private void OnDestroy()
        {
            gameClock.OnClockTick -= Tick;
            piecesManager.OnPieceRequireSolidify -= SolidifyPiece;
        }

        private void Tick()
        {

        }

        private void SolidifyPiece(Vector2Int[] pieceCells)
        {
            foreach (var cell in pieceCells)
            {
                solidPiecesGrid.GridSystem
                    .SetCellState(cell.y, cell.x, true);
            }
            solidPiecesGrid.UpdateGizmosWithSolidCells();

            piecesManager.SpawnPiece(pieceRNG);
        }

        //TODO: game end check
        //TODO: RNG piece spawn
        //TODO: gravity piece movement through game clock
        //TODO: score mark (line check)
    }
}
