using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PanController : CookingDevice
{
    public bool isBeingUsed;
    public Transform pansRoot;
    private Pan[] pans;

    private Coroutine panHeating;

    private float lastTransition;

    #region Variables
    [Header("Position")]
    public float movementX;
    public bool invertMovementX;
    public float movementY;
    public bool invertMovementY;
    public float movementZ;
    public bool invertMovementZ;

    [Header("Rotation")]
    public float rotationX;
    public bool invertRotationX;
    public float rotationY;
    public bool invertRotationY;
    public float rotationZ;
    public bool invertRotationZ;

    private float mouseX;
    private float mouseY;
    private Vector3 currentMousePosition;

    private Vector3 targetEulerAngle;
    private Vector3 targetPosition;
    private Vector3 mousePositionToScreenCoordinate;
    #endregion Variables

    private void Start()
    {
        SetAllPansParts();
        targetHeatingPower = heatingPower;
    }

    protected void Update()
    {
        isBeingUsed = Input.GetMouseButton(0);

        if (isBeingUsed)
        {
            // Mouse calculations
            MousePositionUpdate();

            // Pan translations
            PanMovement();
            PanRotation();
        }

        float heatingPowerAddition = Input.GetAxisRaw("Mouse ScrollWheel");

        if (heatingPowerAddition != 0f)
        {
            StopAllCoroutines();

            lastTransition = Time.time;

            targetHeatingPower += heatingPowerAddition;
            targetHeatingPower = Mathf.Clamp(targetHeatingPower, 0f, 5f);

            StartCoroutine(UpdateHeat());
        }   
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            heatingPower = 0f;
        }     
    }

    private IEnumerator UpdateHeat()
    {
        float percentageComplete = 0f;
        float startHeatingPower = heatingPower;
        float endHeatingPower = targetHeatingPower;

        while (percentageComplete < 1f)
        {
            percentageComplete = (Time.time - lastTransition) / (2f + (Mathf.Abs(startHeatingPower - endHeatingPower) * 0.5f));

            heatingPower = Mathf.Lerp(startHeatingPower, endHeatingPower, percentageComplete);
            heatingPower = Mathf.Clamp(heatingPower, 0f, 5f);
            UpdatePansHeatingPower();

            yield return new WaitForEndOfFrame();
        }
    }

    /*
    private void TakePan()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider)
            {
                Pan pan = hit.collider.GetComponent<Pan>();
                
                if (pan)
                {
                    isBeingUsed = Input.GetMouseButton(0);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isBeingUsed = false;
        }
    }
    */

    private void MousePositionUpdate()
    {
        currentMousePosition = Input.mousePosition;
        mousePositionToScreenCoordinate = Camera.main.ScreenToViewportPoint(currentMousePosition);

        mouseX = Mathf.Clamp01(mousePositionToScreenCoordinate.x);
        mouseY = Mathf.Clamp01(mousePositionToScreenCoordinate.y);
    }

    // Change pan position according to mouse position
    private void PanMovement()
    {
        float xRemap = ExtensionMethods.Remap(mouseX, 0f, 1f, -movementX, movementX);
        float yRemap = ExtensionMethods.Remap(mouseY, 0f, 1f, -movementY, movementY);
        float zRemap = ExtensionMethods.Remap(mouseX, 0f, 1f, -movementZ, movementZ);

        targetPosition.x = invertMovementX == true ? xRemap : -xRemap;
        targetPosition.y = invertMovementY == true ? yRemap : -yRemap;
        targetPosition.z = invertMovementZ == true ? yRemap : -yRemap;

        transform.position = targetPosition;
    }

    // Change pan rotation according to mouse position
    private void PanRotation()
    {
        float xRemap = ExtensionMethods.Remap(mouseX, 0f, 1f, -rotationX, rotationX);
        float yRemap = ExtensionMethods.Remap(mouseY, 0f, 1f, -rotationY, rotationY);
        float zRemap = ExtensionMethods.Remap(mouseX, 0f, 1f, -rotationZ, rotationZ);

        targetEulerAngle.x = invertRotationX == true ? yRemap : -yRemap;
        targetEulerAngle.y = invertRotationY == true ? xRemap : -xRemap;
        targetEulerAngle.z = invertRotationZ == true ? zRemap : -zRemap;

        transform.eulerAngles = targetEulerAngle;
    }

    private void SetAllPansParts()
    {
        pans = new Pan[pansRoot.childCount];

        for (int i = 0; i < pans.Length; i++)
        {
            pans[i] = pansRoot.GetChild(i).GetComponent<Pan>();
        }
    }

    private void UpdatePansHeatingPower()
    {
        foreach (Pan pan in pans)
        {
            pan.heatingPower = heatingPower;
        }
    }
}