using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobbleDrunk : MonoBehaviour
{
    public float wobbleSpeed = 1f;    // Base speed of the wobble
    public float wobbleAmount = 0.1f; // Base amount of the wobble
    public static float wobblespeedincrease = 0.3f, wobbleamountincrease = 0.04f;
    private static float currentWobbleSpeed;
    private static float currentWobbleAmount;
    private float wobbleTimer;

    private Vector3 initialPosition;

    private Transform cameraTransform;

    //public MonoBehaviour cupdrink;

    void Start()
    {
        // Store the initial position of the camera
        initialPosition = transform.localPosition;
        
        //cupdrink<OwnCupDrinking>.HitDrunk() += UpdateDrunkness();

        // Initialize current wobble parameters
        currentWobbleSpeed = wobbleSpeed;
        currentWobbleAmount = wobbleAmount;

        // Get the camera's transform
        cameraTransform = Camera.main.transform;
    }


    public static void UpdateDrunkness()
    {
        currentWobbleSpeed += wobblespeedincrease;  // Increase wobble speed
        currentWobbleAmount += wobbleamountincrease; // Increase wobble amount
    }

    void Update()
    {
        

        // Apply the wobble effect
        wobbleTimer += Time.deltaTime * currentWobbleSpeed;
        float wobbleX = Mathf.Sin(wobbleTimer) * currentWobbleAmount;
        float wobbleY = Mathf.Cos(wobbleTimer * 0.5f) * currentWobbleAmount;

        // Apply wobble to local position
        transform.localPosition = initialPosition + new Vector3(wobbleX, wobbleY, 0);

        // Apply wobble to rotation, without affecting player control
        //cameraTransform.localRotation *= Quaternion.Euler(wobbleY * 10, wobbleX * 10, 0);
    }
}
