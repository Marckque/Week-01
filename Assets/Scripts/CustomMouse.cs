using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMouse : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 customPosition;
    private float mouseX;
    private float mouseY;

	void Update()
    {
        //mouseX = Input.GetAxis("Mouse X");
        //mouseY = Input.GetAxis("Mouse Y");
        //customPosition.x += Mathf.Abs(mouseX) * speed * Time.deltaTime;
        //customPosition.y += Mathf.Abs(mouseY) * speed * Time.deltaTime;

        //if (Input.mousePosition != customPosition)
        //{
        //    mdr += Time.deltaTime;
        //    customPosition = Vector3.Lerp(customPosition, Input.mousePosition, mdr);

        //    if (mdr <= 1f)
        //    {
        //        mdr = 0f;
        //    }
        //}

        //customPosition = Input.mousePosition;
        //customPosition.x *= ;
        //customPosition.y *= Screen.height;

        transform.position = Camera.main.ScreenToViewportPoint(customPosition);
    }
}