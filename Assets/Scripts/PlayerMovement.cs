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

    [SerializeField]
    private Light headligh;

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
        bool userPushLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool userPushRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        bool userPushZDown = Input.GetKeyDown(KeyCode.Z);
        if(userPushLeft)
        {
            RotateLeft();
        }
        if(userPushRight)
        {
            RotateRight();
        }
        if(!userPushRight)
        {
            ClearRightRotation();
        }
        if(!userPushLeft)
        {
            ClearLeftRotation();
        }

        if(userPushZDown)
        {
            ToggleHeadLight();
        }
    }

    private void ToggleHeadLight()
    {
        headligh.enabled = !headligh.enabled;
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
