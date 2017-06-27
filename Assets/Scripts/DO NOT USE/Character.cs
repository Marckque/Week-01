using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
    public float duration = 1f;

    public float delay = 1f;
    private float delayRemaining;

    public AnimationCurve curve;

    public float moveDistance = 3f;
    public float multiplier = 2f;
    private float actualMultiplier = 0f;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private Vector3 direction;
    private Vector3 lastDirection;

    float right, left, up, down;
    bool rightPressed, leftPressed, upPressed, downPressed;

    private bool isMoving = false;
    private bool isWaiting = false;

    protected void Start()
    {
        direction = transform.forward;
        lastDirection = direction;

        setStartPosition();
        setEndPosition();

        StartCoroutine(move());
    }

    private void Controls()
    {
        // RIGHT
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!rightPressed)
            {
                right = Time.time;
            }

            rightPressed = true;
        }
        else
        {
            right = 0f;
            rightPressed = false;
        }

        // LEFT
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if(!leftPressed)
            {
                left = Time.time;
            }

            leftPressed = true;
        }
        else
        {
            left = 0f;
            leftPressed = false;
        }

        // UP
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (!upPressed)
            {
                up = Time.time;
            }

            upPressed = true;
        }
        else
        {
            up = 0f;
            upPressed = false;
        }

        // DOWN
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (!downPressed)
            {
                down = Time.time;
            }

            downPressed = true;
        }
        else
        {
            down = 0f;
            downPressed = false;
        }

        float highest = Mathf.Max(right, Mathf.Max(left, Mathf.Max(up, down)));

        if (Mathf.Approximately(highest, right))
        {
            direction = Vector3.right;
        }
        else if (Mathf.Approximately(highest, left))
        {
            direction = Vector3.left;
        }
        else if (Mathf.Approximately(highest, up))
        {
            direction = Vector3.forward;
        }
        else if (Mathf.Approximately(highest, down))
        {
            direction = Vector3.back;
        }
    }

    protected void Update()
    {
        Controls();

        if (direction != Vector3.zero)
        {
            lastDirection = direction;
        }

        if (Input.GetKey(KeyCode.Space) && isWaiting)
        {
            actualMultiplier = multiplier;
            delayRemaining = -Time.deltaTime;
        }
        else
        {
            actualMultiplier = 0f;
        }
    }

    private void setStartPosition()
    {
        startPosition = transform.position;
    }

    private void setEndPosition()
    {
        if (direction != Vector3.zero)
        {
            endPosition = startPosition + ((direction * moveDistance) * (1f + actualMultiplier));
        } 
        else
        {
            endPosition = startPosition + ((lastDirection * moveDistance) * (1f + actualMultiplier));
        }
    }

    private IEnumerator move()
    {
        isMoving = true;

        float startTime = Time.time;
        float timeElapsed = 0f;
        float completionPercentage = 0f;

        setEndPosition();

        while (completionPercentage < 1f)
        {
            timeElapsed = Time.time - startTime;
            completionPercentage = timeElapsed / duration;

            float remapedPercentage = curve.Evaluate(completionPercentage);

            transform.position = Vector3.Lerp(startPosition, endPosition, remapedPercentage);

            yield return new WaitForEndOfFrame();
        }

        setStartPosition();

        isMoving = false;

        StartCoroutine(delayBeforeMove());
    }

    private IEnumerator delayBeforeMove()
    {
        isWaiting = true;
        delayRemaining = delay;

        while (delayRemaining > 0f)
        {
            delayRemaining -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        isWaiting = false;

        StartCoroutine(move());
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(startPosition, 1f);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(startPosition, endPosition);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(endPosition, 1f);
    }
}