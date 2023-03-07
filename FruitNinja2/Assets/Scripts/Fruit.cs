using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject whole, c1,c2;
    public GameObject sliced;

    private Rigidbody2D fruitRigidBody;
    private Collider2D fruitColider;

    public GameObject impactEffect;

    public int canSpawnChance;
    private int rateOfChance = 5;//change this number to increase or decrease chance of bacteria spawning
    public bool canSpawn;

    private void Awake()
    {
        fruitRigidBody = GetComponent<Rigidbody2D>();
        fruitColider = GetComponent<Collider2D>();
    }

    private void Slice() 
    {
        GameManager.instance.IncreaseScore();
        sliced.transform.parent = null;
        sliced.SetActive(true);
        whole.SetActive(false);
        Instantiate(impactEffect, transform.position, transform.rotation);


        c1.GetComponent<Rigidbody2D>().AddForce(new Vector2(2f, 2f), ForceMode2D.Impulse);
        c1.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-2f, 2f), ForceMode2D.Impulse);

        c2.GetComponent<Rigidbody2D>().AddForce(new Vector2(-2f, 2f), ForceMode2D.Impulse);
        c2.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-2f, 2f), ForceMode2D.Impulse);





    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") 
        {
            if (Spawner.instance.plateToBeSliced.tag == gameObject.tag)
            {
                Slice();

                IdentifyPlateType();
               
            }
            else 
            {
               // Debug.Log("Wrong Fruit");
               // Take away from Players health here(sprite character)
            }

            
        }
    }
           
    public void IdentifyPlateType() 
    {
        canSpawnChance = Random.Range(1, 10);
        Debug.Log(canSpawnChance);

        if (canSpawnChance <= rateOfChance)
        {
            canSpawn = true;
        }
        else 
        {
            canSpawn = false;
        }
        switch (gameObject.tag)
        {
            case "BloodPlate":
                if (canSpawn) 
                {
                    Debug.Log("Hitting bloodPlate can spawn pink worm");
                }
                break;
            case "Chocolate":
                Debug.Log("Hitting Chocolate Plate");
                break;
            case "CNA":
                Debug.Log("Hitting  CNA Plate");
                break;
            case "Mac":
                Debug.Log("Hitting Mac Plate");
                break;
            default:
                Debug.Log("Unknown material being hit");
                break;
        }
    }
}
            

               
               

