using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    public GameObject heart;
    public int numOfHearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    private Rooms rooms;
    private int health;
    private GameObject[] hearts = new GameObject[10];
    void Start()
    {
        health = numOfHearts*2;
        rooms = GameObject.FindGameObjectWithTag("RoomList").GetComponent<Rooms>();
        
        for (int i = 0; i < numOfHearts; i++)
        {
            hearts[i] = Instantiate(heart, new Vector3(-7.75f, (2f+(-i*0.8f)), 0),new Quaternion());
            hearts[i].transform.parent = GameObject.Find("Main Camera").transform;
            hearts[i].layer = LayerMask.NameToLayer("TransparentFX");
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < numOfHearts; i++)
        {
            if (health == ((i+1)*2)-1)
            {
                hearts[i].GetComponent<SpriteRenderer>().sprite = halfHeart;
            }
            else if (health < (i+1)*2)
            {
                hearts[i].GetComponent<SpriteRenderer>().sprite = emptyHeart;
            }
            else
            {
                hearts[i].GetComponent<SpriteRenderer>().sprite = fullHeart;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.H))
        {
            health = health - 1;
        }
        
        if (health <= 0)
        {
            rooms.GameOver();
        }
    }

    public void loseHealth()
    {
        health = health - 1;
        GetComponent<AudioSource>().Play();
    }
}
