using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
물체를 움직이게 하는 코드 
*/
public class Oscillator : MonoBehaviour
{

    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0, 1)]float movementFactor;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
