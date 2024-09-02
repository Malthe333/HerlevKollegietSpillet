using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnCupDrinking : MonoBehaviour
{
    
    //Ballspawner
    public GameObject Ball;
    public string CollisionTagBall;
    public GameObject BallSpawner;
    public ParticleSystem watersplash;
    //End of Ballspawner

    public Transform cameraTransform; // Reference to the camera's transform
    public float drinkSpeed = 2.0f;    // Speed at which the cup moves to the camera
    public float despawnDelay = 2.0f;  // Time before the cup despawns after "drinking"
    public float wobbleIntensity = 0.5f; // Intensity of the camera wobble
    public float wobbleDuration = 5.0f;  // Duration of the wobble effect

    private bool isDrinking = false;   // Flag to indicate if the drinking action has started
    //private Vector3 initialCupPosition; // Initial position of the cup
    //private Quaternion initialCupRotation; // Initial rotation of the cup


    private void Update()
    {
        if (isDrinking)
        {
            // Move the cup towards the camera
            transform.position = Vector3.Lerp(transform.position, cameraTransform.position, Time.deltaTime * drinkSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, cameraTransform.rotation, Time.deltaTime * drinkSpeed);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the specific tag
        
        if (collision.gameObject.CompareTag(CollisionTagBall))
        {
            // Teleport the target object to the destination object's position
            Rigidbody rb = Ball.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            StartCoroutine(DrinkAndDespawn());
            watersplash.Play();
            StartCoroutine(WaitForBallToSpawn());
            Ball.transform.position = BallSpawner.transform.position;
        }

    }

    private IEnumerator DrinkAndDespawn()
    {
        isDrinking = true;

        // Wait for the drinking animation to complete
        yield return new WaitForSeconds(despawnDelay);

        // Despawn the cup
        Destroy(gameObject);

        // Start the camera wobble effect
        StartCoroutine(CameraWobble());
    }


     private IEnumerator CameraWobble()
    {
        float elapsedTime = 0f;

        while (elapsedTime < wobbleDuration)
        {
            // Apply wobble effect to the camera's rotation
            float wobbleX = Mathf.Sin(Time.time * wobbleIntensity) * wobbleIntensity;
            float wobbleY = Mathf.Cos(Time.time * wobbleIntensity) * wobbleIntensity;

            cameraTransform.localRotation = Quaternion.Euler(wobbleX, wobbleY, cameraTransform.localRotation.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Reset the camera's rotation after the wobble effect
        cameraTransform.localRotation = Quaternion.identity;
    }


    IEnumerator WaitForBallToSpawn()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        Rigidbody rb = Ball.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
        //Destroy(gameObject);

    }




}
