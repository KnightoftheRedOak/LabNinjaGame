using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShinobiController : MonoBehaviour
{
    Rigidbody2D rb;
    private bool isMouseButtonPressed;
    private Vector3 startPosition;
    private Vector3 endPosition;
    Camera mainCamera;
    public float speed = 5f; // Adjust this value to control the movement speed
    public bool isGrounded, isOnPlate, correctPlate, isRunning;
    public LayerMask whatIsGround, whatIsPlate;
    public float surfaceAreaForGrounded, surfaceAreaForPlateSensor, throwDistance;
    public Transform jumpingPoint, plateSensorPoint;
    public Animator anim;
    //public bool canJumpAnimations, JumpA, AirRoll;
    public static ShinobiController instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        mainCamera = Camera.main;
        


    }

    void Update()
    {
        
        isGrounded = Physics2D.OverlapCircle(jumpingPoint.transform.position, surfaceAreaForGrounded, whatIsGround);
        isOnPlate = Physics2D.OverlapCircle(plateSensorPoint.transform.position, surfaceAreaForPlateSensor, whatIsPlate);
       

       
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        
        if (Input.GetMouseButton(0))
        {
            isMouseButtonPressed = true;
            anim.SetBool("isFalling", false);
            endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (isMouseButtonPressed)
            {
                // Calculate the normalized distance between start and end positions
               // float distance = Vector2.Distance(startPosition, endPosition);

                // Move the rigidbody towards the end position with adjusted speed
                Vector2 newPosition = Vector2.MoveTowards(rb.position, endPosition, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPosition);


                // Check if the sprite has reached the target position
                if (Vector2.Distance(rb.position, endPosition) < 0.05f)
                {
                    isMouseButtonPressed = false; // Stop the movement
                    


                }

              
            }

           

            if (correctPlate) 
            {
                if (Vector2.Distance(transform.position, Blade.instance.transform.position) > throwDistance)
                {
                    anim.SetTrigger("Throw");
                }
                
            }

           
        }
                
        else if (Input.GetMouseButtonUp(0))
        {
            isMouseButtonPressed = false;
            anim.SetBool("isFalling", true);
           
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -7.5f, 7.5f), Mathf.Clamp(transform.position.y, -2.6f, 4), transform.position.z);

        if (mousePos.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1f, 1f);
        }
        else if (mousePos.x > transform.position.x) 
        {
            transform.localScale = Vector3.one;
        }

        // Debug.Log(rb.velocity.x);



        else 
        {
            anim.SetBool("isRunning", false);
        }

       


        anim.SetBool("isGrounded",isGrounded);


    }


    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == Spawner.instance.plateToBeSliced.gameObject.tag && isMouseButtonPressed) 
        {
            anim.SetTrigger("isSlicing");
            Debug.Log("attack plate!");
            other.GetComponent<Fruit>().Slice();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(jumpingPoint.transform.position, surfaceAreaForGrounded);
        Gizmos.DrawWireSphere(plateSensorPoint.transform.position, surfaceAreaForPlateSensor);
    }

   /* public void canJump() 
    {
        int jumpPickerAnimation = Random.Range(0, 2);
        Debug.Log(jumpPickerAnimation);
        switch (jumpPickerAnimation)
        {
            case 0:
                JumpA = true;
                break;
            case 1:

                AirRoll = true;
                break;
        }
        canJumpAnimations = false;
    }*/



















}
