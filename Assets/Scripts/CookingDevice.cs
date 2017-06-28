using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CookingDevice : MonoBehaviour
{
    [Header("Cooking device")]
    public float heatingPower = 1f;
    protected float targetHeatingPower;

    [Header("Feedback")]
    public ParticleSystem cookingVFX;
    public AudioSource cookingAudioSource;
    public AudioClip cookingSFX;

    protected bool isBeingUsed;

    public void PlayVFX(ParticleSystem value)
    {
        cookingVFX.Play();
    }

    public void PlaySFX(AudioClip value)
    {
        cookingAudioSource.clip = value;
        cookingAudioSource.Play();
    }
}