using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    public Rigidbody2D fly;
    void Start()
    {
        fly.velocity = new Vector2(1,1)*4f;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameObject.FindWithTag("Healthbar").GetComponent<Healthbar>().loseHealth();
        }
    }
}
