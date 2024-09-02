using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    private Vector2 moveInput = Vector2.zero;
    private bool isGrounded = false;
    private float rotationX = 0f;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    [SerializeField]
    private bool LockMovement = false;
    [Range(0.1f, 1f)]
    public float sensitivity = 0.5f;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Run();
    }

    private void Run()
    {
        if (!LockMovement)
        {
            Vector3 playerVelocity = new Vector3(moveInput.x * moveSpeed, m_Rigidbody.velocity.y, moveInput.y * moveSpeed);
            m_Rigidbody.velocity = transform.TransformDirection(playerVelocity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            LockMovement = false;
        }
        else if (collision.gameObject.tag != "Ground" && !isGrounded)
        {
            LockMovement = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    void OnMove(InputValue value)
    {
        if (!LockMovement)
        {
            moveInput = value.Get<Vector2>();
        }
    }

    void OnJump(InputValue value)
    {
        if (isGrounded && !LockMovement)
        {
            m_Rigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }

    void OnLook(InputValue value)
    {
        Vector2 rotation = value.Get<Vector2>();
        Transform camera = transform.GetChild(0);

        // Rotate the player around the Y axis (horizontal rotation) with sensitivity
        transform.Rotate(new Vector3(0, rotation.x * sensitivity, 0));

        // Adjust rotationX based on the mouse input and sensitivity
        rotationX -= rotation.y * sensitivity;

        // Clamp the vertical rotation to avoid flipping
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        // Apply the clamped rotation to the camera
        camera.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }
}
