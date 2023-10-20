using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMechanics : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D ownRigidBody;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletPower = 1;

    private Transform enemyLocation;

    public void SetEnemyLocation(Transform _transform)
    {
        enemyLocation = _transform;
    }    

    private void FixedUpdate()
    {
        if (!enemyLocation)
            return;
        Vector2 direction = (enemyLocation.position - transform.position).normalized;
        ownRigidBody.velocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<BusHealth>().TakeDamage(bulletPower);
        Destroy(gameObject);
    }
}
