using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

public class RotateTowardsTarget2 : MonoBehaviour
{
    public float moveSpeed, changeDirectionTime;
    private float changeDirectionCounter;
    public Vector2 direction;
    
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

        transform.position = Vector3.MoveTowards(transform.position, direction, moveSpeed * Time.deltaTime);
        transform.up = new Vector3(direction.x - transform.position.x, direction.y - transform.position.y, 0);

    }

    public void changeDirection() 
    {
        direction = new Vector2(Random.Range(-7, 7), Random.Range(-3, 3));
    }
}
