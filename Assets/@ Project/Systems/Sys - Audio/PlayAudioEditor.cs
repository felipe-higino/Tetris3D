#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Systems.Audio
{
    [CustomEditor(typeof(PlayAudio))]
    public class PlayAudioEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var obj = (PlayAudio)target;

            if (GUILayout.Button("Play"))
            {
                obj.Play();
            }
            if (GUILayout.Button("Pause"))
            {
                obj.Pause();
            }

        }
    }
}
#endif