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

        public Material GetMaterial()
        {
            return GetComponentInChildren<Material>();
        }

        public void MoveDown()
        {
            if (gameObject.activeSelf)
            {
                transform.localPosition = transform.localPosition + new Vector3(0, -1, 0);
            }
        }

        public void MoveHorizontally(int direction)
        {
            if (gameObject.activeSelf)
            {
                transform.localPosition = transform.localPosition + new Vector3(direction, 0, 0);
            }
        }

        public void Rotate(Degrees degrees, int fixPosition)
        {
            var angle = degrees switch
            {
                Degrees._0 => 0,
                Degrees._90 => 90,
                Degrees._180 => 180,
                Degrees._270 => 270,
                _ => 0
            };

            transform.localRotation = Quaternion.Euler(0, 0, -angle);
            transform.localPosition += new Vector3(fixPosition, 0, 0);
        }

        private void Awake()
        {
            originalPosition = transform.position;
        }

        private void OnDisable()
        {
            transform.localPosition = originalPosition;
            transform.localRotation = Quaternion.identity;
        }

    }

}

