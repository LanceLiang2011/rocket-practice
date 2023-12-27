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
    [SerializeField]
    private AudioClip engineSound;

    [SerializeField]
    private ParticleSystem engineParticle;
    [SerializeField]
    private ParticleSystem rigthParticle;
    [SerializeField]
    private ParticleSystem leftParticle;

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
        if(!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.RightArrow))
        {
            ClearRightRotation();
        }
        if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftArrow))
        {
            ClearLeftRotation();
        }
    }

    private void ClearLeftRotation()
    {
        if(leftParticle.isPlaying)
        {
            leftParticle.Stop();
        }
    }

    private void ClearRightRotation()
    {
        if(rigthParticle.isPlaying)
        {
            rigthParticle.Stop();
        }
    }

    private void RotateRight()
    {
        Rotate(-1);
        if(!rigthParticle.isPlaying)
        {
            rigthParticle.Play();
        }
    }

    private void RotateLeft()
    {
        Rotate(1);
        if(!leftParticle.isPlaying)
        {
            leftParticle.Play();
        }
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
                audioSource.PlayOneShot(engineSound);
            }
            if(!engineParticle.isPlaying)
            {
                engineParticle.Play();
            }

        }
        else
        {
            audioSource.Stop();
            engineParticle.Stop();
        }
    }

    private void BoostRocket()
    {
        rb.AddRelativeForce(thrustForce * Time.deltaTime * Vector3.up);
    }
}
