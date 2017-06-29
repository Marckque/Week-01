using UnityEngine;
using System.Collections;

public class ChangeLightIntensityOvertime : LightBehaviour
{
    [SerializeField]
    private bool m_PauseBehaviourIfLightIsOff;

    [Header("Intensity overtime"), SerializeField]
    private float[] m_Intensities;
    [SerializeField, Range(0f, 10f)]
    private float m_TransitionDuration;

    private bool m_IsTransitioning;
    private float m_TransitionStart;
    private float m_TimeElapsed;
    private float m_CompletionPercentage;
    private float m_CustomTime;

    private int m_CurrentIntensityIndex = 0;
    private int m_NextIntensityIndex = 1;

    protected override void Start()
    {
        base.Start();
        StartIntensityChangement();
    }

    protected void Update()
    {
        if (m_PauseBehaviourIfLightIsOff)
        {
            if (CurrentLight.enabled == false)
            {
                return;
            }
        }

        if (m_IsTransitioning)
        {
            m_CustomTime += Time.deltaTime;
            m_TimeElapsed = m_CustomTime - m_TransitionStart;
            m_CompletionPercentage = m_TimeElapsed / m_TransitionDuration;

            CurrentLight.intensity = Mathf.Lerp(m_Intensities[m_CurrentIntensityIndex], m_Intensities[m_NextIntensityIndex], m_CompletionPercentage);

            if (m_CompletionPercentage >= 1)
            {
                m_IsTransitioning = false;

                m_TimeElapsed = 0f;
                m_CompletionPercentage = 0f;

                m_CurrentIntensityIndex++;
                m_NextIntensityIndex++;
                CheckIndexes();

                StartIntensityChangement();
            }
        }
    }

    private void StartIntensityChangement()
    {
        m_TransitionStart = m_CustomTime;
        m_IsTransitioning = true;
    }

    private void CheckIndexes()
    {
        if (m_CurrentIntensityIndex > m_Intensities.Length - 1)
        {
            m_CurrentIntensityIndex = 0;
        }

        if (m_NextIntensityIndex > m_Intensities.Length - 1)
        {
            m_NextIntensityIndex = 0;
        }
    }
}