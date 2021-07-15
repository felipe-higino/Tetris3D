using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.GridSystem;
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
        [SerializeField]
        private PieceMovementManager pieceMovementManager;

        [Header("Spawners")]
        [Space(15)]
        [SerializeField]
        private Solid3DCellSpawner solid3DCellSpawner;

        [Header("Grid")]
        [Space(15)]
        [SerializeField]
        private SceneGrid solidGrid;


        //cached
        private Vector3[][] _centerPositions = null;
        private Vector3[][] CenterPositions
        {
            get
            {
                if (null == _centerPositions)
                    _centerPositions = solidGrid.GetCenterPositions();
                return _centerPositions;
            }
        }

        private Solid3DCell[,] SolidGrid;

        private void Start()
        {
            var rows = solidGrid.GridSystem.RowsCount;
            var columns = solidGrid.GridSystem.ColumnsCount;

            SolidGrid = new Solid3DCell[rows, columns];

            gameRules.OnSolidify += OnSolidify;
        }

        private void OnDestroy()
        {
            gameRules.OnSolidify -= OnSolidify;
        }

        private void OnSolidify(SO_TetrisPiece tetrisPiece, Vector2Int[] positions)
        {
            foreach (var position in positions)
            {
                var instance = solid3DCellSpawner.InstantiateSolidCell(tetrisPiece);
                instance.transform.SetPositionAndRotation(CenterPositions[position.y][position.x], Quaternion.identity);
                instance.gameObject.SetActive(true);
                SolidGrid[position.y, position.x] = instance;
            }

        }
    }

}