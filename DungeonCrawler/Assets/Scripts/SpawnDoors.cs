using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDoors : MonoBehaviour
{
    public Sprite openedDoors;
    public Sprite closedDoors;

    private SpriteRenderer mySprite;
    private BoxCollider2D myCollider;
    private EnemyCounter counter;
    private bool closed;
    private GameObject player;
    private Collider2D playerCol;
    private Collider2D col;
    private Collider2D otherCol;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        closed = true;
        mySprite = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<BoxCollider2D>();
        counter = GameObject.FindGameObjectWithTag("EnemyCounter").GetComponent<EnemyCounter>();
        playerCol = player.GetComponent<Collider2D>();
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (closed && counter.GetEnemyNum() == 0 && mySprite.sprite == closedDoors)
        {
            Debug.Log("otwieranie drzwi");
            closed = false;
            OpenDoors();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("CollisionPoint") && mySprite != null)
        {
            otherCol = other.gameObject.GetComponent<Collider2D>();
            
            mySprite.sprite = openedDoors;
            other.gameObject.GetComponent<SpriteRenderer>().sprite = openedDoors;

            closed = false;
            Physics2D.IgnoreCollision(col, playerCol);
            Physics2D.IgnoreCollision(col, otherCol);
            Physics2D.IgnoreCollision(otherCol, playerCol);
        }
        else if (other.gameObject.CompareTag("DoorPoint") && mySprite != null && mySprite.sprite == openedDoors)
        {
            Debug.Log("zamykanie drzwi");
            closed = true;
            CloseDoors();
        }
    }

    public void CloseDoors()
    {
        mySprite.sprite = closedDoors;
        Physics2D.IgnoreCollision(col, playerCol, false);
        
    }

    public void OpenDoors()
    {
        mySprite.sprite = openedDoors;
        Physics2D.IgnoreCollision(col, playerCol);
    }
}
