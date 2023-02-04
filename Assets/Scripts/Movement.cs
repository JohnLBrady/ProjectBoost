using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float craftSpeed = 50f;
    [SerializeField] float craftRotate = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainThruster;
    [SerializeField] ParticleSystem rightThruster;
    [SerializeField] ParticleSystem leftThruster;

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

    void ProcessThrust(){
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation(){
        if(Input.GetKey(KeyCode.A))
        {
            ThrustLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            ThrustRight();
        }
        else
        {
            StopRotationParticles();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainThruster.Stop();
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * Time.deltaTime * craftSpeed);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainThruster.isPlaying)
        {
            mainThruster.Play();
        }
    }

    private void ThrustLeft()
    {
        //Rotate to the left
        ApplyRotation(craftRotate);
        if (!rightThruster.isPlaying)
            rightThruster.Play();
    }

    private void ThrustRight()
    {
        //Rotate to the right
        ApplyRotation(-craftRotate);
        if (!leftThruster.isPlaying)
            leftThruster.Play();
    }

    private void ApplyRotation(float rotationSpeed)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
        rb.freezeRotation = false;
    }

    private void StopRotationParticles()
    {
        rightThruster.Stop();
        leftThruster.Stop();
    }
    
}
