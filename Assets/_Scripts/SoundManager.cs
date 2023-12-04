using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource picaxeAudioSource;
    [SerializeField] private AudioClip[] picaxeHitAudioClips;
    [SerializeField] private AudioClip[] mineFractionsAudioClips;

    public void PlayPicaxeHitSound()
    {
        picaxeAudioSource.PlayOneShot(picaxeHitAudioClips[Random.Range(0, picaxeHitAudioClips.Length)]);
    }

    public void PlayMineFractionSound()
    {
        picaxeAudioSource.PlayOneShot(mineFractionsAudioClips[Random.Range(0, mineFractionsAudioClips.Length)]);
    }

}
