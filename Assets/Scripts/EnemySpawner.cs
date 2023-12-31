using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.LookDev;

public class BusSpawner : MonoBehaviour
{
    //For future when there are more bus types
    [Header("References")]
    [SerializeField] private GameObject[] busPrefabs;
    [SerializeField] private GameObject[] clownCarPrefabs;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;

    [Header("Attributes")]
    [SerializeField] private int[] numberOfEnemies= new int[5];
    [SerializeField] private float timeBetweenEnemies = 2f;
    [SerializeField] public float timeBetweenWaves = 5f;

    //call level manager to say a bus reached the school
    [Header("Events")]
    public static UnityEvent onBusReachingSchool = new UnityEvent();

    //call level manager to say a car reached the hospital
    public static UnityEvent onCarReachingHospital = new UnityEvent();

    //call level manager to say a car reached the hospital
    public static UnityEvent onBusDestroyed = new UnityEvent();

    //call level manager to say a clown car is destroyed
    public static UnityEvent onClownCarDestroyed = new UnityEvent();

    public static BusSpawner mainMenu;

    //Enemy means Bus
    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private int clownCarsLeft;
    public static bool isSpawning = false;

    private void Awake()
    {
        mainMenu = this;
        currentWave = 1;
        timeSinceLastSpawn = 0;
        enemiesAlive = 0;
        enemiesLeftToSpawn =0;
        clownCarsLeft=0;
        isSpawning = false;
    //Enemy bus movement calls this
    onBusReachingSchool.AddListener(startSchooltoHospital);
        //Enemy clown car movement calls this
        onCarReachingHospital.AddListener(endGame);
        //Enemy Health Function calls this
        onBusDestroyed.AddListener(DestroyBus);
        //clown car health function calls this
        onClownCarDestroyed.AddListener(DestroyClownCar);

        

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
        UIMainMenu.mainMenu.CloseMenu();
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
        Instantiate(prefabToSpawn, LevelManager.main.schoolstartPoint.position, LevelManager.main.schoolstartPoint.rotation);
    }
    
    private void endGame()
    {
        
        enemiesAlive--;
        clownCarsLeft--;
        Cursor.SetCursor(default, Vector2.zero, CursorMode.ForceSoftware);
        losePanel.SetActive(true);
        StartCoroutine(CanvasLossGame());
    }


    private void DestroyBus()
    {
        enemiesAlive--;
    }

    private void DestroyClownCar()

    {
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
        {
            UIMainMenu.mainMenu.OpenMenu();
            StartCoroutine(startWave());

        }
        else
            WinGame();

    }

    IEnumerator CanvasLossGame()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
    }

    private void WinGame()
    {
        Cursor.SetCursor(default, Vector2.zero, CursorMode.ForceSoftware);
        winPanel.SetActive(true);
        StartCoroutine(CanvasLossGame());
    }

}
