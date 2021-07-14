using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.GridSystem;

namespace Systems.TetrisPiece
{
    [Serializable]
    [CreateAssetMenu(fileName = "SO_TetrisPiece", menuName = "Tetris3D/SO_TetrisPiece", order = 0)]
    public class SO_TetrisPiece : ScriptableObject
    {
        [SerializeField]
        private Vector2Int pieceFullBox;

        [SerializeField]
        public Vector2Int[] positions0degree;
        [SerializeField]
        public Vector2Int[] positions90degree;
        [SerializeField]
        public Vector2Int[] positions180degree;
        [SerializeField]
        public Vector2Int[] positions270degree;


        public Vector2Int PieceFullBox => pieceFullBox;
    }
}
