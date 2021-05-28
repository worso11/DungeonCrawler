using System;
using System.Collections;
using Pathfinding;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AIPath aiPath;
    
    private float health = 100;
    private EnemyCounter enemyCount;

    public void Start()
    {
        // GetComponent<AIDestinationSetter>().target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        enemyCount = GameObject.FindGameObjectWithTag("EnemyCounter").GetComponent<EnemyCounter>();
    }

    public void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } 
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void takeDamage(float dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            Destroy(gameObject);
            enemyCount.AddEnemyNum(-1);
            GameObject.Find("DeathSound").GetComponent<AudioSource>().Play();
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            GameObject.FindWithTag("Healthbar").GetComponent<Healthbar>().loseHealth();

            StartCoroutine(Knockback(0.5f, 200f, other.collider.transform));
        }
    }

    public IEnumerator Knockback(float duration, float power, Transform obj)
    {
        float timer = 0;
        GetComponent<AIDestinationSetter>().enabled = false;
        //obj.GetComponent<Movement>().switchMove();
        Vector2 direction = (obj.transform.position - transform.position).normalized;
        
        while( duration > timer )
        {
            timer += Time.deltaTime;
            GetComponent<Rigidbody2D>().AddForce(-direction * power);
            //obj.GetComponent<Rigidbody2D>().AddForce(direction * power/(duration/Time.deltaTime));
            yield return new WaitForSeconds(Time.deltaTime);
        }
        
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        
        GetComponent<AIDestinationSetter>().enabled = true;
        //obj.GetComponent<Movement>().switchMove();
        
        yield return 0;
    }

    private void OnBecameVisible()
    {
        GetComponent<AIDestinationSetter>().target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
    }

    private void OnBecameInvisible()
    {
        GetComponent<AIDestinationSetter>().target = null;
    }
}
