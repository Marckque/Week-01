using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltGenerator : MonoBehaviour
{
    public GameObject salt;
    public int saltNumber;

    protected void Start()
    {
        StartCoroutine(SaltCreation());
    }

    private IEnumerator SaltCreation()
    {
        for (int i = 0; i < saltNumber; i++)
        {
            Instantiate(salt, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.01f);
        }
    }
}