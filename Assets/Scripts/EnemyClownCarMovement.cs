using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClownCarMovement : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Rigidbody2D m_rigidbody;

    [Header("Attributes")]
    [SerializeField] private float emenySpeed = 3f;
    //how long to wait before clown car starts
    [SerializeField] private float initialWait = 3f;
    private bool waited = false;
    

    //car's next point to go to
    private Transform target;
    private int targetIndex = 0;
    Vector2 direction;

    private void Start()
    {
        target = LevelManager.main.carPath[targetIndex];
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(waitbeforeanything());
    }

    //update the next position
    private void Update()
    {       
        if (waited)
            ChooseCarsNextPoint();
    }

    private IEnumerator waitbeforeanything()
    {
        yield return new WaitForSeconds(Random.Range(0.5f,initialWait));
        
        waited = true;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void ChooseCarsNextPoint()
    {
        
            if (Vector2.Distance(target.position, transform.position) <= 0.1f)
            {
                targetIndex++;
                if (targetIndex > LevelManager.main.carPath.Length)
                {
                BusSpawner.onCarReachingHospital.Invoke();
                Destroy(gameObject);
                    return;
                }
                else
                    target = LevelManager.main.carPath[targetIndex];
            }
        
    }


    //move and rotate to the next position
    private void FixedUpdate()
    {
      
        if (waited)
        {
            direction = (target.position - transform.position).normalized;
            m_rigidbody.velocity = direction * Random.Range(emenySpeed - 0.5f, emenySpeed + 0.5f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, 200 * Time.deltaTime);
        }
    }
}
