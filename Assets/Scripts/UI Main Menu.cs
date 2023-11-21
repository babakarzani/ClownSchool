using System.Collections;
using System.Collections.Generic;
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


    public static UIMainMenu mainMenu;
  

    private void Awake()
    {
        mainMenu = this;
    }
    private void Start()
    {
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
    }


    //the two following functions is to slide down and up the UI menu
    public void OpenMenu()
    {
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
        Shopbehaviour.main.Announcments("");

        mainMenuAnimator.SetBool("IsOpen", true);
        
    }

    public void CloseMenu()
    {
        Shopbehaviour.main.Announcments("");
        Cursor.SetCursor(default, Vector2.zero,CursorMode.ForceSoftware);

        mainMenuAnimator.SetBool("IsOpen", false);

    }

    public void ChangeMouseToCannon()
    {
        
        Cursor.SetCursor(cannonTexture, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void ChangeMouseToCatapult()
    {
        
        Cursor.SetCursor(catapultTexture, Vector2.zero, CursorMode.ForceSoftware);
    }
}
