using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHeat : MonoBehaviour
{
    public PanController panController;
    public ParticleSystem[] particleSystems;

    protected void Update()
    {
		if (panController.heatingPower <= 0f)
        {
            for (int i = 0; i < particleSystems.Length; i++)
            {
                particleSystems[i].Stop();
            }
        }
        else
        {
            for (int i = 0; i < particleSystems.Length; i++)
            {
                var em = particleSystems[i].emission;
                em.rateOverTime = ExtensionMethods.Remap(panController.heatingPower, 0f, 5f, 0f, 20f);

                if (!particleSystems[i].isPlaying)
                {
                    particleSystems[i].Play();
                }
            }
        }
	}
}