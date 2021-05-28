using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class MoveCamera : MonoBehaviour
{
    private Transform player;
    private Rooms rooms;
    private void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rooms = GameObject.FindGameObjectWithTag("RoomList").GetComponent<Rooms>();
        SceneManager.LoadScene("Game");
    }

    // Update is called once per frame
    private void Update()
    {
        if (player.position.x - transform.position.x > 8)
        {
            PrepareRoom(1,0);
        }
        else if (player.position.x - transform.position.x < -8)
        {
            PrepareRoom(-1,0);
        }
        else if (player.position.y - transform.position.y > 5)
        {
            PrepareRoom(0,1);
        }
        else if (player.position.y - transform.position.y < -5)
        {
            PrepareRoom(0,-1);
        }
    }

    public void PrepareRoom(float x, float y)
    {
        rooms.Spawn(transform.position,x, y);
        player.transform.Translate(x,y,0);
        transform.Translate(x*15,y*9,0);
    }
}
