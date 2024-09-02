using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float interactionRange = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InteractWithObject()
    {
        RaycastHit hit;
        // Cast a ray from the camera's position forward to check for interactable objects
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionRange))
        {
            if (hit.transform.CompareTag("Item"))
            {
                Debug.Log("Interacted with " + hit.transform.name);

            }
            else if (hit.transform.CompareTag("Ball"))
            {
                // Fire event that ball script can subscribe to
                OnBallHit?.Invoke(hit.transform);
            }
        }
    }

    // Define an event to notify other scripts when a ball is hit
    public event Action<Transform> OnBallHit;

    void OnPickUp(InputValue value)
    {
        InteractWithObject();
    }
}
