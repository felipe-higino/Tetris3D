using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.GridSystem;
using Systems.Tetris.Model;

namespace Systems.TetrisGame
{
    public static class PieceSpawner
    {
        /// <summary>
        /// spawns a piece with 0 degree at grid top
        /// </summary>
        public static void SpawnTetrisPiece(
            SO_TetrisPiece tetrisPiece, SceneGrid targetGrid,
            out Vector2Int[] pieceCells, out Vector2Int pivotCell)
        {
            var bounds = tetrisPiece.PieceFullBox;
            int renderStartPointX =
                (targetGrid.GridSystem.ColumnsCount / 2) - (bounds.x / 2);
            int renderStartPointY =
                targetGrid.GridSystem.RowsCount - bounds.y;
            pivotCell =
                new Vector2Int(renderStartPointX, renderStartPointY);

            var outPieceCells = new List<Vector2Int>();
            foreach (var cellPosition in tetrisPiece.Positions0degree)
            {
                var position = new Vector2Int(
                    renderStartPointX + cellPosition.x,
                    renderStartPointY + cellPosition.y
                );
                outPieceCells.Add(position);
            }
            pieceCells = outPieceCells.ToArray();
        }
    }
}
