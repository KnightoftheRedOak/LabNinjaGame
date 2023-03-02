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
            }
            else 
            {
               // Debug.Log("Wrong Fruit");
            }
            
            
        }
    }
}
