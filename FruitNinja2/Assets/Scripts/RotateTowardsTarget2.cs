using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

public class RotateTowardsTarget2 : MonoBehaviour
{
    public float moveSpeed, changeDirectionTime;
    private float changeDirectionCounter;
    public Vector2 direction;
    public float turnSpeed;
    
    void Start()
    {
        
    }

   
    void Update()
    {
        if (changeDirectionCounter < 0)
        {
            changeDirection();
            changeDirectionCounter = changeDirectionTime;
        }
        else 
        {
            changeDirectionCounter -= Time.deltaTime;

        }

        // transform.position = Vector3.MoveTowards(transform.position, direction, moveSpeed * Time.deltaTime);
        //transform.up = new Vector3(direction.x - transform.position.x, direction.y - transform.position.y, 0);

        Vector3 amountToMove = new Vector3(direction.x, direction.y, 0) - transform.position;
        float angle = Mathf.Atan2(amountToMove.y, amountToMove.x) * Mathf.Rad2Deg;
        Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);

        transform.position += transform.up * moveSpeed * Time.deltaTime;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -7, 7), Mathf.Clamp(transform.position.y, -3, 3));

       

    }

    public void changeDirection() 
    {
        direction = new Vector2(Random.Range(-3, 3), Random.Range(-3, 3));
    }
}
