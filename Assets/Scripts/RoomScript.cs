using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    public Room room;
    public GameObject[] walls;
    public GameObject[] doors;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    void Start()
    {
        walls = new GameObject[4];
        doors = new GameObject[4];
        GetWallsAndDoors();
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < room.enemies.Length; i++)
        {
            var enemy = Instantiate(room.enemies[i], new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY)), Quaternion.identity);
            enemy.transform.SetParent(gameObject.transform);
        }
    }

    public void GetWallsAndDoors()
    {
        int count = 4;
        for (int i = 0; i < 4; i++)
        {
            walls[i] = transform.GetChild(i).gameObject;
            doors[i] = transform.GetChild(count).gameObject;
            count++;
        }

        minX = walls[0].transform.position.x + walls[0].GetComponent<BoxCollider2D>().bounds.extents.x;
        maxX = walls[1].transform.position.x - walls[1].GetComponent<BoxCollider2D>().bounds.extents.x;

        minY = walls[2].transform.position.y - walls[2].GetComponent<BoxCollider2D>().bounds.extents.y;
        maxY = walls[3].transform.position.y + walls[3].GetComponent<BoxCollider2D>().bounds.extents.y;
    }
}
