using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSlerpSpeed;
    [SerializeField] private Gun gun;

    [SerializeField] private KeyCode shootKey;

    private Vector2 moveDir = Vector2.zero;
    private Vector3 moveVelocity = Vector3.zero;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        moveVelocity = new Vector3(moveDir.x, 0, moveDir.y);

        rb.velocity = moveVelocity * moveSpeed;
    }

    private void Update()
    {
        if (Input.GetKey(shootKey))
            gun.Shoot();

        if (moveDir.magnitude != 0)
        {
            float targetAngle = Mathf.Atan2(moveDir.x, moveDir.y) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSlerpSpeed * Time.deltaTime);
        }
    }
}