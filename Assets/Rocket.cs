using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour{
    //add wind according to clouds 
    Rigidbody rigidbody;
    AudioSource audio;

    [SerializeField] float rocketRotationThrust = 10f;
    [SerializeField] float rocketUpThrust = 10f;


    enum GameState {Alive, Dead, Next};
    GameState state = GameState.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO - stop sound too!
        if (state == GameState.Alive)
        {
            StartRotation();
            StartBoosting();
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        if( state != GameState.Alive) {return;} // basically if dead just go back to scene.

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                state = GameState.Alive;
                print("Friendly Pad hit");
                break;
            case "Finish":
                state = GameState.Next;
                print("Finish Pad Hit");
                Invoke("LoadNextLevel", 1f);
                break;
            default:
                state = GameState.Dead;
                print("You are dead");
                Invoke("LoadFirstLevel", 1f);
                //Reset Game
                break;
           

        }
            
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }

    private void StartRotation()
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

    private void StartBoosting()
    {
        if (Input.GetKey(KeyCode.Space)) //can rotate while boosting - 2 ifs
        {
            ApplyBoost();
        }
        else
        {
            audio.Stop();
        }
    }

    private void ApplyBoost()
    {
        rigidbody.AddRelativeForce(Vector3.up * rocketUpThrust);


        if (!audio.isPlaying)
        {
            audio.Play();
        }
        
    }
}
