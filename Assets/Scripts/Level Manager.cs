using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    [Header("Attribitues")]
    [SerializeField] public int currency=2899;

    // start point and path for enemy Bus
    public Transform BusstartPoint;
    public Transform[] busPath;

    

    // start point and path for enemy clown car
    
    public Transform schoolstartPoint;
    public Transform[] carPath;

    private void Awake()
    {
        main = this;
    }

    public void IncreaseCurrency(int _amount)
    {
        currency += _amount;
    }

    public bool SpendCurrency(int _amount)
    {
        if(_amount<= currency)
        {
            currency -= _amount;

            return true;
        }
        else
        {
            Debug.Log("not enough currency");
            return false;
        }
    }
}
