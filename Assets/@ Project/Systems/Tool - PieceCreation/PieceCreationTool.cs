#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.TetrisModel;
using Systems.GridSystem;
using Systems.TetrisGame;

namespace CreationTools
{
    public class PieceCreationTool : MonoBehaviour
    {
        [SerializeField]
        private SO_TetrisPiece tetrisPiece;

        [SerializeField]
        private bool changeInGrid = false;

        [SerializeField]
        private Degrees degreeToCopy;
        [SerializeField]
        private SceneGrid sceneGrid;

        public Vector2Int[] positions0degree;
        public Vector2Int[] positions90degree;
        public Vector2Int[] positions180degree;
        public Vector2Int[] positions270degree;

        private void OnValidate()
        {
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            if (changeInGrid)
            {
                switch (degreeToCopy)
                {
                    case Degrees._0:
                        sceneGrid.indexesWithColor2 = positions0degree;
                        break;
                    case Degrees._90:
                        sceneGrid.indexesWithColor2 = positions90degree;
                        break;
                    case Degrees._180:
                        sceneGrid.indexesWithColor2 = positions180degree;
                        break;
                    case Degrees._270:
                        sceneGrid.indexesWithColor2 = positions270degree;
                        break;
                    default:
                        break;
                }
            }
        }
    }

}
#endif