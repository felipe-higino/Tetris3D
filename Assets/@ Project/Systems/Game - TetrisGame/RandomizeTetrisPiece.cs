using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.Tetris.Model;

namespace Systems.TetrisGame
{
    public class RandomizeTetrisPiece : MonoBehaviour
    {
        public event Action<SO_TetrisPiece> OnRandomizedPieceChanged;

        [SerializeField]
        private List<SO_TetrisPiece> piecesList;

        public SO_TetrisPiece Piece { get; private set; }

        public void RandomizePiece()
        {
            if (piecesList.Count == 0)
                return;

            var randomIndex = UnityEngine.Random.Range(0, piecesList.Count - 1);
            var piece = piecesList[randomIndex];

            Piece = piece;
            OnRandomizedPieceChanged?.Invoke(piece);
        }
    }
}
