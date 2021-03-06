﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{

   
    [SerializeField] float period = 2f;
    [SerializeField] Vector3 movementVector;

    [Range(0,1)][SerializeField] float movementFactor;


    Vector3 startPos;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //error if period goes to 0 = not a number 404 fix

        if(period <= Mathf.Epsilon) //episilon used as lowest floating number possible!
        {
            return;
        }
        //Math sin wave for movement of enemy object 
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2f;
        float sinWave = Mathf.Sin(cycles * tau); // -1 to +1

        //print(Mathf.Sin(tau / 4f));
        //     print(sinWave);

        movementFactor = sinWave / 2f + 0.5f; // dived by to go between .5 and -.5 + 0.5 to go to .5 and 1


        Vector3 offset = movementFactor * movementVector;
        transform.position = startPos + offset;
    }
}
