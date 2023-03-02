using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{

    public int length;
    public LineRenderer lineRend;
    public Vector3[] segmentPose;
    public Vector3[] segmentV;

    public Transform targetDir;
    public float targetDistance;
    public float smoothSpeed;


    public float wiggleSpeed;
    public float wiggleMagnitude;
    public Transform wiggleDir;

    public Transform[] bodyParts;
    public Transform tailEnd;

    

    private void Start()
    {
        lineRend.positionCount = length;
        segmentPose = new Vector3[length];
        segmentV = new Vector3[length];
        
    }

    private void Update()
    {
        wiggleDir.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * wiggleSpeed) * wiggleMagnitude);

        segmentPose[0] = targetDir.position;

        for (int i = 1; i < segmentPose.Length; i++) 
        {
            Vector3 targetPos = segmentPose[i - 1] + (segmentPose[i] - segmentPose[i - 1]).normalized * targetDistance;
            segmentPose[i] = Vector3.SmoothDamp(segmentPose[i], targetPos, ref segmentV[i], smoothSpeed);
            bodyParts[i - 1].transform.position = segmentPose[i];
            Debug.Log(segmentPose.Length);
        }

        lineRend.SetPositions(segmentPose);
        tailEnd.position = segmentPose[segmentPose.Length - 1];
    }
}
