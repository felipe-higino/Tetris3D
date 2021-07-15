using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.TetrisGame;
using Systems.Tetris.Model;

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

        private Solid3DCell[,] SolidCellsMatrix = new Solid3DCell[0, 0];
        private Movable3DPiece current3DPiece;

        private void Start()
        {
            gameRules.OnSolidify += OnSolidify;
            gameRules.OnGameStart += OnGameStart;
            gameRules.OnSpawnPiece += OnSpawnPiece;

            gameRules.PieceMovementManager.OnPieceMoveDown += OnPieceMoveDown;
        }

        private void OnDestroy()
        {
            gameRules.OnSolidify -= OnSolidify;
            gameRules.OnGameStart -= OnGameStart;
            gameRules.OnSpawnPiece -= OnSpawnPiece;

            gameRules.PieceMovementManager.OnPieceMoveDown -= OnPieceMoveDown;
        }

        private void OnGameStart()
        {
            Debug.Log("start");
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
                instance.transform.SetPositionAndRotation(CenterPositions[position.y][position.x], Quaternion.identity);
                instance.gameObject.SetActive(true);
                SolidCellsMatrix[position.y, position.x] = instance;
            }

        }

        private void OnSpawnPiece(SO_TetrisPiece spawned)
        {
            current3DPiece?.Destruct();
            current3DPiece = movable3DPieceSpawner.SpawnPiece(spawned);
        }

        private void OnPieceMoveDown()
        {

            current3DPiece?.MoveDown();

        }
    }

}