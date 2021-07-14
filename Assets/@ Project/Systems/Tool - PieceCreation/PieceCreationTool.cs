#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.TetrisPiece;
using Systems.GridSystem;

namespace CreationTools
{
    public class PieceCreationTool : MonoBehaviour
    {

        [SerializeField]
        private SO_TetrisPiece tetrisPiece;

        [SerializeField]
        private bool changeInGrid = false;

        private enum Degrees { _0, _90, _180, _270 }
        [SerializeField]
        private Degrees degreeToCopy;
        [SerializeField]
        private SceneGrid sceneGrid;

        [SerializeField]
        private Vector2Int[] positions0degree;
        [SerializeField]
        private Vector2Int[] positions90degree;
        [SerializeField]
        private Vector2Int[] positions180degree;
        [SerializeField]
        private Vector2Int[] positions270degree;

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

        [ContextMenu("Update Scriptable Object")]
        private void UpdateSO()
        {
            tetrisPiece.positions0degree = positions0degree;
            tetrisPiece.positions90degree = positions90degree;
            tetrisPiece.positions180degree = positions180degree;
            tetrisPiece.positions270degree = positions270degree;
        }

    }

}
#endif