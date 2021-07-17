using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.TetrisGame;
using Systems.Pieces3D;
using System;


namespace Widgets
{
    public class NextPieceDisplayer : MonoBehaviour
    {
        [SerializeField]
        private RandomizeTetrisPiece randomizeTetrisPiece;
        [SerializeField]
        private Movable3DPieceSpawner spawner;

        private void Awake()
        {
            randomizeTetrisPiece.OnRandomizedPieceChanged += OnPieceChanged;
        }

        private void OnDestroy()
        {
            randomizeTetrisPiece.OnRandomizedPieceChanged -= OnPieceChanged;
        }

        private void OnPieceChanged()
        {
            spawner.ActivatePiece(randomizeTetrisPiece.NextPiece);
        }
    }

}
