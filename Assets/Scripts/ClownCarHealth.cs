using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownCarHealth : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int health = 1;
    [SerializeField] private int worth = 400;

    private bool isDestroyed = false;

    public void TakeDamage(int _damage)
    {
        health -= _damage;
        if (health <= 0 && !isDestroyed)
        {
            //call this even on Bus spawner
            BusSpawner.onClownCarDestroyed.Invoke();
            isDestroyed = true;
            LevelManager.main.IncreaseCurrency(worth);
            Destroy(gameObject);
        }
    }
}
