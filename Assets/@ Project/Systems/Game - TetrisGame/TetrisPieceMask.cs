using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.GridSystem;
using Systems.TetrisPiece;

namespace Systems.TetrisGame
{
    public class TetrisPieceMask : MonoBehaviour
    {
        [SerializeField]
        private SceneGrid filledGrid;


        //----------------------------------- VERTICAL MOVEMENT
        public void MoveMaskDown(Vector2Int[] currentCells,
            out Vector2Int deslocation, out bool didMovementEnded)
        {
            var maskCopy = new List<Vector2Int>(currentCells);

            var collidedWithFloor = false;
            var collidedwithAnotherPiece = false;

            for (int i = 0; i < maskCopy.Count; i++)
            {
                maskCopy[i] =
                    new Vector2Int(maskCopy[i].x, maskCopy[i].y - 1);

                filledGrid.GridSystem.GetCellState(
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
                deslocation = new Vector2Int(0, 0);
                didMovementEnded = true;
            }
            else
            {
                deslocation = new Vector2Int(0, -1);
                didMovementEnded = false;
            }
        }


        //----------------------------------- HORIZONTAL MOVEMENT
        public enum WallCollision { LEFT, RIGHT, NONE }
        public void MoveMaskHorizontally(
            Vector2Int[] originalCells, bool isRight,
            out WallCollision wallCollision, out Vector2Int deslocation)
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

                filledGrid.GridSystem.GetCellState(
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
                deslocation = new Vector2Int(0, 0);
            }
            else
            {
                if (isRight)
                    deslocation = new Vector2Int(+1, 0);
                else
                    deslocation = new Vector2Int(-1, 0);
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
            out Vector2Int[] newCells, out Vector2Int newPivot)
        {
            //TODO: RELATIVE DESLOCATION
            //Switch statement C#8
            var relativePosition = degree switch
            {
                Degrees._0 => tetrisPiece.Positions0degree,
                Degrees._90 => tetrisPiece.Positions90degree,
                Degrees._180 => tetrisPiece.Positions180degree,
                Degrees._270 => tetrisPiece.Positions270degree,
                _ => originalCells
            };

            var checkCollidedWithRightWall = false;
            var checkCollidedWithLeftWall = false;
            var checkCollidedWithFloor = false;
            var checkCollidedWithAnotherPiece = false;
            var lastCellBeforeRightWall = filledGrid.GridSystem.ColumnsCount - 1;
            var indexExceedingLeftWall = 0;
            var indexExceedingRightWall = 0;
            for (int i = 0; i < relativePosition.Length; i++)
            {
                filledGrid.GridSystem.GetCellState(
                        relativePosition[i].y, relativePosition[i].x,
                        out var isFilled, out var outOfBounds);

                if (isFilled)
                {
                    checkCollidedWithAnotherPiece = true;
                }

                if (outOfBounds)
                {
                    if (relativePosition[i].y < 0)
                    {
                        checkCollidedWithFloor = true;
                    }

                    if (relativePosition[i].x < 0)
                    {
                        checkCollidedWithLeftWall = true;
                        var index = relativePosition[i].x;
                        if (index < indexExceedingLeftWall)
                            indexExceedingLeftWall = index;
                    }
                    else if (relativePosition[i].x > lastCellBeforeRightWall)
                    {
                        checkCollidedWithRightWall = true;
                        var index = relativePosition[i].x;
                        if (index > indexExceedingRightWall)
                            indexExceedingRightWall = index;
                    }

                }
            }

            if (checkCollidedWithLeftWall)
            {
                //deslocate right
                for (int i = 0; i < relativePosition.Length; i++)
                {
                    relativePosition[i] = new Vector2Int(
                        relativePosition[i].x + indexExceedingLeftWall,
                        relativePosition[i].y);
                }
                newCells = relativePosition;

                newPivot = new Vector2Int(
                        originalPivot.x + indexExceedingLeftWall,
                        originalPivot.y);
            }
            else if (checkCollidedWithRightWall)
            {
                //deslocate left
                for (int i = 0; i < relativePosition.Length; i++)
                {
                    relativePosition[i] = new Vector2Int(
                        relativePosition[i].x + indexExceedingRightWall,
                        relativePosition[i].y);
                }
                newCells = relativePosition;
                newPivot = new Vector2Int(
                        originalPivot.x + indexExceedingRightWall,
                        originalPivot.y);
            }
            else if (checkCollidedWithAnotherPiece || checkCollidedWithFloor)
            {
                //maintain old cells, disallow rotation
                newCells = originalCells;
                newPivot = originalPivot;
            }
            else
            {
                //no ocurrences, allow rotation without operations
                newCells = relativePosition;
                newPivot = originalPivot;
            }

        }
    }
}