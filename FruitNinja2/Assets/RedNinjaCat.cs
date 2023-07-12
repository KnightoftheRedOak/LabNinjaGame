using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedNinjaCat : MonoBehaviour
{
   
  


    public float moveSpeed = 5f; // Speed at which the sprite moves
    public float avoidanceDistance = 1f; // Distance to start avoiding obstacles
    public LayerMask obstacleLayer; // Layer mask for the obstacles

    private Vector3[] pathPoints; // Array to store the mouse path
    private int currentPointIndex = 0; // Index of the current path point
    private Rigidbody2D rb; // Reference to the Rigidbody2D component

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // Reset the path and start a new one when the left mouse button is clicked
            ResetPath();
            AddPointToPath(GetMouseWorldPosition());
        }

        if (Input.GetMouseButton(0))
        {
            // Continuously add points to the path while the left mouse button is held down
            AddPointToPath(GetMouseWorldPosition());
        }

        if (currentPointIndex < pathPoints.Length)
        {
            // Move towards the current path point
            MoveTowards(pathPoints[currentPointIndex]);
        }
    }

    private void MoveTowards(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        float distance = direction.magnitude;

        if (distance <= avoidanceDistance)
        {
            // Check for obstacles to avoid
            Collider2D obstacle = Physics2D.OverlapCircle(transform.position, avoidanceDistance, obstacleLayer);
            if (obstacle != null)
            {
                // Calculate a new direction to avoid the obstacle
                Vector3 avoidanceDirection = transform.position - obstacle.transform.position;
                direction += avoidanceDirection.normalized * moveSpeed;
            }
        }

        if (distance <= moveSpeed * Time.deltaTime)
        {
            // Reached the current path point, move to the next one
            currentPointIndex++;
        }
        else
        {
            // Move towards the current path point using the Rigidbody2D component
            rb.velocity = direction.normalized * moveSpeed;
        }
    }

    private void ResetPath()
    {
        pathPoints = new Vector3[0];
        currentPointIndex = 0;
        rb.velocity = Vector2.zero;
    }

    private void AddPointToPath(Vector3 point)
    {
        // Add a new point to the path array
        System.Array.Resize(ref pathPoints, pathPoints.Length + 1);
        pathPoints[pathPoints.Length - 1] = point;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }


}
