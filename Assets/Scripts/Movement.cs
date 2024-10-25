using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Rider.Unity.Editor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float mainThrust = 100f;
    [SerializeField] private float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;


    // 부스터 파티클 시스템
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;


    private Rigidbody rb;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        // 위로 가하는 힘 
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();

        }
        else
        {
            StopThrusting();
        }
    }


    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);           // (0,1,0) : Vector3.up

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }


    private void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void ProcessRotation()
    {
        // 좌측 회전 
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }

        // 우측 회전
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            rightThrusterParticles.Stop();
            leftThrusterParticles.Stop();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationThrust);

        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationThrust);

        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;   // 수동 제어를 할 수 있도록 회전을 고정 한다. 
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;  // 물리 시스템이 적용 되도록 회전 고정을 해제 한다. 
    }
}
