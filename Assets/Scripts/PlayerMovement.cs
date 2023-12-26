using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float thrustForce = 1500f;
    [SerializeField]
    private float rotateSpeed = 100f;

    Rigidbody rb;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            RotateLeft();
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            RotateRight();
        }
    }

    private void RotateRight()
    {
        Rotate(-1);
    }

    private void RotateLeft()
    {
        Rotate(1);
    }

    private void Rotate(int direction)
    {
        // rb.freezeRotation = true;
        transform.Rotate(direction * rotateSpeed * Time.deltaTime * Vector3.forward);
        // rb.freezeRotation = false;
    }

    private void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            BoostRocket();
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }

        }
        else
        {
            audioSource.Stop();
        }
    }

    private void BoostRocket()
    {
        rb.AddRelativeForce(thrustForce * Time.deltaTime * Vector3.up);
    }
}
