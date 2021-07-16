using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.TetrisModel;

namespace Systems.Pieces3D
{
    public class Movable3DPieceSpawner : MonoBehaviour
    {
        [SerializeField]
        private Movable3DPiece[] cachedPieces;

        public Movable3DPiece Current3DPiece { get; private set; }

        public void DeactivateMovablePiece()
        {
            Current3DPiece?.gameObject.SetActive(false);
        }

        public void ActivatePiece(SO_TetrisPiece pieceToSpawn)
        {
            try
            {
                var search = cachedPieces
                    .FirstOrDefault(x => x.TetrisPieceData == pieceToSpawn);

                Current3DPiece?.gameObject.SetActive(false);

                Current3DPiece = search;
                Current3DPiece.gameObject.SetActive(true);

            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        [ContextMenu("Build")]
        private void Build()
        {
            cachedPieces = GetComponentsInChildren<Movable3DPiece>(true);
        }
    }
}
