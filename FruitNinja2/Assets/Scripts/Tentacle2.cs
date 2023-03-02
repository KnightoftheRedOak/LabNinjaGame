using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle2 : MonoBehaviour
{
    public int length;
    public float targetDistance;
    public LineRenderer lineRend;
    public Vector3[] segementPoses;
    public Transform targetDir;
    private Vector3[] segementV;
    public float smoothSpeed;
    void Start()
    {
        lineRend.positionCount = length;
        segementPoses = new Vector3[length];
        segementV = new Vector3[length];

    }

   
    void Update()
    {
        segementPoses[0] = targetDir.position;

        for (int i = 1; i < segementPoses.Length; i++) 
        {
            segementPoses[i] = Vector3.SmoothDamp(segementPoses[i], segementPoses[i - 1] + targetDir.right * targetDistance, ref segementV[i], smoothSpeed);
        }
        lineRend.SetPositions(segementPoses);
    }
}
