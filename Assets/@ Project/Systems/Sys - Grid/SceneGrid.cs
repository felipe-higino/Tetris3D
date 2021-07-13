using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.GridSystem
{

    public class SceneGrid : MonoBehaviour
    {
        [SerializeField]
        private Vector2Int matrix;

        private GridSystem gridSystem;

        private void Awake()
        {
            gridSystem = new GridSystem(matrix.x, matrix.y);
            gridSystem.SetCellState(0, 0, true);
            Debug.Log(gridSystem.CellsMatrix[0][0].IsFilled);
        }

        // public List<int> GetCompletedRows()
        // {
        //     List<int> completedRows = new List<int>();

        //     for (int rowIndex = 0; rowIndex < cellsMatrix.Length; rowIndex++)
        //     {
        //         var row = cellsMatrix[rowIndex];

        //         var isRowComplete = false;
        //         for (int columnIndex = 0; columnIndex < row.Length; columnIndex++)
        //         {
        //             var cell = row[columnIndex];

        //             if (!cell.IsFilled)
        //                 break;
        //             if (columnIndex == row.Length)
        //                 isRowComplete = true;
        //         }

        //         if (isRowComplete)
        //             completedRows.Add(rowIndex);
        //     }
        //     return completedRows;
        // }
    }

}