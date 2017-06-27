using UnityEngine;

public class DeactivateGambas : MonoBehaviour
{
    protected void OnTriggerEnter(Collider other)
    {
        Gamba gamba = other.GetComponent<Gamba>();

        if (gamba)
        {
            gamba.enabled = false;
            Destroy(gamba.gameObject, 2f);
        }
    }
}