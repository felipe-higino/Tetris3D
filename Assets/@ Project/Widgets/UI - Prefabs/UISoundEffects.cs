using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class UISoundEffects : MonoBehaviour
{
    public static UISoundEffects Instance { get; private set; }

    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private AudioClip hover;

    [SerializeField]
    private AudioClip press;

    private void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayHover()
    {
        PlayLonely(hover);
    }

    public void PlayPress()
    {
        PlayLonely(press);
    }

    private void PlayLonely(AudioClip clip)
    {
        source.Stop();
        source.clip = clip;
        source.Play();
    }
}
