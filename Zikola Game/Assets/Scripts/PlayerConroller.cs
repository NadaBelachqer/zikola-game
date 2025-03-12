using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 500f;

    Quaternion targetRotation;
    CameraController cameraController;

    private void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Calculate movement amount (prevents unnecessary calculations when idle)
        float moveAmount = Mathf.Abs(h) + Mathf.Abs(v);

        // Get movement direction based on input
        var moveInput = new Vector3(h, 0, v).normalized;

        // Rotate movement direction to match the camera's direction
        var moveDir = cameraController.PlanarRotation * moveInput;

        if (moveAmount > 0)
        {
            // Move the player smoothly
            transform.position += moveDir * moveSpeed * Time.deltaTime;

            // Set the target rotation towards movement direction
            targetRotation = Quaternion.LookRotation(moveDir);
        }

        // Smoothly rotate towards target rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}