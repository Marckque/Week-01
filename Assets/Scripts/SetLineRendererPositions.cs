using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLineRendererPositions : MonoBehaviour
{
    public Transform antenaRoot;
    private Transform[] antenas;
    private LineRenderer lineRenderer;

    protected void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        antenas = new Transform[antenaRoot.childCount];

        for (int i = 0; i < antenaRoot.childCount; ++i)
        {
            antenas[i] = antenaRoot.GetChild(i);
        }

        lineRenderer.positionCount = antenas.Length;
    }

    protected void Update()
    {
        UpdateLinePositions();
    }

    private void UpdateLinePositions()
    {
        for (int i = 0; i < antenas.Length ; ++i)
        {
            lineRenderer.SetPosition(i, antenas[i].transform.position);
        }
    }
}