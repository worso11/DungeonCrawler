using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    private Rooms rooms;
    
    private void Start()
    {
        rooms = GameObject.FindGameObjectWithTag("RoomList").GetComponent<Rooms>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rooms.Reset();
        }
    }
}
