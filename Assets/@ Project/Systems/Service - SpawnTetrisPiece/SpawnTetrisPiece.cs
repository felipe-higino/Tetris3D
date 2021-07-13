using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.TetrisPiece;

namespace Systems.PieceSpawn
{
    public class SpawnTetrisPiece : MonoBehaviour
    {
        [SerializeField]
        private Transform container;
        [SerializeField]
        private TetrisPieceComponent[] cachedPieces;

        public TetrisPieceComponent SpawnPiece(SO_TetrisPiece pieceToSpawn)
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
            cachedPieces = GetComponentsInChildren<TetrisPieceComponent>(true);
        }
    }
}
