using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D m_rigidbody;

    [Header("Attributes")]
    [SerializeField] private float enemySpeed = 2f;

    //where the enemy is going
    private Transform target;
    private int targetIndex = 0;
    Vector2 direction;

    //first point
    private void Start()
    {
        target = LevelManager.main.busPath[targetIndex];
    }

    //update the next position

    private void Update()
    {
        if(Vector2.Distance(target.position, transform.position)<=0.1f)
        {
            

            if (targetIndex > LevelManager.main.busPath.Length)
            {
                BusSpawner.onBusReachingSchool.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                targetIndex++;
                target = LevelManager.main.busPath[targetIndex];
              
            }
        }

    }

    //move and rotate to the next position
    private void FixedUpdate()
    {
        direction = (target.position - transform.position).normalized;
        m_rigidbody.velocity = direction*enemySpeed;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, 200*Time.deltaTime);
    }
    





}
