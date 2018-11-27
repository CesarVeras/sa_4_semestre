using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public MapManager mapScript;
    public Transform doorToGo;

    // Use this for initialization
    void Start()
    {
        mapScript = Camera.main.GetComponent<MapManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (doorToGo != null)
            {
                mapScript.GoToDoor(doorToGo);
                
            }
        }
    }
}
