using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.GridSystem
{

    public class SceneGrid : MonoBehaviour
    {
        [SerializeField]
        private Vector2Int matrix;

        private GridSystem gridSystem;

        private void Awake()
        {
            gridSystem = new GridSystem(matrix.x, matrix.y);
        }


    }

}