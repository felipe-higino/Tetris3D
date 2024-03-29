using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.GridSystem;
using Systems.TetrisModel;

namespace Systems.TetrisGame
{
    public class TetrisPieceMask : MonoBehaviour
    {
        [SerializeField]
        private SceneGrid solidPiecesGrid;

        //----------------------------------- VERTICAL MOVEMENT
        public void MoveMaskDown(Vector2Int[] currentCells,
            out int deslocation, out bool didMovementEnded)
        {
            var maskCopy = new List<Vector2Int>(currentCells);

            var collidedWithFloor = false;
            var collidedwithAnotherPiece = false;

            for (int i = 0; i < maskCopy.Count; i++)
            {
                maskCopy[i] =
                    new Vector2Int(maskCopy[i].x, maskCopy[i].y - 1);

                solidPiecesGrid.GridSystem.GetCellState(
                        maskCopy[i].y, maskCopy[i].x,
                        out var isFilled, out var outOfBounds);

                if (isFilled)
                {
                    collidedwithAnotherPiece = true;
                }
                if (outOfBounds)
                {
                    collidedWithFloor = true;
                }
            }

            if (collidedWithFloor || collidedwithAnotherPiece)
            {
                deslocation = 0;
                didMovementEnded = true;
            }
            else
            {
                deslocation = -1;
                didMovementEnded = false;
            }
        }


        //----------------------------------- HORIZONTAL MOVEMENT
        public enum WallCollision { LEFT, RIGHT, NONE }
        public void MoveMaskHorizontally(
            Vector2Int[] originalCells, bool isRight,
            out WallCollision wallCollision, out int deslocation)
        {
            var maskCopy = new List<Vector2Int>(originalCells);

            var collidedWithWall = false;
            var collidedWithAnotherPiece = false;

            for (int i = 0; i < maskCopy.Count; i++)
            {
                var movement = -1;
                if (isRight)
                    movement = +1;

                maskCopy[i] =
                    new Vector2Int(maskCopy[i].x + movement, maskCopy[i].y);

                solidPiecesGrid.GridSystem.GetCellState(
                        maskCopy[i].y, maskCopy[i].x,
                        out var isFilled, out var outOfBounds);

                if (isFilled)
                {
                    collidedWithAnotherPiece = true;
                }
                if (outOfBounds)
                {
                    collidedWithWall = true;
                }
            }

            if (collidedWithWall || collidedWithAnotherPiece)
            {
                deslocation = 0;
            }
            else
            {
                if (isRight)
                    deslocation = +1;
                else
                    deslocation = -1;
            }

            if (collidedWithWall)
            {
                if (isRight)
                    wallCollision = WallCollision.RIGHT;
                else
                    wallCollision = WallCollision.LEFT;
            }
            else
            {
                wallCollision = WallCollision.NONE;
            }
        }


        //----------------------------------- ROTATION MOVEMENT
        public void RotateMask(Vector2Int[] originalCells, Vector2Int originalPivot,
            SO_TetrisPiece tetrisPiece, Degrees degree,
            out Vector2Int[] newCells, out Vector2Int newPivot, out bool didChangeRotation, out int fixPosition)
        {
            //Switch expression C#8
            var cellsLocalPositions = degree switch
            {
                Degrees._0 => tetrisPiece.Positions0degree,
                Degrees._90 => tetrisPiece.Positions90degree,
                Degrees._180 => tetrisPiece.Positions180degree,
                Degrees._270 => tetrisPiece.Positions270degree,
                _ => originalCells
            };

            var cellsInGridList = new List<Vector2Int>();
            for (int i = 0; i < cellsLocalPositions.Length; i++)
            {
                var cell = new Vector2Int(
                    originalPivot.x + cellsLocalPositions[i].x,
                    originalPivot.y + cellsLocalPositions[i].y);
                cellsInGridList.Add(cell);
            }

            var cellsInGrid = cellsInGridList.ToArray();

            var checkCollidedWithRightWall = false;
            var checkCollidedWithLeftWall = false;
            var checkCollidedWithFloor = false;
            var checkCollidedWithAnotherPiece = false;
            var lastCellBeforeRightWall = solidPiecesGrid.GridSystem.ColumnsCount - 1;
            var indexExceedingLeftWall = 0;
            var indexExceedingRightWall = 0;
            for (int i = 0; i < cellsInGrid.Length; i++)
            {
                var cell = cellsInGrid[i];
                solidPiecesGrid.GridSystem.GetCellState(cell.y, cell.x,
                        out var isFilled, out var outOfBounds);

                if (isFilled)
                {
                    checkCollidedWithAnotherPiece = true;
                }

                if (outOfBounds)
                {
                    if (cell.y < 0)
                    {
                        checkCollidedWithFloor = true;
                    }

                    if (cell.x < 0)
                    {
                        checkCollidedWithLeftWall = true;
                        var index = cell.x;
                        if (index < indexExceedingLeftWall)
                            indexExceedingLeftWall = index;
                    }
                    else if (cell.x > lastCellBeforeRightWall)
                    {
                        checkCollidedWithRightWall = true;
                        var index = cell.x;
                        if (index > indexExceedingRightWall)
                            indexExceedingRightWall = index;
                    }

                }
            }

            if (checkCollidedWithLeftWall)
            {
                //deslocate right
                for (int i = 0; i < cellsInGrid.Length; i++)
                {
                    cellsInGrid[i] = new Vector2Int(
                        cellsInGrid[i].x - indexExceedingLeftWall,
                        cellsInGrid[i].y);
                }
                newCells = cellsInGrid;

                newPivot = new Vector2Int(
                        originalPivot.x - indexExceedingLeftWall,
                        originalPivot.y);

                didChangeRotation = true;
                fixPosition = -indexExceedingLeftWall;
            }
            else if (checkCollidedWithRightWall)
            {
                //deslocate left
                var amountToDeslocate = indexExceedingRightWall - lastCellBeforeRightWall;
                for (int i = 0; i < cellsInGrid.Length; i++)
                {
                    cellsInGrid[i] = new Vector2Int(
                        cellsInGrid[i].x - amountToDeslocate,
                        cellsInGrid[i].y);
                }
                newCells = cellsInGrid;
                newPivot = new Vector2Int(
                        originalPivot.x - amountToDeslocate,
                        originalPivot.y);

                didChangeRotation = true;
                fixPosition = -amountToDeslocate;
            }
            else if (checkCollidedWithAnotherPiece || checkCollidedWithFloor)
            {
                //maintain old cells, disallow rotation
                newCells = originalCells;
                newPivot = originalPivot;

                didChangeRotation = false;
                fixPosition = 0;
            }
            else
            {
                //no ocurrences, allow rotation without operations
                newCells = cellsInGrid;
                newPivot = originalPivot;

                didChangeRotation = true;
                fixPosition = 0;
            }

        }
    }
}