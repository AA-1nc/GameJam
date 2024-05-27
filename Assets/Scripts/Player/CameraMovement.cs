using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float moveAmount;
    [SerializeField] private float lerpSpeed;

    private Vector3 targetPosition;

    private void LateUpdate()
    {
        targetPosition = target.position / moveAmount;
        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);
    }
}
