using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shopbehaviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currency;
    [SerializeField] private TMP_Text announcementText;

    public static Shopbehaviour main;

    private void Awake()
    {
        main = this;
    }

    public void Announcments(string _announcment)
    {
        announcementText.text = _announcment;
    }    

    private void OnGUI()
    {
        currency.text = LevelManager.main.currency.ToString();
    }
}
