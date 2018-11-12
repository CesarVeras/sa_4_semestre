using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public class Room
    {

    }

    // Use this for initialization
    void Start()
    {
        LoadJSON("levelManager.json");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LoadJSON(string path)
    {
        path = path.Replace(".json", "");
        string json = Resources.Load<TextAsset>(path).text;
        print(json);
        Room room = JsonUtility.FromJson<Room>(json);
    }
}
