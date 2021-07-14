using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.TetrisPiece;
using Systems.GridSystem;

namespace Systems.TetrisGame
{
    public class TetrisGameManualTests : MonoBehaviour
    {
        [SerializeField]
        private TetrisGameGridComponent tetrisGameGridComponent;

        [SerializeField]
        private SO_TetrisPiece tetrisPiece;

        [SerializeField]
        private SceneGrid sceneGrid;

        [ContextMenu("spawn piece")]
        private void SpawnPiece()
        {
            tetrisGameGridComponent.SpawnTetrisPiece(tetrisPiece);

            sceneGrid.indexesWithColor2 =
                tetrisGameGridComponent.PieceCells.ToArray();
        }
    }
}
