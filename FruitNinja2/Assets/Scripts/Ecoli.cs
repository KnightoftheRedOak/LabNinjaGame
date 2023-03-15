using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ecoli : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector2 movement;
    public float moveSpeed;
    public float moveTime, waitTime;
    public bool canMoveDown = true;
    private float moveCounter, waitCounter;
    private int maxHealth = 3;
    public int currentHealth;
    public GameObject explosion;
    public GameObject obstacleRayObjectX;
    public GameObject obstacleRayObjectY;
    public float characterDirectionX;
    public float characterDirectionY;
    public float obstacleRayDistance;
    public bool canChangeDirection;
    public LayerMask whatIsEcoli;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        waitCounter = waitTime;
        currentHealth = maxHealth;
       
    }

   
    void Update()
    {
       /* if (rb.velocity.x < 0)
        {
            characterDirectionX = -1f;
        }
        else if (rb.velocity.x > 0)
        {
            characterDirectionX = 1f;
        }
        else 
        {
            characterDirectionX = 0;
        }

        if (rb.velocity.y < 0)
        {
            characterDirectionY = -1f;
        }
        else if (rb.velocity.y > 0)
        {
            characterDirectionY = -1f;
        }
        else 
        {
            characterDirectionY = 0;
        }

        RaycastHit2D hitobsticleX =  Physics2D.Raycast(obstacleRayObjectX.transform.position,Vector2.right * new Vector2(characterDirectionX,0f),whatIsEcoli);
        RaycastHit2D hitobsticleY = Physics2D.Raycast(obstacleRayObjectY.transform.position, Vector2.up * new Vector2(characterDirectionY, 0f), whatIsEcoli);
        
        if (hitobsticleX.collider != null) 
        {
            Debug.Log("other e.coli encountered");
            canChangeDirection = true;
           
            
            if (canChangeDirection) 
            { 
                newDirection();
            }
            
           
        }

        if (hitobsticleY.collider != null)
        {
            Debug.Log("other e.coli encountered");
            canChangeDirection = true;
           

            if (canChangeDirection)
            {
                newDirection();
            }


        }*/


        if (waitCounter > 0)
        {
            waitCounter -= Time.deltaTime;
            rb.velocity = Vector3.zero;
            


            if (waitCounter <= 0)
            {
                moveCounter = Random.Range(3f,7f);
                movement = new Vector2(Random.Range(-6, 6), Random.Range(-6, 6)).normalized;
            }
        }
        else 
        {
            if (moveCounter > 0) 
            {
                moveCounter -= Time.deltaTime;
                rb.velocity = movement * moveSpeed;
                if (moveCounter <= 0) 
                {
                    waitCounter = waitTime;
                }
            }
        
        }

        if (rb.velocity.x < 0) 
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (rb.velocity.x > 0) 
        {
            transform.localScale = Vector3.one;
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -7f,7f),Mathf.Clamp(transform.position.y, -3f, 3f), transform.position.z);

       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.tag == "Ecoli")
        {
            newDirection();
            //Debug.Log("Hitting other Ecoli");
        }

        if (other.tag == "Player")
        {
            currentHealth--;
            if (currentHealth <= 0)
            {
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    public void newDirection() 
    {
        movement = new Vector2(Random.Range(-6, 6), Random.Range(-6, 6)).normalized;
        //canChangeDirection = false;
    }

    
}
