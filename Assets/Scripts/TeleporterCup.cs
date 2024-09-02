using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterCup : MonoBehaviour
{
    public GameObject Ball;

    public string CollisionTagBall;
    public GameObject BallSpawner;

void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the specific tag
        Debug.Log("Test");
        if (collision.gameObject.CompareTag(CollisionTagBall))
        {
            // Teleport the target object to the destination object's position
            Rigidbody rb = Ball.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            StartCoroutine(WaitForBallToSpawn());
            Ball.transform.position = BallSpawner.transform.position;
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


}
