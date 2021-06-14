using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : MonoBehaviour, Enemy
{
    public GameObject bullet;

    private GameObject player;
    private float health;
    private EnemyCounter enemyCount;
    private float attackTime = 2f;
    
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyCount = GameObject.FindGameObjectWithTag("EnemyCounter").GetComponent<EnemyCounter>();
        health = 60f + 20f * LevelNum.level;
    }

    public void Update()
    {
        if (attackTime <= 0f)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            attackTime = 2f;
        }
        else
        {
            attackTime -= Time.deltaTime;   
        }
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
