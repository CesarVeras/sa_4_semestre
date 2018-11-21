using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    public GameObject[] rooms;
    public Queue<GameObject> rooms_queue = new Queue<GameObject>();
    public GameObject currentRoom;
    public int roomCounter;
    public int enemyCounter;
    public bool canLerp;
    public float lerpControl = 0;
    public RoomScript currentRoomScript;

    // Use this for initialization
    void Start()
    {
        foreach (GameObject r in rooms)
        {
            rooms_queue.Enqueue(r);
            // print(r.name);
        }
        currentRoom = rooms_queue.Dequeue();
        currentRoomScript = currentRoom.GetComponent<RoomScript>();
        enemyCounter = currentRoomScript.room.enemies.Length;
        SpawnEnemy();
        // print(GameObject.Find("Room 1").transform.Find("Wall D").name);
    }

    // Update is called once per frame
    void Update()
    {
        print(currentRoom.name);
        print(currentRoomScript.room.isCleared);
        if (currentRoom.transform.childCount == 12)
        {
            currentRoomScript.room.isCleared = true;
        }
        if (canLerp)
        {
            Vector3 newpos = Vector3.Lerp(Camera.main.transform.position, currentRoom.transform.position, lerpControl);
            newpos.z = transform.position.z;
            transform.position = newpos;
            lerpControl += 0.04f;
            if (lerpControl >= 1)
            {
                canLerp = false;
            }
        }
    }

    void NextRoom()
    {
        if (rooms_queue.Count < 0)
        {
            return;
        }
        roomCounter++;
        currentRoom = rooms_queue.Dequeue();
        currentRoomScript = currentRoom.GetComponent<RoomScript>();
        canLerp = true;
        lerpControl = 0;
        enemyCounter = currentRoomScript.room.enemies.Length;
        SpawnEnemy();
    }

    void MoveToCurrentRoom()
    {
        Camera.main.transform.position = currentRoom.transform.position;
    }


    public void DecreaseEnemyCount()
    {
        enemyCounter--;
    }

    public void SpawnEnemy()
    {
        currentRoomScript.SpawnEnemies();
    }

    /*
        0 - left
        1 - right
        2 - up
        3 - down
     */
    public void GoToDoor(int index)
    {
        if (Camera.main.GetComponent<MapManager>().currentRoomScript.room.isCleared)
        {
            NextRoom();
            GameObject.Find("Player").transform.position = currentRoomScript.doors[index].transform.position;
        }
    }
}
