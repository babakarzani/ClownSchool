using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator mainMenuAnimator;
    [SerializeField] Button cannonButton;
    [SerializeField] Button catapultButton;
    [SerializeField] Texture2D cannonTexture;
    [SerializeField] Texture2D catapultTexture;
    [SerializeField] public TextMeshProUGUI cannonNumberstxt;
    [SerializeField] public int cannonNumbers;
    [SerializeField] public TextMeshProUGUI catapultNumberstxt;
    [SerializeField] public int catapultNumbers;
    [SerializeField] TextMeshProUGUI timerTxt;
    [SerializeField] Animator waveAnimator;


    public static UIMainMenu mainMenu;
    float timeLeft;
    int timeInSecond;
    int waveNumber;
  

    private void Awake()
    {
        mainMenu = this;
    }
    private void Start()
    {
        waveNumber = 1;
        catapultNumberstxt.text = catapultNumbers.ToString() + "x";
        cannonNumberstxt.text = cannonNumbers.ToString() + "x";
        if (LevelManager.main.currency < BuildManager.main.towers[0].cost)
        {
            cannonButton.interactable = false;
        }
        else
        {
            cannonButton.interactable = true;
            Cursor.SetCursor(cannonTexture, Vector2.zero, CursorMode.ForceSoftware);
        }
        if (LevelManager.main.currency < BuildManager.main.towers[1].cost)
        {
            catapultButton.interactable = false;
        }
        else catapultButton.interactable = true;
        timerTxt.gameObject.SetActive(true);
        timeLeft = BusSpawner.mainMenu.timeBetweenWaves+1;
    }

    private void Update()
    {
        if (timeLeft <= 1)
            return;
        if (timeLeft < 6)
            timerTxt.color = Color.red;
        timeLeft -= Time.deltaTime;
        timeInSecond = Mathf.FloorToInt(timeLeft % 60);
        timerTxt.text = string.Format("{00:00}", timeInSecond);
    }
    //the two following functions is to slide down and up the UI menu
    public void OpenMenu()
    {
        waveNumber++;
        waveAnimator.SetInteger("WaveNmb", waveNumber);
        timeLeft = BusSpawner.mainMenu.timeBetweenWaves + 1;
        timerTxt.color = Color.white;
        if (LevelManager.main.currency < BuildManager.main.towers[0].cost || cannonNumbers <= 0)
        {
            cannonButton.interactable = false;
        }
        else
        {
            cannonButton.interactable = true;
            Cursor.SetCursor(cannonTexture, Vector2.zero, CursorMode.ForceSoftware);
        }
        if (LevelManager.main.currency < BuildManager.main.towers[1].cost || catapultNumbers <= 0)
        {
            catapultButton.interactable = false;
        }
        else catapultButton.interactable = true;
        Shopbehaviour.main.Announcments("");
        timerTxt.gameObject.SetActive(true);

        mainMenuAnimator.SetBool("IsOpen", true);
        
    }

    public void CloseMenu()
    {
        Shopbehaviour.main.Announcments("");
        Cursor.SetCursor(default, Vector2.zero,CursorMode.ForceSoftware);

        mainMenuAnimator.SetBool("IsOpen", false);
        timerTxt.gameObject.SetActive(false);


    }

    public void ChangeMouseToCannon()
    {
        
        Cursor.SetCursor(cannonTexture, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void ChangeMouseToCatapult()
    {
        
        Cursor.SetCursor(catapultTexture, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void UpdateUIAfterPurchase(string _towername)
    {
        if (_towername == "Cannon")
        {
           
            cannonNumbers--;
            cannonNumberstxt.text = cannonNumbers.ToString() + "x";
            if (LevelManager.main.currency < BuildManager.main.towers[0].cost || cannonNumbers <= 0)
            {
                cannonButton.interactable = false;
            }
            if (LevelManager.main.currency < BuildManager.main.towers[1].cost || catapultNumbers <= 0)
            {
                catapultButton.interactable = false;
            }
        }
        if (_towername=="Catapult")
        {
            catapultNumbers--;
            catapultNumberstxt.text = catapultNumbers.ToString() + "x";
            if (LevelManager.main.currency < BuildManager.main.towers[1].cost || catapultNumbers <= 0)
            {
                catapultButton.interactable = false;
            }
            if (LevelManager.main.currency < BuildManager.main.towers[0].cost || cannonNumbers <= 0)
            {
                cannonButton.interactable = false;
            }
        }

    }

    
}
