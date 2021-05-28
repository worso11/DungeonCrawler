using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    private Rooms rooms;
    // Start is called before the first frame update
    void Start()
    {
        rooms = GameObject.FindGameObjectWithTag("RoomList").GetComponent<Rooms>();
        Vector3 pos = transform.position;
        rooms.rooms.Add(new Tuple<float, float>(pos.x, pos.y),new Tuple<GameObject,int>(gameObject,0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
