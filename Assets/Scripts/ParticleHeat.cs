using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHeat : MonoBehaviour
{
    public PanController panController;
    public ParticleSystem[] particleSystems;
    public AudioSource[] audioSources;

    protected void Update()
    {
		if (panController.heatingPower <= 0f)
        {
            // VFX
            for (int i = 0; i < particleSystems.Length; i++)
            {
                particleSystems[i].Stop();
            }

            // SFX
            for (int i = 0; i < audioSources.Length; i++)
            {
                audioSources[i].Stop();
            }
        }
        else
        {
            // VFX
            for (int i = 0; i < particleSystems.Length; i++)
            {
                var em = particleSystems[i].emission;
                em.rateOverTime = ExtensionMethods.Remap(panController.heatingPower, 0f, 5f, 0f, 40f);

                if (!particleSystems[i].isPlaying)
                {
                    particleSystems[i].Play();
                }
            }

            // SFX
            for (int i = 0; i < audioSources.Length; i++)
            {
                audioSources[i].volume = ExtensionMethods.Remap(panController.heatingPower, 0f, 5f, 0f, 0.5f);

                if (!audioSources[i].isPlaying)
                {
                    audioSources[i].Play();
                }
            }
        }
	}
}