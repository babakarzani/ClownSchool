using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;
    [Header("References")]
    [SerializeField] public ChooseTower[] towers;

    private int selectedTower = 0;

    private void Awake()
    {
        main = this;
    }

    public ChooseTower GetSelectedTower()
    {
        return towers[selectedTower];
    }

    //Default selected tower is 0, i.e. cannon. UI will call this function
    public void SetSelectedTower(int _selectedTower)
    {
        selectedTower = _selectedTower;
    }    
}
