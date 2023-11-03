using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator mainMenuAnimator;

    public static UIMainMenu mainMenu;

    private void Awake()
    {
        mainMenu = this;
    }
    //the two following functions is to slide down and up the UI menu
    public void OpenMenu()
    {
        
        mainMenuAnimator.SetBool("IsOpen", true);
        Shopbehaviour.main.Announcments("");
    }

    public void CloseMenu()
    {
        
        mainMenuAnimator.SetBool("IsOpen", false);
        
    }

    
}
