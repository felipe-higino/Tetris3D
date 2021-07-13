using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.TetrisPiece
{
    public class TetrisPieceComponent : MonoBehaviour
    {
        [SerializeField]
        private SO_TetrisPiece tetrisPieceData;
        public SO_TetrisPiece TetrisPieceData => tetrisPieceData;

    }
}
