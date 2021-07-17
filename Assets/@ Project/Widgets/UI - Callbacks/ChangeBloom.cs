using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

namespace Widgets
{
    public class ChangeBloom : MonoBehaviour
    {
        [SerializeField]
        private Volume volume;

        [SerializeField]
        private float blinkDuration;
        [SerializeField]
        private float targetBlinkBloom;
        [SerializeField]
        private Ease blinkEase;

        private float originalBloom;

        private Bloom bloom;
        private float Bloom
        {
            get => bloom.intensity.value;
            set => bloom.intensity.value = value;
        }

        private Tween tween;


        private void Awake()
        {
            volume.profile.TryGet<Bloom>(out bloom);
            originalBloom = Bloom;
        }

        [ContextMenu("Blink Bloom")]
        public void BlinkBloom()
        {
            tween.Kill();
            Bloom = originalBloom;
            tween = DOTween.To(() => Bloom, (x) => Bloom = x, targetBlinkBloom, blinkDuration)
                .SetEase(blinkEase)
                .SetLoops(2, LoopType.Yoyo);
        }
    }

}
