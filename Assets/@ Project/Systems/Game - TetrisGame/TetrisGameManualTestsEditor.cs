#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Systems.TetrisGame
{
    [CustomEditor(typeof(TetrisGameManualTests))]
    public class TetrisGameManualTestsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var obj = (TetrisGameManualTests)target;

            if (Application.isPlaying)
            {
                GUILayout.Space(15);
                if (GUILayout.Button("Start game"))
                {
                    obj.StartNewGame();
                }

                GUILayout.Space(15);

                if (GUILayout.Button("spawn piece"))
                {
                    obj.SpawnPiece();
                }
                if (GUILayout.Button("spawn mask"))
                {
                    obj.SpawnMask();
                }

                GUILayout.Space(5);

                if (GUILayout.Button("rotate 90 degreed clockwise"))
                {
                    obj.RotateMask();
                }
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("move left"))
                {
                    obj.MoveMaskHorizontally(false);
                }
                if (GUILayout.Button("move right"))
                {
                    obj.MoveMaskHorizontally(true);
                }
                GUILayout.EndHorizontal();

                if (GUILayout.Button("move down"))
                {
                    obj.MoveMaskDown();
                }

                GUILayout.Space(15);
            }
            base.OnInspectorGUI();
        }
    }
}

#endif