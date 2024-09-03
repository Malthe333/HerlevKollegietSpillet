using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour

{
    [SerializeField]
    private Transform targetPosition;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float throwForce = 500f;
    private Rigidbody ball_rb;
    private Transform ball;


    void Start()
    {
        player.GetComponent<PlayerController>().OnBallHit += Movement_OnBallHit;
        player.GetComponent<PlayerController>().OnFirePress += Movement_OnFirePress;
    }

    private void Movement_OnFirePress(Transform obj)
    {
        if (ball != null && ball.parent != null)
        {
            Throw();
        }
    }

    private void Movement_OnBallHit(Transform ballTransform)
    {
        ballTransform.position = targetPosition.position;
        ballTransform.rotation = targetPosition.rotation;
        ballTransform.parent = targetPosition;
        ball_rb = ballTransform.gameObject.GetComponent<Rigidbody>();
        ball_rb.isKinematic = true;
        ball = ballTransform;
    }

    void Throw()
    {
        ball_rb.isKinematic = false;
        ball_rb.AddForce(ball.forward * throwForce);
        ball.parent = null;
    }
   

}
