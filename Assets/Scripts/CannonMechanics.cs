using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CannonMechanics : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject spriteToRotate;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private Transform firingPoint;

    [Header("Attributes")]
    [SerializeField] private float towerRange = 2f;
    [SerializeField] private float towerRotationSpeed = 300f;
    
    [SerializeField] private float fireInterval = 2f;


    private float Intervaltimer;
    private Transform enemyLocation;

    private void Start()
    {
        Intervaltimer = fireInterval;
    }

    private void Update()
    {
        if(enemyLocation==null)
        {
            FindEnemy();
            return;
        }
        

        RotateTowardsTarget();

        if (!EnemyOutofRange()) //checks if enemy is out of range
        {
            enemyLocation = null;
            
        }
        else
        {
            Intervaltimer += Time.deltaTime;
            if (Intervaltimer>=fireInterval)
            {
                ShootEnemy();
                Intervaltimer = 0;
            }
        }
           
        
    }

    private void FindEnemy()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, towerRange, (Vector2)transform.position, 0, enemyMask);
        if (hits.Length>0)
        {
            enemyLocation = hits[0].transform;
        }
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(spriteToRotate.transform.position.y - enemyLocation.position.y,  spriteToRotate.transform.position.x - enemyLocation.position.x) * Mathf.Rad2Deg +90f;
        Quaternion enemyRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        spriteToRotate.transform.rotation = Quaternion.RotateTowards(spriteToRotate.transform.rotation, enemyRotation, towerRotationSpeed*Time.deltaTime);
    }

    private bool EnemyOutofRange()
    {
        if (Vector2.Distance(enemyLocation.position, transform.position) > towerRange)
        {
            return(false);
        }
        return true;

    }

    private void ShootEnemy()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        BulletMechanics bulletScript = bulletObj.GetComponent<BulletMechanics>();
        bulletScript.SetEnemyLocation(enemyLocation);
    }

    //just for us to see
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, transform.forward, towerRange);
    }


}
