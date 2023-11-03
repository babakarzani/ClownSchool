using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusHealth : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int health=2;
    [SerializeField] private int worth = 700;

    private bool isDestroyed = false;

    public void TakeDamage(int _damage)
    {
        health -= _damage;
        if(health<=0 && !isDestroyed)
        {
            //call this even on Bus spawner
            BusSpawner.onBusDestroyed.Invoke();
            isDestroyed = true;
            LevelManager.main.IncreaseCurrency(worth);
            Destroy(gameObject);
        }    
    }    
}
