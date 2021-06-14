using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BasicWeapon : MonoBehaviour, Weapon
{
    public GameObject bullet;

    private Color color = new Color(0,1,1,1);
    private static float speed = 20f;
    private static float damage = 10f;
    private float fireRate = 0.5f;
    private float nextFire = 0.0f;

    public void PrepareWeapon(int lvl)
    {
        bullet.GetComponent<Shooting>().setSpeed(speed);
        bullet.GetComponent<SpriteRenderer>().color = color;
        bullet.transform.GetChild(0).GetComponent<Light2D>().color = color;
        GameObject.Find("Weapon").transform.GetChild(0).GetComponent<Light2D>().color = color;
        damage = 10f + (10f * lvl);
    }
    public void Shoot(Transform shootingPoint)
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject shot = Instantiate(bullet, shootingPoint.position, shootingPoint.rotation);
            shot.GetComponent<Shooting>().onStart(speed, damage);
        }
    }

    public float getSpeed()
    {
        return speed;
    }

    public float getFireRate()
    {
        return fireRate;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().GetWeapon(0);
            Destroy(gameObject);
        }
    }
}
