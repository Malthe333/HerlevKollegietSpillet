using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour

{
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
    public GameObject player;


    void Start()
    {
        player.GetComponent<PlayerController>().OnBallHit += Movement_OnBallHit;
    }

    private void Movement_OnBallHit(Transform ballTransform)
    {
        ballTransform.transform.position = targetPosition.position;
        ballTransform.transform.rotation = targetPosition.rotation;
        isBallTeleported = true;
        GonnaThrow = false;
        Rigidbody rb = BallToThrow.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePosition;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        BallToThrow.transform.parent = targetPosition;
    }

    void Update () {

         if (isBallTeleported && GonnaThrow == false)
        {
            teleportedObject.position = targetPosition.position; //den accellerer 
        }

        if (isBallTeleported && (Input.GetMouseButtonDown(0)))
        {
            GonnaThrow = true;
            Throw();
        }
	}

    void Throw()
    {
        Rigidbody rb = BallToThrow.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 throwDirection = throwPoint.forward;
            rb.AddForce(BallToThrow.transform.forward * throwForce);
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.None;
            isBallTeleported = false;
            BallToThrow.transform.parent= null;

        }

    }

    void OnFire()
    {
        Debug.Log("Fire!");
    }
   

}
