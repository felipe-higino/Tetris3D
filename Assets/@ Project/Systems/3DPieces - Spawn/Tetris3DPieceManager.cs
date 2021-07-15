using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.GridSystem;
using Systems.TetrisGame;
using Systems.Tetris.Model;

public class Tetris3DPieceManager : MonoBehaviour
{
    [SerializeField]
    private TetrisGameRules gameRules;

    [SerializeField]
    private PieceMovementManager pieceMovementManager;

    [SerializeField]
    private Transform pivot;

    [SerializeField]
    private SceneGrid filledGrid;

    [SerializeField]
    private List<Solid3DCell> SolidCellsPrefabs;
    // [SerializeField]
    // private List<>
    //cached
    private Vector3[][] _centerPositions = null;
    private Vector3[][] CenterPositions
    {
        get
        {
            if (null == _centerPositions)
                _centerPositions = filledGrid.GetCenterPositions();
            return _centerPositions;
        }
    }

    private Solid3DCell[,] SolidGrid;

    private void Awake()
    {
        gameRules.OnSolidify += OnSolidify;
        var rows = filledGrid.GridSystem.RowsCount;
        var columns = filledGrid.GridSystem.ColumnsCount;

        SolidGrid = new Solid3DCell[rows, columns];
    }

    private void OnDestroy()
    {
        gameRules.OnSolidify -= OnSolidify;
    }

    private void OnSolidify(SO_TetrisPiece tetrisPiece, Vector2Int[] positions)
    {
        var cell = SolidCellsPrefabs.FirstOrDefault(x => x.Data == tetrisPiece);
        if (null == cell)
            return;
        foreach (var position in positions)
        {
            var instance = Instantiate(cell, CenterPositions[position.y][position.x], Quaternion.identity);
            instance.transform.SetParent(pivot);
            instance.gameObject.SetActive(true);

            SolidGrid[position.y, position.x] = instance;
        }

    }

    [ContextMenu("Build")]
    private void Build()
    {
        SolidCellsPrefabs = GetComponentsInChildren<Solid3DCell>(true).ToList();
    }
}
