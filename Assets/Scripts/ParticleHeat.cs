using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHeat : MonoBehaviour
{
    public PanController panController;
    public ParticleSystem[] particleSystems;
    public AudioSource[] audioSources;
    public bool soundCoroutineIsWorking;

    protected void Update()
    {
		if (panController.heatingPower <= 0f)
        {
            // VFX
            for (int i = 0; i < particleSystems.Length; i++)
            {
                particleSystems[i].Stop();
            }

            if (!soundCoroutineIsWorking)
            {
                soundCoroutineIsWorking = true;
                StartCoroutine(StopSounds());
            }
        }
        else
        {
            soundCoroutineIsWorking = false;

            // VFX
            for (int i = 0; i < particleSystems.Length; i++)
            {
                var em = particleSystems[i].emission;
                em.rateOverTime = i == 0 ? ExtensionMethods.Remap(panController.heatingPower, 0f, 5f, 0f, 40f) : ExtensionMethods.Remap(panController.heatingPower, 0f, 5f, 0f, 200f);

                if (!particleSystems[i].isPlaying)
                {
                    particleSystems[i].Play();
                }
            }

            // SFX
            for (int i = 0; i < audioSources.Length; i++)
            {
                audioSources[i].volume = ExtensionMethods.Remap(panController.heatingPower, 0f, 5f, 0f, 0.4f);

                if (!audioSources[i].isPlaying)
                {
                    audioSources[i].Play();
                }
            }
        }
	}

    private IEnumerator StopSounds()
    {
        float timeStart = Time.time;
        float startVolume = audioSources[0].volume;
        float endVolume = 0f;
        float percentageComplete = 0f;

        while (percentageComplete < 1f && soundCoroutineIsWorking)
        {
            percentageComplete = (Time.time - timeStart) / 1.5f;
            audioSources[0].volume = Mathf.Lerp(startVolume, endVolume, percentageComplete);
            yield return new WaitForEndOfFrame();
        }

        soundCoroutineIsWorking = false;
    }
}