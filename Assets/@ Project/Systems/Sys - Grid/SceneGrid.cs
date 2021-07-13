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

        public GridSystem GridSystem { get; private set; }
        private Vector2[][] CenterPositions;

        private void Awake()
        {
            GridSystem = new GridSystem(rowsCount, columnsCount);
        }

        private Vector3[][] GetCenterPositions()
        {
            var verticalDirection = origin.up;
            var horizontalDirection = origin.right;

            var positions = new Vector3[rowsCount][];

            var originPosition = centerSnapOrigin.position;
            for (int rowIndex = 0; rowIndex < rowsCount; rowIndex++)
            {
                positions[rowIndex] = new Vector3[columnsCount];
                for (int columnIndex = 0; columnIndex < columnsCount; columnIndex++)
                {
                    positions[rowIndex][columnIndex] = originPosition;
                    originPosition += (horizontalDirection * cellSize);
                }
                originPosition = centerSnapOrigin.position;
                originPosition += (verticalDirection * cellSize * (rowIndex + 1));
            }

            return positions;
        }

#if UNITY_EDITOR

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

        //grid gizmos
        private void OnDrawGizmos()
        {
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


            var rows = GetCenterPositions();
            for (int i = 0; i < rows.Length; i++)
            {
                var column = rows[i];
                for (int j = 0; j < column.Length; j++)
                {
                    var position = column[j];
                    if (indexesWithColor2.Contains(new Vector2Int(i, j)))
                    {
                        Gizmos.color = centerGizmosColor2;
                    }
                    else
                    {
                        Gizmos.color = centerGizmosColor1;
                    }
                    Gizmos.DrawCube(position, new Vector3(1, 1, 1) * boxGizmoSizeMultiplier);
                }
            }
        }

    }

#endif

}