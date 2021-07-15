using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.Tetris.Model;

namespace Systems.Pieces3D
{
    public class Movable3DPieceSpawner : MonoBehaviour
    {
        [SerializeField]
        private Transform container;
        [SerializeField]
        private Movable3DPiece[] cachedPieces;

        public Movable3DPiece SpawnPiece(SO_TetrisPiece pieceToSpawn)
        {
            try
            {
                var original = cachedPieces
                    .FirstOrDefault(x => x.TetrisPieceData == pieceToSpawn);
                var instance = Instantiate(original, container, true);
                instance.gameObject.SetActive(true);
                return instance;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return null;
            }
        }

        [ContextMenu("Build")]
        private void Build()
        {
            cachedPieces = GetComponentsInChildren<Movable3DPiece>(true);
        }
    }
}
