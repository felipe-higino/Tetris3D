using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.TetrisModel;

namespace Systems.TetrisGame
{
    public class RandomizeTetrisPiece : MonoBehaviour
    {
        public event Action OnRandomizedPieceChanged;

        [SerializeField]
        private List<SO_TetrisPiece> piecesList;

        public SO_TetrisPiece CurrentPiece { get; private set; }
        public SO_TetrisPiece NextPiece { get; private set; }

        private void Awake()
        {
            CurrentPiece = GetRandomPiece();

        }
        public void RandomizeNextPiece()
        {
            if (null == NextPiece)
            {
                NextPiece = GetRandomPiece();
            }

            CurrentPiece = NextPiece;
            NextPiece = GetRandomPiece();

            OnRandomizedPieceChanged?.Invoke();
        }

        private SO_TetrisPiece GetRandomPiece()
        {
            if (piecesList.Count == 0)
                return null;

            var randomIndex = UnityEngine.Random.Range(0, piecesList.Count);
            var randomPiece = piecesList[randomIndex];

            return randomPiece;
        }
    }
}
