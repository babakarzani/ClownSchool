using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        m_spriteRenderer.color = hoverColor;
    }

    private void OnMouseExit()
    {
        m_spriteRenderer.color = startColor;
    }

    private void OnMouseDown()
    {
        //in future we can use upgrade or sell to this if
        if (tower != null) return;
        m_spriteRenderer.color = startColor;
        GameObject typeOfTowerToBuild = BuildManager.main.GetSelectedTower();
        tower = Instantiate(typeOfTowerToBuild, transform.position, Quaternion.identity);
    }
}
