using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreptCharacterController : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed;
    public Transform[] wayPoints;
    public int wayPointIndex;
    public Vector2 movement;
    public GameObject firstBodyPart;
    private float SpawnCounter;
    public float timeBtwnSpawns;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        movement = wayPoints[wayPointIndex].transform.position - transform.position;
        rb.velocity = movement.normalized * moveSpeed;

        if (Vector2.Distance(transform.position, wayPoints[wayPointIndex].transform.position) < .2f) 
        {
            wayPointIndex++;
        }

        if (wayPointIndex > wayPoints.Length - 1) 
        {
            wayPointIndex = 0;
        }

        if (SpawnCounter <= 0)
        {
           GameObject firstStrept = Instantiate(firstBodyPart, transform.position, transform.rotation);
            Destroy(firstStrept, .5f);

            SpawnCounter = timeBtwnSpawns;
        }
        else 
        {
            SpawnCounter -= Time.deltaTime;
        }
    }
}
