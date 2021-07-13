#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using Systems.TetrisPiece;

namespace Systems.PieceSpawn
{
    [CustomEditor(typeof(SpawnTetrisPiece))]
    public class SpawnTetrisPieceEditor : Editor
    {
        public Object pieceToSpawn;

        public override void OnInspectorGUI()
        {
            if (Application.isPlaying)
            {
                var obj = (SpawnTetrisPiece)target;

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