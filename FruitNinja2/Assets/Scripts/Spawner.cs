using System.Collections;
using UnityEngine;




public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    private BoxCollider2D spawnArea;
    public GameObject[] fruitPreFabs;
    public GameObject plateToBeSliced;
    public GameObject bombPreFab;
    public GameObject spawner2, spawner3;
    public float frenzyTime;
    private float frenzyCounter;


    [Range(0f,1f)]
    public float bombChance = 0.7f;
    public int chosenPlateChance = 4;
   
    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = .45f;

    public float minAngle = -15f;
    public float maxAngle = 15f;

    public float minForce = 18f;
    public float maxForce = 22f;

    public float maxLifeTime = 5f;


    private void Awake()
    {
        instance = this;
        spawnArea = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        Invoke("startSpawningFruitGroups",1f);
        
    }

    private void Update()
    {
        if (frenzyCounter > 0) 
        {
            frenzyCounter -= Time.deltaTime;
            if (frenzyCounter <= 0) 
            {
                spawner2.SetActive(false);
                spawner3.SetActive(false);
            }
        }
    }

    public void startSpawningFruitGroups() 
    {
        InvokeRepeating("spawnFruitGroups", 1f, 8f);
    }

    public void spawnFruitGroups() 
    {
        chosenPlates();
        StartCoroutine("Spawn");
    }

    public void chosenPlates() 
    {
        int randomPlate = Random.Range(0, fruitPreFabs.Length);
        plateToBeSliced = fruitPreFabs[randomPlate];
        //call plateToBeSlicedSetChosenPlateImageLocation
        GameManager.instance.chosenPlateImage.sprite = plateToBeSliced.GetComponent<SpriteRenderer>().sprite;
        GameManager.instance.chosenPlateText.text = plateToBeSliced.tag;
        
    }

    public void startFrenzy() //call startFrenzy from magic urine??
    {
        frenzyCounter = frenzyTime;
        spawner2.SetActive(true);
        spawner3.SetActive(true);
    }
        

    private IEnumerator Spawn() 
    {
        for (int i = 0; i < 7; i++) 
        {
            GameObject prefab = fruitPreFabs[Random.Range(0, fruitPreFabs.Length)];
            int randomValue = Random.Range(0, 10);
           // Debug.Log(randomValue);

            if (Random.value < bombChance)
            {
                prefab = bombPreFab;
               
            }
            if ( randomValue < chosenPlateChance) 
            {
                prefab = plateToBeSliced;
                //Debug.Log("Rigged plate being spawned");
            }
            Vector3 position = new Vector3();
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            position.z = transform.position.z;

            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));

            GameObject fruit = Instantiate(prefab, position, rotation);
            Destroy(fruit, maxLifeTime);
            float force = Random.Range(minForce, maxForce);
            fruit.GetComponent<Rigidbody2D>().AddForce(fruit.transform.up * force, ForceMode2D.Impulse);

            

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }

        
       
           
        
    }
}
