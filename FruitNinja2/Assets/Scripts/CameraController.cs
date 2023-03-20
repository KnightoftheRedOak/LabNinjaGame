using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float shakeTime;
    private float shakeTimeCounter;
    public static CameraController instance;
    public float shakeTimeForce;
    void Start()
    {
        instance = this;
        
    }

    
    void Update()
    {
        if (shakeTimeCounter > 0)
        {
            shakeTimeCounter -= Time.deltaTime;
            float x = Random.Range(-.05f, .05f) * shakeTimeForce;
            float y = Random.Range(-.05f, .05f) * shakeTimeForce;

            transform.position = new Vector3(x, y, transform.position.z);
        }
        else 
        {
            transform.position = new Vector3(0, 0, -10);
            shakeTimeForce = 0;
        }
    }

    public void shakeCamera(float shakeTimeStrength) 
    {
        shakeTimeCounter = shakeTime;
        shakeTimeForce = shakeTimeStrength;
    }
}
