using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeExpression : MonoBehaviour
{
    public float waitTime;
    private float waitTimeCounter;
    public Sprite[] expresions;
    public bool canChangeExpression = true;
    SpriteRenderer sr;
    void Start()
    {
        waitTimeCounter = waitTime;
        sr = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        waitTimeCounter -= Time.deltaTime;
        if (waitTimeCounter < 0 && canChangeExpression) 
        {
            changeExpression();
        }
    }

    public void changeExpression() 
    {
        int selector = Random.Range(0, expresions.Length);
        Sprite chosenSprite = expresions[selector];
        sr.sprite = chosenSprite;
        canChangeExpression = false;

    }
}
