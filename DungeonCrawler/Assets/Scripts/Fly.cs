using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour, Enemy
{
    public Rigidbody2D fly;
    
    private float health;
    private EnemyCounter enemyCount;
    
    public void Start()
    {
        enemyCount = GameObject.FindGameObjectWithTag("EnemyCounter").GetComponent<EnemyCounter>();
        health = 50f + 10f * LevelNum.level;
        fly.velocity = new Vector2(1,1)*4f;
    }

    public void Update()
    {
        
    }

    public void takeDamage(float dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            GameObject.FindWithTag("Healthbar").GetComponent<Healthbar>().loseHealth();
            Destroy(gameObject);
        }
    }

    public IEnumerator Knockback(float duration, float power, Transform obj)
    {
        throw new System.NotImplementedException();
    }

    public void OnBecameVisible()
    {
        
    }

    public void OnBecameInvisible()
    {
        
    }

    public void OnDestroy()
    {
        enemyCount.AddEnemyNum(-1);
        GameObject.Find("DeathSound").GetComponent<AudioSource>().Play();
    }
}
