using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScreenResolution : MonoBehaviour
{
	protected void Awake()
    {
        Screen.SetResolution(800, 800, false);

        enabled = false;
        gameObject.SetActive(false);
	}
}