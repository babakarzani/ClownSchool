using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlotMechanics : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private Color hoverColor;
    


    private GameObject tower;
    private Color startColor;

    private void Start()
    {
        startColor = m_spriteRenderer.color;
    }

    private void OnMouseEnter()
    {
        if (BusSpawner.isSpawning) return;
        m_spriteRenderer.color = hoverColor;
    }

    private void OnMouseExit()
    {
        if (BusSpawner.isSpawning) return;
        m_spriteRenderer.color = startColor;
    }

    private void OnMouseDown()
    {
        //if it is occupied already
        if (tower != null) return;
        //if player has enough money

        if (BusSpawner.isSpawning) return;

        m_spriteRenderer.color = startColor;
        ChooseTower typeOfTowerToBuild = BuildManager.main.GetSelectedTower();
        if (typeOfTowerToBuild.cost > LevelManager.main.currency)
        {
            Shopbehaviour.main.Announcments("You have no money, walrus face cheesburger eating clown!");
            return;
        }
        LevelManager.main.SpendCurrency(typeOfTowerToBuild.cost);
        tower = Instantiate(typeOfTowerToBuild.prefab, transform.position, Quaternion.identity);
        Shopbehaviour.main.Announcments("");

    }

    

}
