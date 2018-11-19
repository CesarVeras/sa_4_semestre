using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    public GameObject[] rooms;
    public Queue<GameObject> rooms_queue = new Queue<GameObject>();
    public GameObject currentRoom;
    public int roomCounter;

    // Use this for initialization
    void Start()
    {
        foreach (GameObject r in rooms)
        {
            rooms_queue.Enqueue(r);
            print(r.name);
        }
        print(GameObject.Find("Room 1").transform.Find("Wall D").name);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void NextRoom()
    {
        if (rooms_queue.Count < 0)
        {
            return;
        }
        roomCounter++;
        currentRoom = rooms_queue.Dequeue();
    }

    void MoveToCurrentRoom()
    {
        Camera.main.transform.position = currentRoom.transform.position;
    }
}
