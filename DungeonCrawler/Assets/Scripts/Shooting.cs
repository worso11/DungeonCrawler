using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private float speed = 10f;
    private float damage = 20f;
    public Rigidbody2D bullet;
    public GameObject player;
    public GameObject explosion;
        
    public void onStart(float sp, float dmg)
    {
        speed = sp;
        damage = dmg;
        bullet.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().takeDamage(damage);
        }
        else
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }

    public void setSpeed(float sp)
    {
        speed = sp;
    }
}
