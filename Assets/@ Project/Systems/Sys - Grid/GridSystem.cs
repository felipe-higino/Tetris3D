using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.GridSystem
{
    [Serializable]
    public struct Cell
    {
        public bool IsFilled { set; get; }
    }

    public class GridSystemComponent
    {
        public int RowsCount { get; }
        public int ColumnsCount { get; }
        private Cell[][] cellsMatrix;

        public GridSystemComponent(GridSystemComponent toCopy)
        {
            this.RowsCount = toCopy.RowsCount;
            this.ColumnsCount = toCopy.ColumnsCount;
            _InitMatrix();

            for (int row = 0; row < toCopy.RowsCount; row++)
            {
                for (int column = 0; column < toCopy.RowsCount; column++)
                {
                    cellsMatrix[row][column].IsFilled = toCopy.cellsMatrix[row][column].IsFilled;
                }
            }
        }

        public GridSystemComponent(int rows, int columns)
        {
            this.RowsCount = rows;
            this.ColumnsCount = columns;
            _InitMatrix();
        }

        public void SetCellState(int row, int column, bool isFilled)
        {
            try
            {
                cellsMatrix[row][column].IsFilled = isFilled;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        public void GetCellState(int row, int column,
            out bool isFilled, out bool outOfBounds)
        {
            var fillCheck = false;
            var outOfBoundsCheck = false;

            try
            {
                fillCheck = cellsMatrix[row][column].IsFilled;
            }
            catch (IndexOutOfRangeException e)
            {
                // Debug.Log(e.Message);
                outOfBoundsCheck = true;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }

            isFilled = fillCheck;
            outOfBounds = outOfBoundsCheck;

        }

        public List<int> GetCompletedRows()
        {
            var completedRows = new List<int>();

            for (int rowIndex = 0; rowIndex < RowsCount; rowIndex++)
            {
                var isRowComplete = false;
                for (int columnIndex = 0; columnIndex < ColumnsCount; columnIndex++)
                {
                    var cell = cellsMatrix[rowIndex][columnIndex];

                    if (!cell.IsFilled)
                        break;
                    if (columnIndex == ColumnsCount - 1)
                        isRowComplete = true;
                }

                if (isRowComplete)
                    completedRows.Add(rowIndex);
            }

            return completedRows;
        }

        public void ClearGrid()
        {
            for (int row = 0; row < RowsCount; row++)
            {
                for (int column = 0; column < ColumnsCount; column++)
                {
                    SetCellState(row, column, false);
                }
            }
        }

        private void _InitMatrix()
        {
            cellsMatrix = new Cell[RowsCount][];
            for (int row = 0; row < RowsCount; row++)
            {
                cellsMatrix[row] = new Cell[ColumnsCount];
            }
        }

    }

}
