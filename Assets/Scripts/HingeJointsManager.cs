using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HingeJointsManager : MonoBehaviour
{
    private HingeJoint[] hingeJoints;
    private Rigidbody[] rigidbodies;

    protected void Start()
    {
        HingeJointsArrayInitialisation();
        HingeJointsRigidbodyArrayInitialisation();
        HingeJointsRigidbodyConfiguration();
    }

    private void HingeJointsArrayInitialisation()
    {
        hingeJoints = new HingeJoint[transform.childCount];
        for (int i = 0; i < hingeJoints.Length; i++)
        {
            hingeJoints[i] = transform.GetChild(i).GetComponent<HingeJoint>();
        }
    }

    private void HingeJointsRigidbodyArrayInitialisation()
    {
        rigidbodies = new Rigidbody[transform.childCount];
        for (int i = 0; i < hingeJoints.Length; i++)
        {
            rigidbodies[i] = transform.GetChild(i).GetComponent<Rigidbody>();
        }
    }

    private void HingeJointsRigidbodyConfiguration()
    {
        hingeJoints[0].connectedBody = GetComponent<Rigidbody>();

        for (int i = 1; i < hingeJoints.Length; i++)
        {
            hingeJoints[i].connectedBody = hingeJoints[i - 1].GetComponent<Rigidbody>();
        }
    }
}