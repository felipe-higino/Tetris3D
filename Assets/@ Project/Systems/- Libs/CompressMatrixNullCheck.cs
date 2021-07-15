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

    public static class CompressMatrixNullCheckExtension
    {
        public static void CompressMatrix<T>(this T[][] matrix, Action<LineMove> moveCallback) where T : class
        {
            try
            {
                var rowsCount = matrix.Length;
                var columnsCount = matrix[0].Length;

                for (int row = 0; row < rowsCount; row++)
                {
                    matrix.CheckIfRowIsEmpty(row, columnsCount, out var thisRowIsEmpty);

                    if (!thisRowIsEmpty)
                    {
                        continue;
                    }

                    //when finding an empty row, get the next one that is not empty
                    matrix.GetNextFilledRow(row, rowsCount, columnsCount,
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

        private static void CheckIfRowIsEmpty<T>(this T[][] gridToCompress,
            int rowBeingVerified, int columnsCount, out bool isEmpty) where T : class
        {
            var _checkThisRowIsEmpty = true;

            for (int column = 0; column < columnsCount; column++)
            {
                if (null != gridToCompress[rowBeingVerified][column])
                {
                    _checkThisRowIsEmpty = false;
                    break;
                }
            }

            isEmpty = _checkThisRowIsEmpty;
        }

        private static void GetNextFilledRow<T>(this T[][] gridToCompress,
            int startRow, int rowsCount, int columnsCount,
            out bool didFound, out int nextFilledRow, out List<int> filledIndexes)
        {
            var _didFound = false;
            var _nextFilledRow = -1;
            var _filledIndexes_y = new List<int>();

            for (int row = startRow + 1; row < rowsCount; row++)
            {
                for (int column = 0; column < columnsCount; column++)
                {
                    if (null != gridToCompress[row][column])
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