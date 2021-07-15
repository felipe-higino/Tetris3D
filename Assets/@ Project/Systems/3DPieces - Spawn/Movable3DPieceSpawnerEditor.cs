#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using Systems.Tetris.Model;

namespace Systems.Pieces3D
{
    [CustomEditor(typeof(Movable3DPieceSpawner))]
    public class Movable3DPieceSpawnerEditor : Editor
    {
        public Object pieceToSpawn;

        public override void OnInspectorGUI()
        {
            if (Application.isPlaying)
            {
                var obj = (Movable3DPieceSpawner)target;

                pieceToSpawn = EditorGUILayout.ObjectField(pieceToSpawn, typeof(SO_TetrisPiece), true);
                if (GUILayout.Button("Spawn"))
                {
                    obj.SpawnPiece(pieceToSpawn as SO_TetrisPiece);
                }
            }
            base.OnInspectorGUI();
        }
    }
}
#endif