using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private int amountMoney;

    // Use this for initialization
    void Start()
    {
        amountMoney = (int)(Random.value * 9) + 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collider = collision.gameObject;
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<Player>().AddMoney(amountMoney);
            Destroy(this.gameObject);
        }
    }
}
