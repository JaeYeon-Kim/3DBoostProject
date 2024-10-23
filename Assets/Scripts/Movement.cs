using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Rider.Unity.Editor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float mainThrust = 100f;

    [SerializeField] private float rotationThrust = 1f;

    private Rigidbody rb;

    private AudioSource audioSource;
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

    private void ProcessThrust()
    {
        // 위로 가하는 힘 
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);           // (0,1,0) : Vector3.up
            
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }


    private void ProcessRotation()
    {
        // 좌측 회전 
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        }

        // 우측 회전
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;   // 수동 제어를 할 수 있도록 회전을 고정 한다. 
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;  // 물리 시스템이 적용 되도록 회전 고정을 해제 한다. 
    }
}
