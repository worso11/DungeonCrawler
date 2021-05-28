using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ShotgunWeapon : MonoBehaviour, Weapon
{
    public GameObject bullet;

    private Color color = new Color(1,0,1,1);
    private static float speed = 10f;
    private static float damage = 30f;
    private float fireRate = 1f;
    private float nextFire = 0.0f;
    
    public void Start()
    {
        bullet.GetComponent<Shooting>().setSpeed(speed);
        bullet.GetComponent<SpriteRenderer>().color = color;
        bullet.transform.GetChild(0).GetComponent<Light2D>().color = color;
        GameObject.Find("Weapon").transform.GetChild(0).GetComponent<Light2D>().color = color;
    }
    
    public void Shoot(Transform shootingPoint)
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject shot = Instantiate(bullet, shootingPoint.position, shootingPoint.rotation*Quaternion.Euler(0, 0, -15));
            shot.GetComponent<Shooting>().onStart(speed, damage);
            shot = Instantiate(bullet, shootingPoint.position, shootingPoint.rotation);
            shot.GetComponent<Shooting>().onStart(speed, damage);
            shot = Instantiate(bullet, shootingPoint.position, shootingPoint.rotation*Quaternion.Euler(0, 0, 15));
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
}
