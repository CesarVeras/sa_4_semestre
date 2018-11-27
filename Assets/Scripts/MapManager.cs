using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    public List<GameObject> rooms;
    public GameObject currentRoom;
    public int roomCounter;
    public int enemyCounter;
    public bool canLerp;
    public float lerpControl = 0;
    public RoomScript currentRoomScript;

    // Use this for initialization
    void Start()
    {
        currentRoom = rooms[roomCounter];
        currentRoomScript = currentRoom.GetComponent<RoomScript>();
        enemyCounter = currentRoomScript.room.enemies.Length;
        foreach (var r in rooms)
        {
            r.GetComponent<RoomScript>().room.isCleared = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
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
        currentRoomScript = currentRoom.GetComponent<RoomScript>();
        canLerp = true;
        lerpControl = 0;
        enemyCounter = currentRoomScript.room.enemies.Length;
        if (!currentRoomScript.room.isCleared)
        {
            SpawnEnemy();
        }
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

    public void GoToDoor(Transform position)
    {
        if (Camera.main.GetComponent<MapManager>().currentRoomScript.room.isCleared)
        {
            var Player = GameObject.Find("Player");
            Player.transform.position = position.position;
            Vector3 difference = Player.transform.position - currentRoom.transform.position;
            Player.transform.position += difference.normalized * 2;
            currentRoom = position.gameObject.transform.parent.gameObject;
            Vector3 newPos = currentRoom.transform.position;
            var minimap = GameObject.Find("Minimap");
            newPos.z = minimap.transform.position.z;
            minimap.transform.position = newPos;
            NextRoom();
        }
    }
}
