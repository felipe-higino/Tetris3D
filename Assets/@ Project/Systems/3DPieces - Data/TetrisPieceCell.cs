using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.Pieces3D.Data
{
    public class TetrisPieceCell : MonoBehaviour
    {
        /// <summary>
        /// Original coordinate as 0 degrees piece. Not used yet.
        /// </summary>
        [SerializeField]
        private Vector2Int relativeCoordinate;

#if UNITY_EDITOR
        [SerializeField]
        private bool drawCoordinates;
        [SerializeField]
        private GUIStyle style;


        private void OnDrawGizmos()
        {
            if (drawCoordinates)
            {
                UnityEditor.Handles.Label(transform.position, $"{relativeCoordinate}", style);
            }
        }
#endif

    }

}