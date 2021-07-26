using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.GridSystem;
using CreationTools;

namespace Systems.TetrisModel
{
    [Serializable]
    [CreateAssetMenu(fileName = "SO_TetrisPiece", menuName = "Tetris3D/SO_TetrisPiece", order = 0)]
    public class SO_TetrisPiece : ScriptableObject
    {
        [SerializeField]
        private Vector2Int pieceFullBox;

        [SerializeField]
        private Vector2Int[] positions0degree;
        [SerializeField]
        private Vector2Int[] positions90degree;
        [SerializeField]
        private Vector2Int[] positions180degree;
        [SerializeField]
        private Vector2Int[] positions270degree;

        [SerializeField]
        private Material material;


        public Vector2Int[] Positions0degree => positions0degree;
        public Vector2Int[] Positions90degree => positions90degree;
        public Vector2Int[] Positions180degree => positions180degree;
        public Vector2Int[] Positions270degree => positions270degree;


        public Vector2Int PieceFullBox => pieceFullBox;
        public Material Material => material;

        [SerializeField]
        private PieceCreationTool creationTool;

#if UNITY_EDITOR
        [ContextMenu("Copy Data From Creation Tool")]
        public void CopyDataFromCreationTool()
        {
            positions0degree = creationTool.positions0degree;
            positions90degree = creationTool.positions90degree;
            positions180degree = creationTool.positions180degree;
            positions270degree = creationTool.positions270degree;
        }
#endif

    }
}
