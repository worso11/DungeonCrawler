using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject explosion;
    
    private float speed = 5f;
    private Transform player;
    private Vector3 target;
    private Vector3 direction;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        direction = (target - transform.position).normalized;

    }

    public void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        transform.position += direction * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    
        if (other.CompareTag("Player"))
        {
            GameObject.FindWithTag("Healthbar").GetComponent<Healthbar>().loseHealth();
        }
        else
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }
}
