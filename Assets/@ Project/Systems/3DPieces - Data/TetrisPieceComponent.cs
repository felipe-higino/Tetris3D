using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.Tetris.Model;


namespace Systems.Pieces3D.Data
{
    public class TetrisPieceComponent : MonoBehaviour
    {
        [SerializeField]
        private SO_TetrisPiece tetrisPieceData;
        public SO_TetrisPiece TetrisPieceData => tetrisPieceData;

        public Material GetMaterial()
        {
            return GetComponentInChildren<Material>();
        }

    }
}
