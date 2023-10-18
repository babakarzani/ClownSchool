using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CannonMechanics : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject spriteToRotate;

    [Header("Attributes")]
    [SerializeField] private float towerRange = 2f;
    [SerializeField] private float towerRotationSpeed = 300f;
    [SerializeField] private LayerMask enemyMask;

    private Transform enemyLocation;

    private void Update()
    {
        if(enemyLocation==null)
        {
            FindEnemy();
        }
        else
        {
            RotateTowardsTarget();
            EnemyOutofRange();
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

    private void EnemyOutofRange()
    {
        if (Vector2.Distance(enemyLocation.position, transform.position) > towerRange)
        {
            enemyLocation = null;
        }

    }

    //just for us to see
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, transform.forward, towerRange);
    }


}
