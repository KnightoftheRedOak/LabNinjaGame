
using JetBrains.Annotations;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private CapsuleCollider2D bladeCollider;
    private bool slicing;
    ParticleSystem trailOne, trailTwo;
    private Camera mainCamera;
    public Vector3 direction;
    public float sliceForce = 15f;
    public float minSliceVelocity = 0.01f;
    public static Blade instance;
    private void Awake()
    {
        instance = this;
        mainCamera = Camera.main;
        bladeCollider = GetComponent<CapsuleCollider2D>();
        trailOne = GetComponentInChildren<ParticleSystem>();
        trailTwo = GetComponentInChildren<ParticleSystem>();
    }

    private void OnEnable()
    {
        StopSlicing();
    }

    private void OnDisable()
    {
        StopSlicing();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }
        else if(slicing)
        {
            ContinueSlicing();
        }
    }

    private void StartSlicing() 
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;
        transform.position = newPosition;

        slicing = true;
        bladeCollider.enabled = true;
        trailOne.Play();
        trailTwo.Play();

        trailOne.Clear();
        trailTwo.Clear();
        
        
        

        

        //Debug.Log("slicing has started");
    }

    private void StopSlicing() 
    {
        slicing = false;
        bladeCollider.enabled = false;
        trailOne.Stop();
        trailTwo.Stop();

       


       // Debug.Log("Slicing has stoped");

    }



    private void ContinueSlicing() 
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        direction = newPosition - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;
        bladeCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition;
        
    }

   

    
}
