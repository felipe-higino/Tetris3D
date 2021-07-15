using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.GridSystem;

namespace Libs
{
    public static class CompressGridSystemLinesExtension
    {
        public static void CompressGrid(this GridSystemComponent gridToCompress)
        {
            Conditional cellIsFilledConditional = (x, y) =>
            {
                gridToCompress.GetCellState(x, y, out var isFilled, out var _);
                return isFilled;
            };

            Action<LineMove> moveCallback = (x) =>
            {
                foreach (var column in x.filledIndexesCoordinateX)
                {
                    //replaces pieces
                    gridToCompress.SetCellState(x.nextFilledRowIndex, column, false);
                    gridToCompress.SetCellState(x.emptyRowIndex, column, true);
                }
            };

            gridToCompress
                .GetMatrixSnap()
                .CompressMatrix(cellIsFilledConditional, moveCallback);

            // var rowsCount = gridToCompress.RowsCount;
            // var columnsCount = gridToCompress.ColumnsCount;
            // // loop through all rows searching for an empty one
            // for (int row = 0; row < rowsCount; row++)
            // {
            //     gridToCompress.CheckIfRowIsEmpty(row, columnsCount, out var thisRowIsEmpty);

            //     if (!thisRowIsEmpty)
            //     {
            //         continue;
            //     }

            //     //when finding an empty row, get the next one that is not empty
            //     gridToCompress.GetNextFilledRow(row, out var didFound, out var nextFilledRow, out var filledIndexes);

            //     if (didFound)
            //     {
            //         foreach (var column in filledIndexes)
            //         {
            //             //replaces pieces
            //             gridToCompress.SetCellState(nextFilledRow, column, false);
            //             gridToCompress.SetCellState(row, column, true);
            //         }
            //     }

            // }
        }

        // #region Helpers --------------------------------------------------------------------------

        // private static void CheckIfRowIsEmpty(this GridSystemComponent gridToCompress,
        //     int row, int columnsCount, out bool isEmpty)
        // {
        //     var _checkThisRowIsEmpty = true;

        //     for (int x = 0; x < columnsCount; x++)
        //     {
        //         gridToCompress.GetCellState(row, x, out var isFilled, out var _);
        //         if (isFilled)
        //         {
        //             _checkThisRowIsEmpty = false;
        //             break;
        //         }
        //     }

        //     isEmpty = _checkThisRowIsEmpty;
        // }

        // private static void GetNextFilledRow(this GridSystemComponent gridToCompress,
        //     int startRow, out bool didFound, out int nextFilledRow, out List<int> filledIndexes)
        // {
        //     var _didFound = false;
        //     var _nextFilledRow = -1;
        //     var _filledIndexes_y = new List<int>();

        //     for (int row = startRow + 1; row < gridToCompress.RowsCount; row++)
        //     {
        //         for (int column = 0; column < gridToCompress.ColumnsCount; column++)
        //         {
        //             gridToCompress.GetCellState(row, column, out var isFilled, out var _);
        //             if (isFilled)
        //             {
        //                 _didFound = true;
        //                 _filledIndexes_y.Add(column);//gets all filled cells
        //             }
        //         }

        //         if (_didFound)
        //         {
        //             _nextFilledRow = row;
        //             break;
        //         }
        //     }

        //     didFound = _didFound;
        //     nextFilledRow = _nextFilledRow;
        //     filledIndexes = _filledIndexes_y;
        // }

        // #endregion Helpers --------------------------------------------------------------------------
    }

}
