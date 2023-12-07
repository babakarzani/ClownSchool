using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuFunctions : MonoBehaviour
{
    [Header("References")]
    [SerializeField] AudioSource playAudioSource;
    [SerializeField] AudioSource quitAudioSource;
    [SerializeField] CanvasGroup canvasToFade;
    //Start of the game
    public void StartGame()
    {
        playAudioSource.Play();
        StartCoroutine(CanvasFade());
        
    }

    public void QuitGame()
    {
        
        Application.Quit();
    }

    IEnumerator CanvasFade()
    {
        
        for (float alpha = 1f; alpha >= 0; alpha -= 0.2f)
        {
           
            canvasToFade.alpha = alpha;
            yield return new WaitForSeconds(0.1f);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
