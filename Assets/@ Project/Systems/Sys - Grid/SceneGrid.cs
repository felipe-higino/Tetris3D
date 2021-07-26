using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.GridSystem
{
    public class SceneGrid : MonoBehaviour
    {
        [Header("References")]
        [Space(15)]
        [SerializeField]
        private Transform origin;
        [SerializeField]
        private Transform centerSnapOrigin;

        [Header("Parameters")]
        [Space(15)]
        [SerializeField, Min(1)]
        private int rowsCount = 20;
        [SerializeField, Min(1)]
        private int columnsCount = 10;
        [SerializeField]
        private float cellSize = 1f;

        public GridSystemComponent GridSystem { get; private set; }

        public void UpdateGizmosWithSolidCells()
        {
#if UNITY_EDITOR
            var indexesToHighlight = new List<Vector2Int>();
            for (int row = 0; row < GridSystem.RowsCount; row++)
            {
                for (int column = 0; column < GridSystem.ColumnsCount; column++)
                {
                    this.GridSystem.GetCellState(row, column,
                        out var isFilled, out var _);
                    if (isFilled)
                        indexesToHighlight.Add(new Vector2Int(column, row));
                }
            }
            indexesWithColor2 = indexesToHighlight.ToArray();
#endif
        }

        private void Awake()
        {
            GridSystem = new GridSystemComponent(rowsCount, columnsCount);
        }

        public struct PositionAndRotation
        {

        }

        public Vector3[,] GetCenterPositions()
        {
            var verticalDirection = origin.up;
            var horizontalDirection = origin.right;

            var positions = new Vector3[rowsCount, columnsCount];

            var originPosition = centerSnapOrigin.position;
            for (int rowIndex = 0; rowIndex < rowsCount; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < columnsCount; columnIndex++)
                {
                    positions[rowIndex, columnIndex] = originPosition;
                    originPosition += (horizontalDirection * cellSize);
                }
                originPosition = centerSnapOrigin.position;
                originPosition += (verticalDirection * cellSize * (rowIndex + 1));
            }

            return positions;
        }

        [Header("Gizmos")]
        [Space(15)]
        [SerializeField]
        private Color centerGizmosColor1;
        [SerializeField]
        private Color centerGizmosColor2;

        [SerializeField]
        private Color horizontalLinesGizmosColor;
        [SerializeField]
        private Color verticalLinesGizmosColor;
        [SerializeField]
        private float boxGizmoSizeMultiplier = 0.1f;
        [SerializeField]
        public Vector2Int[] indexesWithColor2;

        [SerializeField]
        private bool drawCoordinates;
        [SerializeField]
        private GUIStyle style;
        [SerializeField]
        private Vector3 worldDeslocation;

        //grid gizmos
        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            var verticalDirection = origin.up;
            var horizontalDirection = origin.right;

            Gizmos.color = horizontalLinesGizmosColor;
            //draw vertical lines
            var verticalLineOriginPosition = origin.position;
            for (int i = 0; i < columnsCount + 1; i++)
            {
                var verticalEndPosition = (origin.up * cellSize * rowsCount) + verticalLineOriginPosition;
                Gizmos.DrawLine(verticalLineOriginPosition, verticalEndPosition);

                verticalLineOriginPosition += (horizontalDirection * cellSize);
            }

            Gizmos.color = verticalLinesGizmosColor;
            //draw horizontal lines
            var horizontalLineOriginPosition = origin.position;
            for (int i = 0; i < rowsCount + 1; i++)
            {
                var horizontalEndPostion = (origin.right * cellSize * columnsCount) + horizontalLineOriginPosition;
                Gizmos.DrawLine(horizontalLineOriginPosition, horizontalEndPostion);

                horizontalLineOriginPosition += (verticalDirection * cellSize);
            }


            var positions = GetCenterPositions();
            for (int i = 0; i < positions.GetLength(0); i++)
            {
                for (int j = 0; j < positions.GetLength(1); j++)
                {
                    var position = positions[i, j];
                    if (indexesWithColor2.Contains(new Vector2Int(j, i)))
                    {
                        Gizmos.color = centerGizmosColor2;
                    }
                    else
                    {
                        Gizmos.color = centerGizmosColor1;
                    }
                    Gizmos.DrawCube(position, new Vector3(1, 1, 1) * boxGizmoSizeMultiplier);

                    if (drawCoordinates)
                    {
                        UnityEditor.Handles.Label(position + worldDeslocation, $"({j},{i})", style);
                    }

                }
            }
#endif
        }
    }



}