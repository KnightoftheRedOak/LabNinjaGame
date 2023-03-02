using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEditor.UIElements;
using UnityEngine;

public class ChosenPlateImage : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    void Start()
    {
        
    }

   
    void Update()
    {
        transform.position += new Vector3(1,0,0) * moveSpeed * Time.deltaTime;
        

    }
}
