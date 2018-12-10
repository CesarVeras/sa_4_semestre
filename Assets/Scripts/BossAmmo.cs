using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAmmo : MonoBehaviour
{

    [HideInInspector] public Vector3 pointToGo;
    [HideInInspector] public float speed;
    [HideInInspector] public float damage;
    [HideInInspector] public Vector3 difference;
    private Rigidbody2D rb;

    void Start()
    {
        difference = (pointToGo - transform.position).normalized;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = difference * speed;
    }

    void Update()
    {
        if (transform.position == pointToGo)
        {
            Destroy(gameObject);
        }
        rb.velocity = difference * speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
