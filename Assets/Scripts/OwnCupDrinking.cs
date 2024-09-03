using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System;

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
    

    public Volume volume;
    private ChromaticAberration chromaticAberration;
    private LensDistortion lensDistortion;
    private Bloom bloom;

    private bool isDrinking = false;   // Flag to indicate if the drinking action has started
    //private Vector3 initialCupPosition; // Initial position of the cup
    //private Quaternion initialCupRotation; // Initial rotation of the cup



     void Start()
    {
        // Ensure the Volume component has a Chromatic Aberration override
        if (volume.profile.TryGet<ChromaticAberration>(out chromaticAberration))
        {
            // Optional: Set the initial intensity if needed
            chromaticAberration.intensity.value = 0f;
        }
        else
        {
            Debug.LogWarning("No Chromatic Aberration override found in the Volume profile.");
        }

        if (volume.profile.TryGet<LensDistortion>(out lensDistortion))
        {
            // Optional: Set the initial distortion value if needed
            lensDistortion.intensity.value = 0f;
        }
        else
        {
            Debug.LogWarning("No Lens Distortion override found in the Volume profile.");
        }

         if (volume.profile.TryGet<Bloom>(out bloom))
        {
            // Optional: Set the initial bloom intensity if needed
            bloom.intensity.value = 0f;
        }
        else
        {
            Debug.LogWarning("No Bloom override found in the Volume profile.");
        }
        
    }





    private void Update()
    {
        if (isDrinking)
        {
            // Move the cup towards the camera
            transform.position = Vector3.Lerp(transform.position, cameraTransform.position, Time.deltaTime * drinkSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, cameraTransform.rotation, Time.deltaTime * drinkSpeed);
        }
    }


    //public event Action HitDrunk;

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
            //HitDrunk.Invoke();
            WobbleDrunk.UpdateDrunkness();
            DisableAllColliders(gameObject);
            StartCoroutine(WaitForBallToSpawn());
            DisableAllColliders(gameObject);
            Ball.transform.position = BallSpawner.transform.position;

            
            
        }

    }


    void DisableAllColliders(GameObject obj)
    {
        // Disable the collider on the current GameObject
        Collider[] colliders = obj.GetComponents<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
        // Disable the colliders on all child GameObjects
        foreach (Transform child in obj.transform)
        {
            DisableAllColliders(child.gameObject);
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
        GettingDrunk();
    }


     void GettingDrunk()
    {
        chromaticAberration.intensity.value = Mathf.Clamp(chromaticAberration.intensity.value + 0.5f, 0f, 1f);
        lensDistortion.intensity.value = Mathf.Clamp(lensDistortion.intensity.value + 0.05f, -1f, 1f);
        bloom.intensity.value = Mathf.Clamp(bloom.intensity.value + 0.15f, 0f, 10f);
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
