using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomSpawner : MonoBehaviour
{
    public int dir;
    public GameObject room;
    // L -> 1
    // T -> 2
    // R -> 3
    // B -> 4
    private GameObject[][] allRooms;
    private Rooms rooms;
    private int rand;
    private bool spawned = false;
    // Start is called before the first frame update
    private void Start()
    {
        rooms = GameObject.FindGameObjectWithTag("RoomList").GetComponent<Rooms>();
        Invoke(nameof(SpawnRoom),0.1f);
        allRooms = new [] {rooms.L, rooms.T, rooms.R, rooms.B};
    }

    private void SpawnRoom()
    {
        if (spawned == false)
        {
            /*if (dir == 1)
            {
                rand = Random.Range(0, rooms.L.Length);
                room = Instantiate(rooms.L[rand], transform.position, rooms.L[rand].transform.rotation);
            }
            else if (dir == 2)
            {
                rand = Random.Range(0, rooms.T.Length);
                room = Instantiate(rooms.T[rand], transform.position, rooms.T[rand].transform.rotation);
            }
            else if (dir == 3)
            {
                rand = Random.Range(0, rooms.R.Length);
                room = Instantiate(rooms.R[rand], transform.position, rooms.R[rand].transform.rotation);
            }
            else if (dir == 4)
            {
                rand = Random.Range(0, rooms.B.Length);
                room = Instantiate(rooms.B[rand], transform.position, rooms.B[rand].transform.rotation);
            }*/
            if (dir != 0)
            {
                rand = Random.Range(0, allRooms[dir-1].Length);
                room = Instantiate(allRooms[dir-1][rand], transform.position, allRooms[dir-1][rand].transform.rotation);
                //Debug.Log( room.name.Length + ":" + transform.position);
            }
            spawned = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (dir == 0)
        {
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                Debug.Log("kolizja");
                //Instantiate(rooms.closed, transform.position, Quaternion.identity);
                if (other.GetComponent<RoomSpawner>().dir >= dir)
                {
                    Destroy(gameObject);
                }
                else
                {
                    Destroy(other.gameObject);
                }
            }
            else
            {
                spawned = true;
            }
        }
    }
}
