using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBDragon : MonoBehaviour
{
    public float amp;
    public float freq;
    void Start()
    {
        
    }

   
    void Update()
    {
        transform.position = new Vector3(0, Mathf.Sin(Time.time * freq) * amp, 0);
    }
}
