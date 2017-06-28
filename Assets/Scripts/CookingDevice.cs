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
    //public AudioClip sourceStartCookingSFX;

    protected bool isBeingUsed;
}