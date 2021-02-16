using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor

    //CACHE - e.g. references for readability or speed

    //STATE - private instance (member) variables


    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainThrustSFX;
    //other audio clips are managed in collision handler
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

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainThrustSFX);
            }
        }
        else 
        {
            audioSource.Stop();
        }
    }
    
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            applyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            applyRotation(-rotationThrust);
        }
    }

    void applyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //freezing rotation to manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //unfreezing rotation so the physics systems take over
    }
}
