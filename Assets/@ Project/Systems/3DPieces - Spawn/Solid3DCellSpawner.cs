using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.TetrisModel;

namespace Systems.Pieces3D
{
    public class Solid3DCellSpawner : MonoBehaviour
    {
        [SerializeField]
        private List<Solid3DCell> SolidCellsPrefabs;
        [SerializeField]
        private Transform pivot;

        [ContextMenu("Build")]
        private void Build()
        {
            SolidCellsPrefabs = GetComponentsInChildren<Solid3DCell>(true).ToList();
        }

        public Solid3DCell InstantiateSolidCell(SO_TetrisPiece tetrisPiece)
        {
            if (!gameObject.activeSelf)
            {
                return null;
            }
            var cell = SolidCellsPrefabs.FirstOrDefault(x => x.Data == tetrisPiece);
            if (null == cell)
                return null;

            var instance = Instantiate(cell, pivot);

            return instance;
        }
    }
}