using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Systems.Audio
{
    public class PlayAudio : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;

        public void Play()
        {
            audioSource.Play();
        }

        public void Pause()
        {
            audioSource.Pause();
        }
    }

}
