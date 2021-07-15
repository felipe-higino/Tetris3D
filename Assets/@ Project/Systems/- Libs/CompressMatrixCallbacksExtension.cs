using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Libs
{
    public struct LineMove
    {
        public int emptyRowIndex;
        public int nextFilledRowIndex;
        public int[] filledIndexesCoordinateX;
    }

    public delegate bool Conditional(int row, int column);

    public static class CompressMatrixCallbacksExtension
    {
        public static void CompressMatrix<T>(this T[,] matrix,
            Conditional cellIsFilledConditional, Action<LineMove> moveCallback)
        {
            try
            {
                var rowsCount = matrix.GetLength(0);
                var columnsCount = matrix.GetLength(1);

                for (int row = 0; row < rowsCount; row++)
                {
                    matrix.CheckIfRowIsEmpty(row, columnsCount, cellIsFilledConditional,
                        out var thisRowIsEmpty);

                    if (!thisRowIsEmpty)
                    {
                        continue;
                    }

                    //when finding an empty row, get the next one that is not empty
                    matrix.GetNextFilledRow(row, rowsCount, columnsCount, cellIsFilledConditional,
                         out var didFound, out var nextFilledRow, out var filledIndexes);

                    if (didFound)
                    {
                        var lineMove = new LineMove
                        {
                            emptyRowIndex = row,
                            nextFilledRowIndex = nextFilledRow,
                            filledIndexesCoordinateX = filledIndexes.ToArray(),
                        };
                        moveCallback?.Invoke(lineMove);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        private static void CheckIfRowIsEmpty<T>(this T[,] gridToCompress,
            int rowBeingVerified, int columnsCount, Conditional cellIsFilledconditional, out bool isEmpty)
        {
            var _checkThisRowIsEmpty = true;

            for (int column = 0; column < columnsCount; column++)
            {
                var satisfyed = cellIsFilledconditional.Invoke(rowBeingVerified, column);
                if (satisfyed)
                {
                    _checkThisRowIsEmpty = false;
                    break;
                }
            }

            isEmpty = _checkThisRowIsEmpty;
        }

        private static void GetNextFilledRow<T>(this T[,] gridToCompress,
            int startRow, int rowsCount, int columnsCount, Conditional cellIsFilledConditional,
            out bool didFound, out int nextFilledRow, out List<int> filledIndexes)
        {
            var _didFound = false;
            var _nextFilledRow = -1;
            var _filledIndexes_y = new List<int>();

            for (int row = startRow + 1; row < rowsCount; row++)
            {
                for (int column = 0; column < columnsCount; column++)
                {
                    var satisfyed = cellIsFilledConditional.Invoke(row, column);
                    if (satisfyed)
                    {
                        _didFound = true;
                        _filledIndexes_y.Add(column);//gets all filled cells
                    }
                }

                if (_didFound)
                {
                    _nextFilledRow = row;
                    break;
                }
            }

            didFound = _didFound;
            nextFilledRow = _nextFilledRow;
            filledIndexes = _filledIndexes_y;
        }
    }
}