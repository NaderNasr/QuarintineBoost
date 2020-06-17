using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rocket : MonoBehaviour{


    private new Rigidbody rigidbody;
    AudioSource audioSource;
    //[SerializeField] Text FuelText;
    //[SerializeField] Image FuelImage;

    //Scene Manager
    [SerializeField] float levelDelay = 2f;

    [Header("Movement Speed")]
    [SerializeField] float rocketRotationThrust = 10f;
    [SerializeField] float rocketUpThrust = 10f;

    //[Header("Fuel Burn Rate")]
    //[SerializeField] float burnRatePerSecond = 10f;
    //[SerializeField] float maxFuelPerSecond = 100f;

    [Header("Audio")]
    [SerializeField] AudioClip boostAudio;
    [SerializeField] AudioClip deathAudio;
    [SerializeField] AudioClip successAudio;

    [Header("Particles")]
    [SerializeField] ParticleSystem boostParticles;
    [SerializeField] ParticleSystem boostParticles2;

    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem successParticles;

    [Header("Collision Debugger")]
    public bool collisionDisabled = false;

   
    enum GameState {Alive, Dead, Next};
    GameState state = GameState.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

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

        if (Debug.isDebugBuild) // Debug keys only  respond in development mode
        {
            RespondToDebugLogs();
        }

       
    }

  

    private void RespondToDebugLogs()
    {
        if (Input.GetKeyDown(KeyCode.L)) // start next level
        {
            StartNextLevel();
        } 
        else if(Input.GetKeyDown(KeyCode.C)) // disable collision
        {
            collisionDisabled = !collisionDisabled; // toggle switch
        }
    }

 

    void OnCollisionEnter(Collision collision)
    {

        if( state != GameState.Alive || collisionDisabled) {return;} // basically if dead just go back to scene.

        switch (collision.gameObject.tag)
        {

            //KEEP IT DRY AS F
            case "Friendly":
                state = GameState.Alive;
                //Do stuff like animation?
                break;
            case "Finish":
                StartNextLevel();
                break;
            default:
                StartLevelAgain();
                break;

        }
            
    }

    private void StartNextLevel()
    {
        state = GameState.Next;
        audioSource.Stop();
        successParticles.Play();
        audioSource.PlayOneShot(successAudio);
        Invoke("LoadNextLevel", levelDelay);
    }


    private void StartLevelAgain()
    {
        state = GameState.Dead;
        audioSource.Stop();
        deathParticles.Play();
        audioSource.PlayOneShot(deathAudio);
        Invoke("LoadFirstLevel", levelDelay);
    }

   

    private void LoadFirstLevel()
    {
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        //print(activeScene);
        int nextLevel = activeScene + 1;
        //load level till end of last scene then reset
        if(nextLevel == SceneManager.sceneCountInBuildSettings)
        {
            nextLevel = 0;
        }
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
            audioSource.Stop();
            boostParticles.Stop();
            boostParticles2.Stop();

        }
    }

    private void ApplyBoost()
    {
        rigidbody.AddRelativeForce(Vector3.up * rocketUpThrust * Time.deltaTime); //real time
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(boostAudio);
        }
        boostParticles.Play();
        boostParticles2.Play();

        //FuelImage.fillAmount = maxFuelPerSecond / burnRatePerSecond;
        //FuelText.text = "Fuel: " + ((int)(maxFuelPerSecond -= burnRatePerSecond)).ToString() + " %";


       //print(maxFuelPerSecond);
       //print(Time.deltaTime+"Deltaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

      //if (maxFuelPerSecond <= 0f)
      //  {
      //     StartLevelAgain();
      //  }



    }

    
}
