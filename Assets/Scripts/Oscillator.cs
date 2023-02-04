using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;
    float movementFactor;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon) return; // can't compare float to float exactly, epislon is smallest float possible

        float cycles = Time.time / period; // continually growing over time

        const float tau = Mathf.PI * 2;  // constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); //going from -1 to 1

        //ability to make movement between 0 and 1 so it's cleaner
        movementFactor = (rawSinWave + 1f)/2f;

        Vector3 offset = movementFactor * movementVector;
        transform.position = startingPosition + offset;
    }
}
