using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        // LoadJSON("levelManager.json");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LoadJSON()
    {
        //path = path.Replace(".json", "");
        // string json = Resources.Load<TextAsset>(path).text;
        // print(json);
        // Data data = JsonUtility.FromJson<Data>(json);
        // print(data);
        // var path = Resources.Load<>();
    }
}
public class Data
{
    public Room[] rooms;
    public float width;
    public float height;
}