using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour

{
	Vector2 rotation = Vector2.zero;
	public float speed = 3;
    public Transform targetPosition; 
    public Transform throwPoint;
    public float interactionRange = 5f;    // How far you can interact with objects
    public LayerMask Ball; 
    private bool isBallTeleported;
    public Transform teleportedObject;
    public GameObject BallToThrow;
    public float throwForce = 500f;
    private bool GonnaThrow;
    public Vector3 additionalOffset = new Vector3(0, 0, 1);


    void Start()
    {
           Cursor.lockState = CursorLockMode.Locked;
    }
	void Update () {
        // Movement for the mouse
		rotation.y += Input.GetAxis ("Mouse X");
		rotation.x += -Input.GetAxis ("Mouse Y");
		transform.eulerAngles = (Vector2)rotation * speed;
        // Movement end of the mouse

         if (Input.GetKeyDown(KeyCode.E))
        {
            InteractWithObject();
        }

         if (isBallTeleported && GonnaThrow == false)
        {
            teleportedObject.position = targetPosition.position;
        }

        if (isBallTeleported && (Input.GetMouseButtonDown(0)))
        {
            GonnaThrow = true;
            Throw();
        }
	}

    void InteractWithObject()
    {
        RaycastHit hit;
        // Cast a ray from the camera's position forward to check for interactable objects
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionRange, Ball))
        {
            if (hit.transform.CompareTag("Ball"))
            {
                // Perform interaction logic here
                Debug.Log("Interacted with " + hit.transform.name);
                hit.transform.position = targetPosition.position;
                isBallTeleported = true;
                GonnaThrow = false;
                // Additional interaction logic can be added here
            }
        }
    }

    void Throw()
    {
        Rigidbody rb = BallToThrow.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 throwDirection = throwPoint.forward;
            rb.AddForce(throwDirection * throwForce);
        }

    }
   

}
