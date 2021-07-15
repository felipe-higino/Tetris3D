using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.Tetris.Model;

namespace Systems.Pieces3D
{
    public class Movable3DPiece : MonoBehaviour
    {
        [SerializeField]
        private SO_TetrisPiece tetrisPieceData;
        public SO_TetrisPiece TetrisPieceData => tetrisPieceData;

        public Material GetMaterial()
        {
            return GetComponentInChildren<Material>();
        }

        public void MoveDown()
        {
            if (gameObject.activeSelf)
            {
                transform.SetPositionAndRotation(transform.position + new Vector3(0, -1, 0), Quaternion.identity);
            }
        }

        public void Destruct()
        {
            Destroy(gameObject);
        }
    }

}

