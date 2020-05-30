using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{

    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

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
        //Math sin wave for movement of enemy object 
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2f;
        float sinWave = Mathf.Sin(cycles * tau);

        //     print(sinWave);

        movementFactor = sinWave / 2f + 0.5f; // dived by to go between .5 and -.5 + 0.5 to go to .5 and 1


        Vector3 offset = movementFactor * movementVector;
        transform.position = startPos + offset;
    }
}
