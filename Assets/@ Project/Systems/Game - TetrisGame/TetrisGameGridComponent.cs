using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.GridSystem;
using Systems.TetrisPiece;

namespace Systems.TetrisGame
{
    public class TetrisGameGridComponent : MonoBehaviour
    {
        public event Action OnClockTick;

        [SerializeField]
        private SceneGrid sceneGrid;
        private GridSystemComponent GridSystem => sceneGrid.GridSystem;

        [SerializeField]
        private float clock = 1f;

        public Degrees ActualDegree { get; private set; }
        public SO_TetrisPiece ActiveTetrisPiece { get; private set; }
        public Vector2Int PivotCell { get; private set; }
        private List<Vector2Int> pieceCells;
        public IEnumerable<Vector2Int> PieceCells => pieceCells;

        public void SpawnTetrisPiece(SO_TetrisPiece tetrisPiece)
        {
            LetPieceFall();

            this.ActiveTetrisPiece = tetrisPiece;

            var bounds = tetrisPiece.PieceFullBox;
            int renderStartPointX = (GridSystem.ColumnsCount / 2) - (bounds.x / 2);
            int renderStartPointY = GridSystem.RowsCount - bounds.y;
            this.PivotCell = new Vector2Int(renderStartPointX, renderStartPointY);

            this.pieceCells = new List<Vector2Int>();
            foreach (var cellPosition in tetrisPiece.positions0degree)
            {
                var position = new Vector2Int(
                    renderStartPointX + cellPosition.x,
                    renderStartPointY + cellPosition.y
                );
                this.pieceCells.Add(position);
            }
        }

        public void RotatePieceClockwise()
        {
            //TODO: render new piece bounds 
        }

        public void MovePieceLeft()
        {
            MoveObject(Direction.Left);
        }

        public void MovePieceRight()
        {
            MoveObject(Direction.Right);
        }

        [ContextMenu("Let Piece Fall")]
        public void LetPieceFall()
        {
            //TODO: move down until touches the floor
        }

        private void Awake()
        {
            StartCoroutine(UpdateClock());
        }

        private IEnumerator UpdateClock()
        {
            yield return new WaitForSeconds(clock);

            MoveObject(Direction.Down);
            OnClockTick?.Invoke();
        }


        private enum Direction { Left, Right, Down }
        private void MoveObject(Direction direction)
        {
            MoveMask();
            RenderMovement();
        }

        private void MoveMask()
        {
            //TODO: move piece bounds and check collisions
        }

        private void RenderMovement()
        {
            //TODO: corrects position relative to floor/wall and detect collisions
        }

    }

}
