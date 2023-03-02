using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToTarget : MonoBehaviour
{
    private float WaitTimeCounter, RotationCounter;
    public float timeForWaitTimeCounter, timeForRotationCounter;
    public bool canMove;
   
    public float rotationSpeed;
    public float moveSpeed;
    public float x, y;
    Vector2 chosenDirecton, direction;
    void Start()
    {
        choseDirection();
        RotationCounter = timeForRotationCounter;
    }

   
    void Update()
    {
        if (!canMove)
        {

            if (RotationCounter > 0)
            {
                RotationCounter -= Time.deltaTime;
                if (RotationCounter <= 0)
                {
                    direction = Camera.main.ScreenToWorldPoint(chosenDirecton) - transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle,new Vector3(0,0,angle));
                    
                    WaitTimeCounter = timeForWaitTimeCounter;
                }
            }
            else
            {
                if (WaitTimeCounter > 0)
                {
                    WaitTimeCounter -= Time.deltaTime;
                    if (WaitTimeCounter <= 0)
                    {
                        canMove = true;
                    }

                }
            }
        }
        else 
        {
            Vector2 pointToMoveTowards = chosenDirecton;
            transform.up = new Vector3(pointToMoveTowards.x - transform.position.x, pointToMoveTowards.y - transform.position.y, 0);
            transform.position = Vector2.MoveTowards(transform.position, pointToMoveTowards, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, pointToMoveTowards) < .2f) 
            {
                choseDirection();
                canMove = false;
                RotationCounter = timeForRotationCounter;
            }
        }
        


        
    }

    public void movement()
    {
        Vector2 pointToMoveTowards = chosenDirecton;
        transform.position = Vector2.MoveTowards(transform.position, pointToMoveTowards, moveSpeed * Time.deltaTime);
    }

    public void choseDirection() 
    {
        chosenDirecton = new Vector2(Random.Range(-7, 7), Random.Range(-3, 3));
    }

    public void rotationBlackThorny() 
    {
       // Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
       // transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }


}




