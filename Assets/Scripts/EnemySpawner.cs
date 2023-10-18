using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BusSpawner : MonoBehaviour
{
    //For future when there are more bus types
    [Header("References")]
    [SerializeField] private GameObject[] busPrefabs;
    [SerializeField] private GameObject[] clownCarPrefabs;

    [Header("Attributes")]
    [SerializeField] private int[] numberOfEnemies= new int[5];
    [SerializeField] private float timeBetweenEnemies = 2f;
    [SerializeField] private float timeBetweenWaves = 5f;

    //call level manager to say a bus reached the school
    [Header("Events")]
    public static UnityEvent onBusReachingSchool = new UnityEvent();

    //call level manager to say a car reached the hospital
    public static UnityEvent onCarReachingHospital = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private int clownCarsLeft;
    private bool isSpawning = false;

    private void Awake()
    {
        //Call journey from school to hostpital everytime Bus reaches school
        onBusReachingSchool.AddListener(startSchooltoHospital);
        onCarReachingHospital.AddListener(endGame);
    }
    private void Start()
    {
       StartCoroutine(startWave());
    }

    private void Update()
    {
        if (!isSpawning)
            return;
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn>timeBetweenEnemies && enemiesLeftToSpawn>0)
        {
            SpawnEnemies();
            enemiesAlive++;
            enemiesLeftToSpawn--;
            timeSinceLastSpawn = 0;
        }

        if (enemiesLeftToSpawn == 0 && clownCarsLeft == 0 && enemiesAlive==0)
            endWave();

    }

    private IEnumerator startWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        enemiesLeftToSpawn = numberOfEnemies[currentWave - 1];
        isSpawning = true;
    }

    private void SpawnEnemies()
    {
        //later we can change this numnber for other enemies
        GameObject prefabToSpawn = busPrefabs[0];
        Instantiate(prefabToSpawn, LevelManager.main.BusstartPoint.position, Quaternion.identity);
    }

    private void startSchooltoHospital()
    {
        clownCarsLeft++;
        
        GameObject prefabToSpawn = clownCarPrefabs[0];
        Instantiate(prefabToSpawn, LevelManager.main.schoolstartPoint.position, Quaternion.identity);
    }
    //for now endgame is a replacement for clowncar destroyed
    private void endGame()
    {
        //for now
        enemiesAlive--;
        clownCarsLeft--;
    }

    //ends current wave and calls the next one
    private void endWave()
    {
        timeSinceLastSpawn = 0;
        isSpawning = false;
        currentWave++;
        if (currentWave <= 5)
            StartCoroutine(startWave());

    }    

}
