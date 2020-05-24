using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour{

    Rigidbody rigidbody;
    AudioSource audio;

    [SerializeField] float rocketRotationThrust = 10f;
    [SerializeField] float rocketUpThrust = 10f;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        ThrustingAction();
    }

    void OnCollisionEnter(Collision collision)
    {
        print("You have collided");
    }

    private void Rotate()
    {

        rigidbody.freezeRotation = true;
        float rotationSpeed = rocketRotationThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            
            transform.Rotate(Vector3.forward * rotationSpeed);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);

        }

        rigidbody.freezeRotation = false;
    }

    private void ThrustingAction()
    {
        if (Input.GetKey(KeyCode.Space)) //can rotate while boosting - 2 ifs
        {
            rigidbody.AddRelativeForce(Vector3.up * rocketUpThrust);
            

            if (!audio.isPlaying)
            {
                audio.Play();
            }
            else
            {
                audio.Stop();
            }
        }
    }
}
