using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShinobiController2 : MonoBehaviour
{
    
    
    private Rigidbody2D rb;
    private bool isMouseButtonPressed = false;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
       

public class SpriteMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isMouseButtonPressed = false;
    private Vector2 targetPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMouseButtonPressed = true;

            // Get the target position in world coordinates
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMouseButtonPressed = false;
        }
    }

    void FixedUpdate()
    {
        if (isMouseButtonPressed)
        {
            // Calculate the direction to the target position
            Vector2 direction = targetPosition - rb.position;

            // Normalize the direction and calculate the step size
            direction.Normalize();
            float step = 5f * Time.fixedDeltaTime;

            // Move the sprite towards the target position with the calculated step size
            rb.MovePosition(rb.position + direction * step);
        }
    }
}

    


}
