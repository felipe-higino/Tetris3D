using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.TetrisGame;
using Systems.Tetris.Model;
using Systems.GridSystem;

namespace Systems.Pieces3D
{

    public class Tetris3DPieceManager : MonoBehaviour
    {
        [Header("References")]
        [Space(15)]
        [SerializeField]
        private TetrisGameRules gameRules;

        [Header("Spawners")]
        [Space(15)]
        [SerializeField]
        private Solid3DCellSpawner solid3DCellSpawner;
        [SerializeField]
        private Movable3DPieceSpawner movable3DPieceSpawner;

        private Solid3DCell[,] SolidCellsMatrix;


        //cached
        private Vector3[][] _centerPositions = null;
        private Vector3[][] CenterPositions
        {
            get
            {
                if (null == _centerPositions)
                {
                    _centerPositions =
                        gameRules.SolidPiecesGrid.GetCenterPositions();
                }
                return _centerPositions;
            }
        }


        private void Start()
        {
            gameRules.OnSolidify += OnSolidify;
            gameRules.OnGameStart += OnGameStart;
            gameRules.OnSpawnPiece += OnSpawnPiece;
            gameRules.OnGridCompress += OnGridCompress;

            gameRules.PieceMovementManager.OnPieceRotate += OnPieceRotate;
            gameRules.PieceMovementManager.OnPieceMoveDown += OnPieceMoveDown;
            gameRules.PieceMovementManager.OnPieceMoveHorizontally += OnPieceMoveHorizontally;
        }

        private void OnGridCompress()
        {
            throw new NotImplementedException();
        }

        private void OnDestroy()
        {
            gameRules.OnSolidify -= OnSolidify;
            gameRules.OnGameStart -= OnGameStart;
            gameRules.OnSpawnPiece -= OnSpawnPiece;

            gameRules.PieceMovementManager.OnPieceRotate -= OnPieceRotate;
            gameRules.PieceMovementManager.OnPieceMoveDown -= OnPieceMoveDown;
            gameRules.PieceMovementManager.OnPieceMoveHorizontally -= OnPieceMoveHorizontally;
        }

        private void OnGameStart()
        {
            foreach (var item in SolidCellsMatrix)
            {
                item.Destruct();
            }
            var rows = gameRules.SolidPiecesGrid.GridSystem.RowsCount;
            var columns = gameRules.SolidPiecesGrid.GridSystem.ColumnsCount;
            SolidCellsMatrix = new Solid3DCell[rows, columns];
        }

        private void OnSolidify(SO_TetrisPiece tetrisPiece, Vector2Int[] positions)
        {
            foreach (var position in positions)
            {
                var instance = solid3DCellSpawner.InstantiateSolidCell(tetrisPiece);
                if (null == instance)
                    return;
                instance.transform.SetPositionAndRotation(CenterPositions[position.y][position.x], Quaternion.identity);
                instance.gameObject.SetActive(true);
                SolidCellsMatrix[position.y, position.x] = instance;
            }

        }

        private void OnSpawnPiece(SO_TetrisPiece spawned)
        {
            movable3DPieceSpawner.ActivatePiece(spawned);
        }

        private void OnPieceMoveDown()
        {
            movable3DPieceSpawner.Current3DPiece?.MoveDown();
        }

        private void OnPieceMoveHorizontally(int direction)
        {
            movable3DPieceSpawner.Current3DPiece?.MoveHorizontally(direction);
        }

        private void OnPieceRotate(Degrees degree)
        {
            movable3DPieceSpawner.Current3DPiece?.Rotate(degree);
        }
    }

}