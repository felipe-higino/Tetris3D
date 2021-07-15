using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.Tetris.Model;

namespace Systems.Pieces3D
{
    public class Solid3DCell : MonoBehaviour
    {
        [SerializeField]
        private SO_TetrisPiece data;
        public SO_TetrisPiece Data => data;

        [SerializeField]
        private Renderer render;

        private void Awake()
        {
            render.material = data.Material;
        }

        public void Destruct()
        {
            Destroy(gameObject);
        }
    }

}