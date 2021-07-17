using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Widgets
{
    public class ChangeBlur : MonoBehaviour
    {
        [SerializeField]
        private Volume volume;

        private DepthOfField depthOfField;

        private void Awake()
        {
            volume.profile.TryGet<DepthOfField>(out depthOfField);
        }

        [ContextMenu("show blur")]
        public void ShowBlur()
        {
            depthOfField.focalLength.value = 220f;
        }

        [ContextMenu("hide blur")]
        public void HideBlur()
        {
            depthOfField.focalLength.value = 0f;
        }

    }
}
