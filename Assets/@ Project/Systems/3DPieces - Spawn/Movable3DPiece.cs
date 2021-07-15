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

        private Vector3 originalPosition;
        private Quaternion originalRotation;
        private Vector3 originalScale;

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

        private void Awake()
        {
            originalPosition = transform.position;
            originalRotation = transform.rotation;
            originalScale = transform.localScale;
        }

        private void OnDisable()
        {
            transform.SetPositionAndRotation(originalPosition, originalRotation);
            transform.localScale = originalScale;
        }

    }

}

