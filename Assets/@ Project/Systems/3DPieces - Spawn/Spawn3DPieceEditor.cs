#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using Systems.Tetris.Model;

namespace Systems.Pieces3D.Spawn
{
    [CustomEditor(typeof(Spawn3DPiece))]
    public class Spawn3DPieceEditor : Editor
    {
        public Object pieceToSpawn;

        public override void OnInspectorGUI()
        {
            if (Application.isPlaying)
            {
                var obj = (Spawn3DPiece)target;

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