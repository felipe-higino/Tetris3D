using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.GridSystem
{
    public class GridSystem
    {
        private int rowsCount;
        private int columnsCount;
        public Cell[][] CellsMatrix { get; private set; }

        public struct Cell
        {
            public bool IsFilled { set; get; }
        }

        public GridSystem(int rows, int columns)
        {
            this.rowsCount = rows;
            this.columnsCount = columns;
            _InitMatrix(rows, columns);
        }

        public void SetCellState(int positionX, int positionY, bool isFilled)
        {
            try
            {
                CellsMatrix[positionX][positionY].IsFilled = isFilled;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        public List<int> GetCompletedRows()
        {
            List<int> completedRows = new List<int>();

            for (int rowIndex = 0; rowIndex < CellsMatrix.Length; rowIndex++)
            {
                var row = CellsMatrix[rowIndex];

                var isRowComplete = false;
                for (int columnIndex = 0; columnIndex < row.Length; columnIndex++)
                {
                    var cell = row[columnIndex];

                    if (!cell.IsFilled)
                        break;
                    if (columnIndex == row.Length)
                        isRowComplete = true;
                }

                if (isRowComplete)
                    completedRows.Add(rowIndex);
            }
            return completedRows;
        }

        private void _InitMatrix(int rows, int columns)
        {
            CellsMatrix = new Cell[rowsCount][];
            for (int row = 0; row < rowsCount; row++)
            {
                CellsMatrix[row] = new Cell[columnsCount];
            }
        }

    }

}
