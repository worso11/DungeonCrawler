using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Machinegun : MonoBehaviour, Weapon
{
    public GameObject bullet;

    private Color color = new Color(1,0.4f,0,1);
    private static float speed = 10f;
    private static float damage = 5f;
    private float fireRate = 1f;
    private float nextFire = 0.0f;

    public void PrepareWeapon(int lvl)
    {
        bullet.GetComponent<Shooting>().setSpeed(speed);
        bullet.GetComponent<SpriteRenderer>().color = color;
        bullet.transform.GetChild(0).GetComponent<Light2D>().color = color;
        GameObject.Find("Weapon").transform.GetChild(0).GetComponent<Light2D>().color = color;
        damage = 5f + (5f * lvl);
    }
    
    public void Shoot(Transform shootingPoint)
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            StartCoroutine(ShootMachinegun());
        }

        IEnumerator ShootMachinegun()
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject shot = Instantiate(bullet, shootingPoint.position, shootingPoint.rotation);
                shot.GetComponent<Shooting>().onStart(speed, damage);
                yield return new WaitForSeconds(0.1f);
            }
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
            GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().GetWeapon(2);
            Destroy(gameObject);
        }
    }
}
