using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    

    public GameObject Ball;

    public GameObject targetObject; // The GameObject to be teleported
    public GameObject destinationObject; // The GameObject representing the destination
    public string collisionTagGround; // The tag of the specific GameObject that triggers the teleport
    public string collisionTagCup;


     void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the specific tag
        if (collision.gameObject.CompareTag(collisionTagGround))
        {
            // Teleport the target object to the destination object's position
            Rigidbody rb = Ball.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            StartCoroutine(WaitForBallToSpawn());
            targetObject.transform.position = destinationObject.transform.position;
        }

    }





    IEnumerator WaitForBallToSpawn()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        Rigidbody rb = Ball.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
        


    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
