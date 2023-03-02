using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Text chosenPlateText;
    public Image chosenPlateImage;
    public int score;

    private Blade blade;
    private Spawner spawner;

    public static GameManager instance;

   

    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawner>();
        instance = this;
    }

   

    private void Start()
    {
        NewGame();
    }

    public void IncreaseScore() 
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void NewGame() 
    {
        score = 0;
        scoreText.text = score.ToString();
    }

    public void explode() 
    {
        // blade.enabled = false;
        //spawner.enabled = false;
        Debug.Log("You have hit the bomb!");
    }
}
