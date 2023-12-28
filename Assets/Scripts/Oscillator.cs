using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    private Vector3 startingPosition;
    [SerializeField]
    private Vector3 movementVector = new Vector3(10, 0, 0);
    [SerializeField]
    private float period = 2f;
    private const float TAU = Mathf.PI * 2;


    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float cycles = Time.time / period;
        float amplitude = Mathf.Sin(cycles * TAU);
        Vector3 offset = movementVector * amplitude;
        transform.position = startingPosition + offset;
    }
}
